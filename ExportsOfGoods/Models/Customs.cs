using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExportsOfGoods.Models
{
    public class Customs
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CustomsId")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Тамож. пункт"), StringLength(50)]
        public string Name { get; set; }
        [DisplayName("ID отправителя")]
        public int? SenderId { get; set; }
        [ForeignKey("SenderId"), DisplayName("Отправитель")]
        public Country CountrySend { get; set; }
        [DisplayName("ID получателя")]
        public int? RecipientId { get; set; }
        [ForeignKey("RecipientId"), DisplayName("Получатель")]
        public Country CountryRec { get; set; }
    }
}