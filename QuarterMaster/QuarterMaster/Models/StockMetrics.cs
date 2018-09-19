using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuarterMaster.Models
{
    public class StockMetrics
    {
        [Key]
        public int Id { get; set; }

        public string ticker { get; set; }

        //[DataType(DataType.Currency)]
        //[Display(Name = "Market Capitalization")]
        //public decimal? MarketCap { get; set; }
        //[DataType(DataType.Currency)]
        //[Display(Name = "Total Capital")]
        //public decimal? TotalCapital { get; set; }
        //[DataType(DataType.Currency)]
        //[Display(Name = "Total Debt")]
        //public decimal? TotalDebt { get; set; }
        ////Percentages//
        [Display(Name = "Profit Margin")]
        public decimal? profitmargin { get; set; }

        [Display(Name = "Gross Margin")]
        public decimal? grossmargin { get; set; }

        //public decimal? ROE { get; set; }
        //[Display(Name = "Divident Yield")]
        //public decimal? DividendYield { get; set; }
        //Ratios//
        [Display(Name = "Current Ratio")]
        public float? currentratio { get; set; }
        //[Display(Name = "Quick Ratio")]
        //public float? QuickRatio { get; set; }
        //public float? Beta { get; set; }
        [Display(Name = "Dividend Yield")]
        public decimal? dividendyield { get; set; }

    }
}