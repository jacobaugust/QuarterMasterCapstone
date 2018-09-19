using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuarterMaster.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }



        [Display(Name = "Stock Ticker Symbol")]
        public string ticker { get; set; }

        [Display(Name = "Article Title")]
        public string Subject { get; set; }
        
        //Convert to DateTime
        [Display(Name = "Article Date")]
        public string Start { get; set; }

        [Display(Name = "Article URL")]
        public string Description { get; set; }
    }
}