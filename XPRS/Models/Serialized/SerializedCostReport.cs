using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

using XPRS.Models.Entities;

namespace XPRS.Models.Serialized
{
    public class SerializedCostReport
    {
        
        public SerializedCostReport() { }

        public string Contractor { get; set; }
        public string Order { get; set; }
        public DateTime ReportingPeriodStart { get; set; }
        public DateTime ReportingPeriodEnd { get; set; }
        public List<SerializedLineItem> LineItems { get; set; }


        public static Expression<Func<CostReport, SerializedCostReport>> GenerationLambda =
            (cr => new SerializedCostReport
            {
                Contractor = cr.Contractor,
                Order = cr.Order.ContractNumber,
                ReportingPeriodStart = cr.ReportingPeriodStart,
                ReportingPeriodEnd = cr.ReportingPeriodEnd,
                LineItems = cr.LineItems.AsQueryable().Select(SerializedLineItem.GenerationLambda).ToList()

            });
    }

    public class SerializedLineItem
    {
        public string SLIN { get; set; }

        public string POP { get; set; }

        public string Type { get; set; }
        public string Category { get; set; }
        public string PLC { get; set; }

        public Decimal ContractorRate { get; set; }

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




        public static Expression<Func<LineItem, SerializedLineItem>> GenerationLambda =
            (cr => new SerializedLineItem
            {
                AllottedHours = cr.AllottedHours,
                BudgetedHours = cr.BudgetedHours,
                CurrentHours = cr.CurrentHours,
                CumulativeHours = cr.CumulativeHours,
                HoursRemaining = cr.HoursRemaining,
                AmountAllotted = cr.AmountAllotted,
                AmountBudgeted = cr.AmountBudgeted,
                CurrentAmount = cr.CurrentAmount,
                CumulativeAmount = cr.CumulativeAmount,
                AmountRemaining = cr.AmountRemaining,

                SLIN = cr.SLIN.Name,
                ContractorRate = cr.ContractorRate.Rate,
                POP = cr.ContractorRate.ContractorPeriod.PopStart + " - " + cr.ContractorRate.ContractorPeriod.PopEnd,
                Type = cr.ContractorRate.Type.ToString(),
                Category = cr.ContractorRate.LaborCategory.Name,
                PLC = cr.ContractorRate.LaborCategory.PLC
            });
    }
}