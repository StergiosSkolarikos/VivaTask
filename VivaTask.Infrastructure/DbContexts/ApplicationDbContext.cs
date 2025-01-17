using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivaTask.Domain.Models;

namespace VivaTask.Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CountriesPersistenceModel> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountriesPersistenceModel>(entity =>
            {
                entity.ToTable(nameof(Countries));

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.CommonName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Capital)
                      .HasMaxLength(200)
                      .IsRequired(false);

                entity.Property(e => e.Borders)
                      .HasMaxLength(600)
                      .IsRequired(false);
            });
        }
    }
}
