using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoSimpliss.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace ProjetoSimpliss.Data
{
    public class ProjetoSimplissContext : DbContext
    {
        public ProjetoSimplissContext (DbContextOptions<ProjetoSimplissContext> options)
            : base(options)
        {
        }

        public DbSet<ProjetoSimpliss.Models.Contribuintes> Contribuintes { get; set; }
        public DbSet<ProjetoSimpliss.Models.Beneficios> Beneficios { get; set; } = default!;

        public DbSet<ProjetoSimpliss.Models.ContribBeneficio> ContribuintesBeneficios { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseNpgsql();
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da tabela intermediária ContribBeneficio
            modelBuilder.Entity<ContribBeneficio>()
                .HasKey(cb => cb.Id); // Definir a chave primária

            modelBuilder.Entity<ContribBeneficio>()
                .HasOne(cb => cb.Contribuinte)
                .WithMany()
                .HasForeignKey(cb => cb.ContribuinteId)
                .OnDelete(DeleteBehavior.Cascade); // Configura o comportamento ao excluir um Contribuinte

            modelBuilder.Entity<ContribBeneficio>()
                .HasOne(cb => cb.Beneficio)
                .WithMany()
                .HasForeignKey(cb => cb.BeneficioId)
                .OnDelete(DeleteBehavior.Cascade); // Configura o comportamento ao excluir um Benefício
        }
    }
}
