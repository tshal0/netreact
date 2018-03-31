using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace XPRS.Models.Entities
{
    public class Contractor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContractorID { get; set; }

        [Index(IsUnique =true)]
        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        public virtual List<Placement> Placements { get; set; }
        public virtual List<Document> Documents { get; set; }
    }
}