using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XPRS.Models.Serialized;
using XPRS.Models;
using XPRS.Models.Entities;
using XPRS.Utilities;

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
            throw new NotImplementedException();
        }

        public SerializedOrder GetOrder(int orderID)
        {
            SerializedOrder sord = _db.Orders.Where(o => o.OrderID == orderID).Select(SerializedOrder.GenerationLambda).FirstOrDefault();
            return sord;
        }

        public List<SerializedOrder> GetOrders()
        {
            List<SerializedOrder> sords = _db.Orders.AsQueryable().Select(SerializedOrder.GenerationLambda).ToList();
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
    }
}