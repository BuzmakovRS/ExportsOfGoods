using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExportsOfGoods.Models
{
    public class Parti
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PartiId"), DisplayName("№ партии")]
        public int Id { get; set; }
        [Required]
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        [Required, DisplayName("Размер партии")]
        public int PartiSize { get; set; }
        [DisplayName("Время на досмотр"), DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:t}")]
        public DateTime? InspectionTime { get; set; }
        //[DisplayName("Дата досмотра"), DisplayFormat(ApplyFormatInEditMode = true)]
        //public DateTime? InspectionDate { get; set; }
        //[NotMapped]
        //public string InspectionTimeDisplay
        //{
        //    get
        //    {

        //        return InspectionTime.HasValue ? InspectionTime.Value.ToShortTimeString() : "";
        //    }
        //    set
        //    {
        //        if (string.IsNullOrEmpty(value))
        //            InspectionTime = null;
        //        else
        //        {
        //            var timeArg = value.Split('.', ':').Cast<int>().ToArray();
        //            int hour = 0;
        //            int min = 0;
        //            int sec = 0;

        //            if (timeArg.Any())
        //                hour = timeArg[0];
        //            if (timeArg.Length > 1)
        //                min = timeArg[1];
        //            if (timeArg.Length > 2)
        //                sec = timeArg[2];

        //            var datetime = DateTime.Now;
        //            InspectionTime = new DateTime(0, 0, 0, hour, min, sec);
        //        }
        //    }
        //}
    }
}