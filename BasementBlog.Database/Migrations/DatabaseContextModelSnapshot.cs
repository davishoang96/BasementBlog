﻿// <auto-generated />
using System;
using BasementBlog.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BasementBlog.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("BasementBlog.Database.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BasementBlog.Database.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Id");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "Test",
                            Description = "Test",
                            ModifiedDate = new DateTime(2024, 4, 27, 0, 39, 53, 642, DateTimeKind.Local).AddTicks(3600),
                            PublishDate = new DateTime(2024, 4, 27, 0, 39, 53, 642, DateTimeKind.Local).AddTicks(3610),
                            Title = "Make the world better"
                        },
                        new
                        {
                            Id = 2,
                            Body = "Test",
                            Description = "Test",
                            ModifiedDate = new DateTime(2024, 4, 27, 0, 39, 53, 642, DateTimeKind.Local).AddTicks(3610),
                            PublishDate = new DateTime(2024, 4, 27, 0, 39, 53, 642, DateTimeKind.Local).AddTicks(3610),
                            Title = "AI take over the world"
                        });
                });

            modelBuilder.Entity("BasementBlog.Database.Models.WorkLog", b =>
                {
                    b.Property<string>("LoggedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("LoggedDate");

                    b.HasIndex("Id");

                    b.ToTable("WorkLogs");

                    b.HasData(
                        new
                        {
                            LoggedDate = "11/04/1996",
                            Body = "Test",
                            Id = 1,
                            ModifiedDate = new DateTime(2024, 4, 27, 0, 39, 53, 642, DateTimeKind.Local).AddTicks(3480)
                        });
                });

            modelBuilder.Entity("BasementBlog.Database.Models.Post", b =>
                {
                    b.HasOne("BasementBlog.Database.Models.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BasementBlog.Database.Models.Category", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
