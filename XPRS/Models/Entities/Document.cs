using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XPRS.Models.Entities
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentID { get; set; }
        public string OriginalFileName { get; set; }


        [Index(IsUnique = true)]
        [StringLength(20)]
        public string UniqueFileName { get; set; }
    }
}