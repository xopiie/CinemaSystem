﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Uzunova_Nadica_1002387434_DSR_2021.Models;

namespace Uzunova_Nadica_1002387434_DSR_2021.Migrations.db
{
    [DbContext(typeof(dbContext))]
    [Migration("20210518161109_initialcontext")]
    partial class initialcontext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Uzunova_Nadica_1002387434_DSR_2021.Models.Dvorana", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pot_slike")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stevilo_sedezev")
                        .HasColumnType("int");

                    b.Property<bool>("ThreeD")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Dvorane");
                });

            modelBuilder.Entity("Uzunova_Nadica_1002387434_DSR_2021.Models.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumIzida")
                        .HasColumnType("datetime2");

                    b.Property<string>("Jezik")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<double>("Ocena")
                        .HasColumnType("float");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pot_slike")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Trajanje")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Filmi");
                });

            modelBuilder.Entity("Uzunova_Nadica_1002387434_DSR_2021.Models.Rezervacija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdSpored")
                        .HasColumnType("int");

                    b.Property<int>("SteviloSedezev")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Rezervacije");
                });

            modelBuilder.Entity("Uzunova_Nadica_1002387434_DSR_2021.Models.Spored", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumCas")
                        .HasColumnType("datetime2");

                    b.Property<string>("NaslovFilma")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NazivDvorane")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Spored");
                });
#pragma warning restore 612, 618
        }
    }
}
