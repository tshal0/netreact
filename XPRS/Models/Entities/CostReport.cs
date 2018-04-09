using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace XPRS.Models.Entities
{
    public class CostReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CostReportID { get; set; }

        public string Contractor { get; set; }
        public int OrderID { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime ReportingPeriodStart { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime ReportingPeriodEnd { get; set; }

        public virtual Order Order { get; set; }
        public virtual List<LineItem> LineItems { get; set; }

    }
}