﻿// <auto-generated />
using System;
using BlazorKiteConnect.Server.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorKiteConnect.Server.Migrations
{
    [DbContext(typeof(KiteAppContext))]
    [Migration("20230212134454_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("BlazorKiteConnect.Shared.KiteModel.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Exchange")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ExchangeToken")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Expiry")
                        .HasColumnType("TEXT");

                    b.Property<int>("InstrumentToken")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InstrumentType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("LastPrice")
                        .HasColumnType("TEXT");

                    b.Property<int>("LotSize")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Segment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Strike")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TickSize")
                        .HasColumnType("TEXT");

                    b.Property<string>("TradingSymbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Instruments");
                });
#pragma warning restore 612, 618
        }
    }
}
