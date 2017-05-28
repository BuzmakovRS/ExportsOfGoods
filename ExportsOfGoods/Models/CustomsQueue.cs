using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExportsOfGoods.Models
{
    public class CustomsQueue
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int? CustomsId { get; set; }
        public Customs Customs { get; set; }
        [Required, DisplayName("№ партии")]
        public int? PartiId { get; set; }
        public Parti Parti { get; set; }
        [DisplayName("Начало досмотра"), DisplayFormat(ApplyFormatInEditMode = true,
    DataFormatString = "{0:G}")]
        public DateTime TimeBegInsp { get; set; }
        [DisplayName("Окончание досмотра"), DisplayFormat(ApplyFormatInEditMode = true,
    DataFormatString = "{0:G}")]
        public DateTime TimeEndInsp { get; set; }
    }


    //public class RangeDateAttribute : RangeAttribute
    //{
    //    public RangeDateAttribute()
    //      : base(typeof(DateTime),
    //              DateTime.Now.AddDays(-1).ToString("{0:g}"),
    //              DateTime.Now.AddDays(14).ToString("{0:g}"))
    //    { }
    //}
}