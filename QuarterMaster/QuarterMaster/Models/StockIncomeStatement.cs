using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuarterMaster.Models
{
    public class StockIncomeStatement
    {
        [Key]
        public int Id { get; set; }
        public string ticker { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Net Income")]
        public decimal? netincome { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Total Revenue")]
        public decimal? totalrevenue { get; set; }
        //[DataType(DataType.Currency)]
        //[Display(Name = "Total Cost of Revenue")]
        //public decimal? TotalCostOfRevenue { get; set; }
        //[DataType(DataType.Currency)]
        //[Display(Name = "Total Operating Income")]
        //public decimal? TotalOperatingIncome { get; set; }
        //[DataType(DataType.Currency)]
        //[Display(Name = "Total Operating Expenses")]
        //public decimal? TotalOperatingExpenses { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "EPS")]
        public decimal? basiceps { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Total Gross Profit")]
        public decimal? totalgrossprofit { get; set; }
        //[DataType(DataType.Currency)]
        //[Display(Name = "Cash Dividends Per Share")]
        //public decimal? CashDividendsPerShare { get; set; }
    }
}