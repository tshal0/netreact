using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace XPRS.Models.Entities
{
    public class LaborCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LaborCategoryID { get; set; }

        public string Name { get; set; }
        public string PLC { get; set; }

    }
}