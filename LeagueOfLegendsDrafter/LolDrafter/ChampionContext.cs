using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolDrafter
{
    internal class ChampionContext : DbContext
    {
        public DbSet<Champion> Champions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LeagueChampions;Trusted_Connection=True;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Champion>().HasData(
                new Champion { ChampionID = 1, ChampName = "Aatrox", Abilities = "q, w, e, r", GoodMatchup = "Ramus, Nausus", BadMatchup = "Kled, Jax", Build = "item1, item2, item3", Positions = "Top, Mid" }
                );
        }
    }
}
