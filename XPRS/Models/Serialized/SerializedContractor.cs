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
    public class SerializedContractor
    {
        public string Name { get; set; }
        public int ContractorID { get; set; }
        public SerializedContractor() { }


        public static Expression<Func<Contractor, SerializedContractor>> GenerationLambda =
            (o => new SerializedContractor
            {
                ContractorID = o.ContractorID,
                Name = o.Name

            });
    }
}