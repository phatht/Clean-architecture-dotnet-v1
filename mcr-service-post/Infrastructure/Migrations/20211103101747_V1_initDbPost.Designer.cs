﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using mcr_service_user.Infrastructure.Data;

namespace mcr_service_post.Migrations
{
    [DbContext(typeof(PostDbContext))]
    [Migration("20211103101747_V1_initDbPost")]
    partial class V1_initDbPost
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("mcr_service_post.Domain.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tile")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e3283297-5c6c-4562-b1f6-d4ffeb3c5d2b"),
                            Content = "Content 1",
                            Name = "Huỳnh Tấn Phát",
                            Tile = "Tile 1",
                            UserId = new Guid("637f0a75-0185-44ee-a550-e01c6effec75")
                        },
                        new
                        {
                            Id = new Guid("ea43574b-66f3-4963-bea8-c128d480ac21"),
                            Content = "Content 2",
                            Name = "Huỳnh Tấn Phát",
                            Tile = "Tile 2",
                            UserId = new Guid("e63a1343-ec97-4d91-b0bf-690df5eaafc9")
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
