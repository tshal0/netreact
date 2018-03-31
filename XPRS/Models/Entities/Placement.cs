using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XPRS.Models.Entities
{
    public class Placement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlacementID { get; set; }
        public int ContractorID { get; set; }
        public int OrderID { get; set; }
        public virtual Contractor Contractor { get; set; }
        public virtual Order Order { get; set; }
        public virtual List<Document> Documents { get; set; }
    }
}