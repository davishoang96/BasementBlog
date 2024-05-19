using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Database.Migrations
{
    /// <inheritdoc />
    public partial class addIsDeleteCollumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Posts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsDeleted", "ModifiedDate", "PublishDate" },
                values: new object[] { null, new DateTime(2024, 3, 30, 12, 49, 13, 752, DateTimeKind.Local).AddTicks(4177), new DateTime(2024, 3, 30, 12, 49, 13, 752, DateTimeKind.Local).AddTicks(4189) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsDeleted", "ModifiedDate", "PublishDate" },
                values: new object[] { null, new DateTime(2024, 3, 30, 12, 49, 13, 752, DateTimeKind.Local).AddTicks(4190), new DateTime(2024, 3, 30, 12, 49, 13, 752, DateTimeKind.Local).AddTicks(4191) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 3, 12, 11, 41, 38, 316, DateTimeKind.Local).AddTicks(9017), new DateTime(2024, 3, 12, 11, 41, 38, 316, DateTimeKind.Local).AddTicks(9040) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 3, 12, 11, 41, 38, 316, DateTimeKind.Local).AddTicks(9043), new DateTime(2024, 3, 12, 11, 41, 38, 316, DateTimeKind.Local).AddTicks(9043) });
        }
    }
}
