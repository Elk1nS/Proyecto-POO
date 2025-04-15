using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputersAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories_components",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories_components", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories_peripherals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories_peripherals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "components",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    brand = table.Column<string>(type: "TEXT", nullable: false),
                    model = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_components", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "computers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    type = table.Column<string>(type: "TEXT", nullable: false),
                    brand = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_computers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "peripherals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    brand = table.Column<string>(type: "TEXT", nullable: false),
                    model = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_peripherals", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categories_components");

            migrationBuilder.DropTable(
                name: "categories_peripherals");

            migrationBuilder.DropTable(
                name: "components");

            migrationBuilder.DropTable(
                name: "computers");

            migrationBuilder.DropTable(
                name: "peripherals");
        }
    }
}
