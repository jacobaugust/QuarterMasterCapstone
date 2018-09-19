using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace QuarterMaster.Models
{
    
    public class StockBasic
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Stock Symbol")]
        public string ticker { get; set; }
        [Display(Name = "Company Name")]
        public string name { get; set; }
        [Display(Name = "Exchange")]
        public string stock_exchange { get; set; }
        [Display(Name = "Sector")]
        public string sector { get; set; }
        [Display(Name = "Website")]
        public string company_url { get; set; }

        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string StockExchange { get; set; }
        //public string Ticker { get; set; }
        //public string Sector { get; set; }
        //public string IndustryCategory { get; set; }
        //public string ShortDescription { get; set; }
        //public string CompanyURL { get; set; }


    }
}