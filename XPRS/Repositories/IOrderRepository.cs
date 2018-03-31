using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Delete

        void DeleteOrder(int orderID);
    }
}
