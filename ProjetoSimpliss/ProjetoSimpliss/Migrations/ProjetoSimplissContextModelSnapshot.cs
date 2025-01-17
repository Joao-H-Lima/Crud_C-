﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjetoSimpliss.Data;

#nullable disable

namespace ProjetoSimpliss.Migrations
{
    [DbContext(typeof(ProjetoSimplissContext))]
    partial class ProjetoSimplissContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjetoSimpliss.Models.Beneficios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("NomeBeneficio")
                        .HasColumnType("text");

                    b.Property<double>("PorcentagemDesconto")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Beneficios");
                });

            modelBuilder.Entity("ProjetoSimpliss.Models.ContribBeneficio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BeneficioId")
                        .HasColumnType("integer");

                    b.Property<int>("ContribuinteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataVinculo")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BeneficioId");

                    b.HasIndex("ContribuinteId");

                    b.ToTable("ContribuintesBeneficios");
                });

            modelBuilder.Entity("ProjetoSimpliss.Models.Contribuintes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BeneficioId")
                        .HasColumnType("integer");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DataAbertura")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RegimeTributacao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BeneficioId");

                    b.ToTable("Contribuintes");
                });

            modelBuilder.Entity("ProjetoSimpliss.Models.ContribBeneficio", b =>
                {
                    b.HasOne("ProjetoSimpliss.Models.Beneficios", "Beneficio")
                        .WithMany()
                        .HasForeignKey("BeneficioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoSimpliss.Models.Contribuintes", "Contribuinte")
                        .WithMany()
                        .HasForeignKey("ContribuinteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beneficio");

                    b.Navigation("Contribuinte");
                });

            modelBuilder.Entity("ProjetoSimpliss.Models.Contribuintes", b =>
                {
                    b.HasOne("ProjetoSimpliss.Models.Beneficios", "Beneficio")
                        .WithMany()
                        .HasForeignKey("BeneficioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beneficio");
                });
#pragma warning restore 612, 618
        }
    }
}
