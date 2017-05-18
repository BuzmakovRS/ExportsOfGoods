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
        [DisplayName("Время досмотра"), DisplayFormat(ApplyFormatInEditMode= true,
            DataFormatString ="{0:T}")]
        public DateTime? InspectionTime { get; set; }
        [DisplayName("Дата досмотра"), DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:G}")]
        public DateTime? InspectionDate { get; set; }
    }
}