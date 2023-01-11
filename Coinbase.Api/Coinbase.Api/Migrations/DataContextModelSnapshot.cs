﻿// <auto-generated />
using System;
using Coinbase.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Coinbase.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Coinbase.Api.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("Coinbase.Api.Entities.Wallet", b =>
                {
                    b.Property<string>("ObjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("OwnerId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("RecoveryPhrase")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ObjectId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Wallet");
                });

            modelBuilder.Entity("Coinbase.Api.Entities.Wallet", b =>
                {
                    b.HasOne("Coinbase.Api.Entities.Owner", null)
                        .WithMany("Wallets")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coinbase.Api.Entities.Owner", b =>
                {
                    b.Navigation("Wallets");
                });
#pragma warning restore 612, 618
        }
    }
}
