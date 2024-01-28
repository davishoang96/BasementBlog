using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasementBlog.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "Id", "Body", "ModifiedDate", "PublishDate", "Title" },
                values: new object[] { 1, "Test", new DateTime(2024, 1, 26, 0, 41, 46, 647, DateTimeKind.Local).AddTicks(2334), new DateTime(2024, 1, 26, 0, 41, 46, 647, DateTimeKind.Local).AddTicks(2343), "Make the world better" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
