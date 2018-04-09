using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPRS.Models.Entities
{
    public class SLIN
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SLINID { get; set; }
        public string Name { get; set; }

        public virtual List<LineItem> LineItems { get; set; }

    }
}