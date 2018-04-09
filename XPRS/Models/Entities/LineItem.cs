using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace XPRS.Models.Entities
{
    public class LineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineItemID { get; set; }

        public int CostReportID { get; set; }
        public int SLINID { get; set; }
        public int ContractorRateID { get; set; }

        public Decimal AllottedHours { get; set; }
        public Decimal BudgetedHours { get; set; }

        public Decimal CurrentHours { get; set; }
        public Decimal CumulativeHours { get; set; }
        public Decimal HoursRemaining { get; set; }
        public Decimal AmountAllotted { get; set; }
        public Decimal AmountBudgeted { get; set; }
        public Decimal CurrentAmount { get; set; }
        public Decimal CumulativeAmount { get; set; }
        public Decimal AmountRemaining { get; set; }



        public virtual CostReport CostReport { get; set; }
        public virtual SLIN SLIN { get; set; }
        public virtual ContractorRate ContractorRate { get; set; }

    }
}