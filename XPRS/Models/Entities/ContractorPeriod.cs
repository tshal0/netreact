using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace XPRS.Models.Entities
{
    public class ContractorPeriod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContractorPeriodID { get; set; }

        public int ContractorID { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime PopStart { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime PopEnd { get; set; }

        public virtual Contractor Contractor { get; set; }
        public virtual List<ContractorRate> ContractorRates { get; set; }


    }
}