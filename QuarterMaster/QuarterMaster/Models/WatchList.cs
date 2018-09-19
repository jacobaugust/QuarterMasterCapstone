using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuarterMaster.Models
{
    public class WatchList
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Stock")]
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}