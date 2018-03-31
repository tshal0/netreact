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
    public class SerializedPlacement
    {
        public SerializedContractor Contractor { get; set; }
        public List<SerializedDocument> Documents { get; set; }
        public int ContractorID { get; set; }
        public int OrderID { get; set; }

        public SerializedPlacement() { }


        public static Expression<Func<Placement, SerializedPlacement>> GenerationLambda =
            (o => new SerializedPlacement
            {
                Contractor = new SerializedContractor {
                    ContractorID = o.ContractorID,
                    Name = o.Contractor.Name
                },
                Documents = o.Documents.AsQueryable().Select(SerializedDocument.GenerationLambda).ToList(),
                ContractorID = o.ContractorID,
                OrderID = o.OrderID

            });
    }
}