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
        [Range(1, int.MaxValue, ErrorMessage = "Введите целое число больше 0")]
        public int PartiSize { get; set; }
        public int? TypeOfInspectionId { get; set; }
        public TypeOfInspecion TypeOfInspection { get; set; }
        [DisplayName("Время на досмотр"), DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:T}")]
        public DateTime? InspectionTime { get; set; }

        [NotMapped]
        public string GetName { 
            get
            {
                Product pr = new ExportsContext().Products.Find(this.ProductId);
                return String.Format("{0} : {1} ({2})", Id, pr.Name, PartiSize);
            }
        }

        [NotMapped]
        public string GetFullName
        {
            get
            {
                Product pr = new ExportsContext().Products.Find(this.ProductId);
                return String.Format("{0} : {1} ({2}) [{3}]", Id, pr.Name, pr.NameProducer, PartiSize);
            }
        }
    }
}