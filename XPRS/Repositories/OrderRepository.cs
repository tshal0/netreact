using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;


using XPRS.Models.Serialized;
using XPRS.Models;
using XPRS.Models.Entities;
using XPRS.Utilities;

using OfficeOpenXml;

namespace XPRS.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        ModelContext _db = new ModelContext();

        public SerializedOrder CreateOrder(SerializedOrder sord)
        {
            Order ord = _db.Orders.Create();
            ord.ContractNumber = sord.ContractNumber;
            _db.Orders.Add(ord);
            _db.SaveChanges();

            sord = _db.Orders.Where(o => o.OrderID == ord.OrderID).Select(SerializedOrder.GenerationLambda).FirstOrDefault();
            return sord;

        }

        public void DeleteOrder(int orderID)
        {
            Order ord = _db.Orders.Where(o => o.OrderID == orderID).FirstOrDefault();
            _db.Documents.RemoveRange(ord.Placements.SelectMany(p => p.Documents.ToList()));
            _db.Documents.RemoveRange(ord.Documents);
            _db.Placements.RemoveRange(ord.Placements);
            _db.Orders.Remove(ord);
            _db.SaveChanges();
        }

        public SerializedOrder GetOrder(int orderID)
        {
            SerializedOrder sord = _db.Orders.Where(o => o.OrderID == orderID).Select(SerializedOrder.GenerationLambda).FirstOrDefault();
            return sord;
        }

        public List<SerializedOrder> GetOrders()
        {
            List<SerializedOrder> sords = _db.Orders.AsQueryable().Select(SerializedOrder.GenerationLambda).ToList();
            CostReport cr = _db.CostReports.FirstOrDefault();
            return sords;
        }

        public SerializedOrder UpdateOrder(SerializedOrder sord, int orderID)
        {
            Order ord = _db.Orders.Where(o => o.OrderID == orderID).FirstOrDefault();
            ord.ContractNumber = sord.ContractNumber;
            _db.SaveChanges();
            return GetOrder(orderID);
        }

        public SerializedOrder UpdateOrCreatePlacements(List<SerializedContractor> scs, int orderID)
        {
            Order ord = _db.Orders.Where(o => o.OrderID == orderID).FirstOrDefault();

            _db.Placements.RemoveRange(ord.Placements);

            foreach (SerializedContractor sc in scs)
            {
                Contractor ctr = _db.Contractors.Where(c => c.ContractorID == sc.ContractorID).FirstOrDefault();
                Placement placement = _db.Placements.Create();
                placement.Contractor = ctr;
                placement.Order = ord;
                _db.Placements.Add(placement);
            }

            _db.SaveChanges();

            return GetOrder(orderID);
        }

        public SerializedOrder UploadDocuments(List<HttpPostedFile> files, int orderID)
        {
            Order ord = _db.Orders.Where(o => o.OrderID == orderID).FirstOrDefault();

            foreach (HttpPostedFile f in files)
            {
                Document doc = _db.Documents.Create();
                
                doc.OriginalFileName = f.FileName;
                doc.UniqueFileName = System.IO.Path.GetRandomFileName();
                FileUtility.SaveFile(f, doc.UniqueFileName);
                _db.Documents.Add(doc);
                ord.Documents.Add(doc);
            }

            _db.SaveChanges();

            return GetOrder(orderID);
        }

        public SerializedOrder UploadDocument(HttpPostedFile file, int orderID)
        {


            return GetOrder(orderID);
        }

        public void GenerateCostReport(int orderID)
        {
            SerializedCostReport scr = _db.CostReports.Where(o => o.OrderID == orderID).Select(SerializedCostReport.GenerationLambda).FirstOrDefault();
            Order ord = _db.Orders.Where(o => o.OrderID == orderID).FirstOrDefault();
            Document doc = _db.Documents.Create();

            doc.OriginalFileName = "TestCostReport.xlsx";
            doc.UniqueFileName = System.IO.Path.GetRandomFileName();


            List<PropertyInfo> props = typeof(SerializedLineItem).GetProperties().ToList();

            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add(scr.Contractor);

                int row = 6, col = 1;
                foreach (SerializedLineItem sli in scr.LineItems)
                {
                    foreach (PropertyInfo prop in props)
                    {
                        ws.Cells[row, col].Value = prop.GetValue(sli, null);
                        col++;
                    }
                    row++;
                    col = 1;
                }
                

                FileUtility.SaveFile(p, doc.UniqueFileName);
            }

            _db.Documents.Add(doc);
            ord.Documents.Add(doc);

        }
    }
}