﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiComidas;

#nullable disable

namespace WebApiComidas.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220214183510_Restaurantes")]
    partial class Restaurantes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApiComidas.Entidades.Comida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Comidas");
                });

            modelBuilder.Entity("WebApiComidas.Entidades.Restaurante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ComidaId")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ComidaId");

                    b.ToTable("Restaurantes");
                });

            modelBuilder.Entity("WebApiComidas.Entidades.Restaurante", b =>
                {
                    b.HasOne("WebApiComidas.Entidades.Comida", "Comida")
                        .WithMany("restaurantes")
                        .HasForeignKey("ComidaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comida");
                });

            modelBuilder.Entity("WebApiComidas.Entidades.Comida", b =>
                {
                    b.Navigation("restaurantes");
                });
#pragma warning restore 612, 618
        }
    }
}