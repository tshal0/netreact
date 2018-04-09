using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace XPRS.Models.Entities
{
    public class ContractorRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContractorRateID { get; set; }

        public int ContractorPeriodID { get; set; }
        public int LaborCategoryID { get; set; }
        public Decimal Rate { get; set; }
        public bool Type { get; set; }

        public virtual LaborCategory LaborCategory { get; set; }
        public virtual ContractorPeriod ContractorPeriod { get; set; }

    }
}