﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(SamuraiContext))]
    [Migration("20210405100210_SamuraiBattleStatus")]
    partial class SamuraiBattleStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("Domain.Clan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClanName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clans");
                });

            modelBuilder.Entity("Domain.Horse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SamuraiId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SamuraiId")
                        .IsUnique();

                    b.ToTable("Horses");
                });

            modelBuilder.Entity("Domain.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SamuraiId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SamuraiId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("Domain.Samurai", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClanId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClanId");

                    b.ToTable("Samurais");
                });

            modelBuilder.Entity("Domain.SamuraiBattle", b =>
                {
                    b.Property<int>("SamuraiId")
                        .HasColumnType("int");

                    b.Property<int>("BattleId")
                        .HasColumnType("int");

                    b.HasKey("SamuraiId", "BattleId");

                    b.HasIndex("BattleId");

                    b.ToTable("SamuraiBattle");
                });

            modelBuilder.Entity("Domain.Horse", b =>
                {
                    b.HasOne("Domain.Samurai", null)
                        .WithOne("Horse")
                        .HasForeignKey("Domain.Horse", "SamuraiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Quote", b =>
                {
                    b.HasOne("Domain.Samurai", "Samurai")
                        .WithMany("Quotes")
                        .HasForeignKey("SamuraiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Samurai");
                });

            modelBuilder.Entity("Domain.Samurai", b =>
                {
                    b.HasOne("Domain.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId");

                    b.Navigation("Clan");
                });

            modelBuilder.Entity("Domain.SamuraiBattle", b =>
                {
                    b.HasOne("Domain.Battle", "Battle")
                        .WithMany("SamuraiBattles")
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Samurai", "Samurai")
                        .WithMany("SamuraiBattles")
                        .HasForeignKey("SamuraiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Battle");

                    b.Navigation("Samurai");
                });

            modelBuilder.Entity("Domain.Battle", b =>
                {
                    b.Navigation("SamuraiBattles");
                });

            modelBuilder.Entity("Domain.Samurai", b =>
                {
                    b.Navigation("Horse");

                    b.Navigation("Quotes");

                    b.Navigation("SamuraiBattles");
                });
#pragma warning restore 612, 618
        }
    }
}
