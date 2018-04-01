using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;

using XPRS.Models;
using XPRS.Models.Entities;
using XPRS.Models.Serialized;


namespace XPRS.Repositories
{
    public interface IOrderRepository
    {
        // Get

        SerializedOrder GetOrder(int orderID);
        List<SerializedOrder> GetOrders();
        

        // Create

        SerializedOrder CreateOrder(SerializedOrder sord);
        
        // Update 

        SerializedOrder UpdateOrder(SerializedOrder sord, int orderID);
        SerializedOrder UpdateOrCreatePlacements(List<SerializedContractor> scs, int orderID);
        SerializedOrder UploadDocuments(List<HttpPostedFile> files, int orderID);

        // Delete

        void DeleteOrder(int orderID);
    }
}
