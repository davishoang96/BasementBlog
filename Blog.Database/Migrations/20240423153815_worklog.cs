using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Database.Migrations
{
    /// <inheritdoc />
    public partial class worklog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    LoggedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkLogs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 4, 23, 23, 38, 15, 151, DateTimeKind.Local).AddTicks(2726), new DateTime(2024, 4, 23, 23, 38, 15, 151, DateTimeKind.Local).AddTicks(2738) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 4, 23, 23, 38, 15, 151, DateTimeKind.Local).AddTicks(2739), new DateTime(2024, 4, 23, 23, 38, 15, 151, DateTimeKind.Local).AddTicks(2740) });

            migrationBuilder.CreateIndex(
                name: "IX_WorkLogs_Id",
                table: "WorkLogs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkLogs");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 3, 30, 12, 49, 13, 752, DateTimeKind.Local).AddTicks(4177), new DateTime(2024, 3, 30, 12, 49, 13, 752, DateTimeKind.Local).AddTicks(4189) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedDate", "PublishDate" },
                values: new object[] { new DateTime(2024, 3, 30, 12, 49, 13, 752, DateTimeKind.Local).AddTicks(4190), new DateTime(2024, 3, 30, 12, 49, 13, 752, DateTimeKind.Local).AddTicks(4191) });
        }
    }
}
