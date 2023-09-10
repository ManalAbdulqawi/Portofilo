using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture
{
    public class DataContext:DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
        }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<Portfoilo> portfoilos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Portfoilo>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Owner>().HasData(
                new Owner
                {Id=Guid.NewGuid(),
                Avatar="Avatar.jpg",
                FullName="Manal Abdulqawi",
                Portfil="Asp.net Core Developer",



                });
        }
    }
}
