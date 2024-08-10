﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240330044916_addIsDeleteCollumn")]
    partial class addIsDeleteCollumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            ModifiedDate = new DateTime(2024, 3, 30, 12, 49, 13, 752, DateTimeKind.Local).AddTicks(4177),
                            PublishDate = new DateTime(2024, 3, 30, 12, 49, 13, 752, DateTimeKind.Local).AddTicks(4189),
                            Title = "Make the world better"
                        },
                        new
                        {
                            Id = 2,
                            Body = "Test",
                            Description = "Test",
                            ModifiedDate = new DateTime(2024, 3, 30, 12, 49, 13, 752, DateTimeKind.Local).AddTicks(4190),
                            PublishDate = new DateTime(2024, 3, 30, 12, 49, 13, 752, DateTimeKind.Local).AddTicks(4191),
                            Title = "AI take over the world"
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