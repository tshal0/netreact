using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPRS.Models.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(50)]
        [Required]
        public string ContractNumber { get; set; }

        public virtual List<Placement> Placements { get; set; }
        public virtual List<Document> Documents { get; set; }
    }
}