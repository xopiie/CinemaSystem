using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uzunova_Nadica_1002387434_DSR_2021.Models
{
    public class dbContext : DbContext
    {
        public DbSet<Dvorana> Dvorane { get; set; }
        public DbSet<Film> Filmi { get; set; }
        public DbSet<Spored> Spored { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CinemaSystem");
        }


    }
}
