using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

using XPRS.Models.Entities;

namespace XPRS.Models.Serialized
{
    public class SerializedOrder 
    {
        public int OrderID { get; set; }
        public string ContractNumber { get; set; }
        public List<SerializedPlacement> Placements { get; set; }
        public List<SerializedDocument> Documents { get; set; }
        public SerializedOrder() { }


        public static Expression<Func<Order, SerializedOrder>> GenerationLambda =
            (o => new SerializedOrder
            {
                OrderID = o.OrderID,
                ContractNumber = o.ContractNumber,
                Placements = o.Placements.AsQueryable().Select(SerializedPlacement.GenerationLambda).ToList(),
                Documents = o.Documents.AsQueryable().Select(SerializedDocument.GenerationLambda).ToList()
            });
    }
}