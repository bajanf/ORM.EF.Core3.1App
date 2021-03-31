using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SamuraiContext: DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Clan> Clans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            string connectionstring = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog =  SamuraiAppData";
            optionBuilder.UseSqlServer(connectionstring);
            //base.OnConfiguring(optionBuilder);
        }
    }
}
