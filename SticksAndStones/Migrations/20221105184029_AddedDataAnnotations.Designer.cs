﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SticksAndStones.Models.DAL;

namespace SticksAndStones.Migrations
{
    [DbContext(typeof(PlayerDataContext))]
    [Migration("20221105184029_AddedDataAnnotations")]
    partial class AddedDataAnnotations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SticksAndStones.Models.DAL.Leaderboard", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long>("Ranking")
                        .HasColumnType("bigint");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Leaderboard");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Date = new DateTime(2022, 11, 5, 14, 40, 28, 764, DateTimeKind.Local).AddTicks(9541),
                            Ranking = 1L,
                            UserID = 1
                        },
                        new
                        {
                            ID = 2,
                            Date = new DateTime(2022, 11, 5, 14, 40, 28, 768, DateTimeKind.Local).AddTicks(2428),
                            Ranking = 1L,
                            UserID = 2
                        });
                });

            modelBuilder.Entity("SticksAndStones.Models.DAL.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GamesPlayed")
                        .HasColumnType("int");

                    b.Property<int>("GamesWon")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("RealName")
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("UserID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            GamesPlayed = 10,
                            GamesWon = 6,
                            IsActive = false,
                            Username = "jP"
                        },
                        new
                        {
                            UserID = 2,
                            GamesPlayed = 10,
                            GamesWon = 4,
                            IsActive = false,
                            Username = "Max"
                        });
                });

            modelBuilder.Entity("SticksAndStones.Models.DAL.Leaderboard", b =>
                {
                    b.HasOne("SticksAndStones.Models.DAL.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}