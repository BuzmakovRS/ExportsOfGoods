using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ExportsOfGoods.Models
{
    public class TypeOfInspecion
    {
        public int Id { get; set; }
        [DisplayName("Степень досмотра")]
        public string Type { get; set; }
        [DisplayName("Время досмотра на единицу товара")]
        public double Time { get; set; }
    }
}