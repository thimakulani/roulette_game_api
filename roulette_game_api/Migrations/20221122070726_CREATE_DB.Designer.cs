﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using roulette_game_api.Data;

namespace roulette_game_api.Migrations
{
    [DbContext(typeof(roulette_game_apiContext))]
    [Migration("20221122070726_CREATE_DB")]
    partial class CREATE_DB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("roulette_game_api.Model.BetResults", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("BetAmount")
                        .HasColumnType("REAL");

                    b.Property<string>("BetType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<string>("Player")
                        .HasColumnType("TEXT");

                    b.Property<int>("Rolled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BetResults");
                });
#pragma warning restore 612, 618
        }
    }
}
