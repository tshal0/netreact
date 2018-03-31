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
    public class SerializedDocument
    {
        public string OriginalName { get; set; }
        public string URLEncryptedID { get; set; }
        public SerializedDocument() { }


        public static Expression<Func<Document, SerializedDocument>> GenerationLambda =
            (o => new SerializedDocument
            {
                OriginalName= o.OriginalFileName
            });
    }
}