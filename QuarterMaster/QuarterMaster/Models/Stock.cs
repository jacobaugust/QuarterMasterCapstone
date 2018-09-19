using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;




namespace QuarterMaster.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("StockBasic")]
        public int StockBasicId { get; set; }
        public StockBasic StockBasic { get; set; }

        [ForeignKey("StockIncomeStatement")]
        public int StockIncomeStatementId { get; set; }
        public StockIncomeStatement StockIncomeStatement { get; set; }

        [ForeignKey("StockBalanceSheet")]
        public int StockBalanceSheetId { get; set; }
        public StockBalanceSheet StockBalanceSheet { get; set; }

        [ForeignKey("StockMetrics")]
        public int StockMetricsId { get; set; }
        public StockMetrics StockMetrics { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }


    }
}