﻿// <auto-generated />
using System;
using BasementBlog.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BasementBlog.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240130150432_morePosts")]
    partial class morePosts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("BasementBlog.Database.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Post");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "Test",
                            Description = "Test",
                            ModifiedDate = new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6268),
                            PublishDate = new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6289),
                            Title = "Make the world better"
                        },
                        new
                        {
                            Id = 2,
                            Body = "Test",
                            Description = "Test",
                            ModifiedDate = new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6291),
                            PublishDate = new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6292),
                            Title = "AI take over the world"
                        },
                        new
                        {
                            Id = 3,
                            Body = "Test",
                            Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum",
                            ModifiedDate = new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6294),
                            PublishDate = new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6294),
                            Title = "Lorem Ipsum"
                        },
                        new
                        {
                            Id = 4,
                            Body = "Test",
                            Description = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots",
                            ModifiedDate = new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6296),
                            PublishDate = new DateTime(2024, 1, 30, 23, 4, 32, 469, DateTimeKind.Local).AddTicks(6297),
                            Title = "Where does it come from?"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
