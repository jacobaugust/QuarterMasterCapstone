using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuarterMaster.Models
{
    public class StockBalanceSheet
    {
        [Key]
        public int Id { get; set; }
        public string ticker { get; set; }

        //[DataType(DataType.Currency)]
        //[Display(Name = "Net Income")]
        //public decimal? NetIncome { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Total Assets")]
        public decimal? totalassets { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Total Liabilities")]
        public decimal? totalliabilities { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Total Equity")]
        public decimal? totalequity { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Retained Earnings")]
        public decimal? retainedearnings { get; set; }
        //[DataType(DataType.Currency)]
        //[Display(Name = "Total Common Equity")]
        //public decimal? TotalCommonEquity { get; set; }
    }
}