using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExportsOfGoods.Models
{
    public class Product
    {
        [Required]
        [Column("ProductId")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DisplayName("Товар"), StringLength(50)]
        public string Name { get; set; }
        [Required]
        [DisplayName("Производитель"), StringLength(50)]
        public string Producer { get; set; }

    }
}