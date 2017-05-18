using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ExportsOfGoods.Models
{
    public class ExportsContext: DbContext
    {
        public  ExportsContext()
            : base("ExportsContext") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Parti> Parties { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customs> Customs { get; set; }
        public DbSet<CustomsQueue> CustomsQueues { get; set; }
    }
}