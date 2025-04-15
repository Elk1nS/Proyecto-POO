using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputersAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTableRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "category_peripheral_id",
                table: "peripherals",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "category_component_id",
                table: "components",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "computers_components",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    computer_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    component_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_computers_components", x => x.id);
                    table.ForeignKey(
                        name: "FK_computers_components_components_component_id",
                        column: x => x.component_id,
                        principalTable: "components",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_computers_components_computers_computer_id",
                        column: x => x.computer_id,
                        principalTable: "computers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "computers_peripherals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    computer_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    peripheral_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_computers_peripherals", x => x.id);
                    table.ForeignKey(
                        name: "FK_computers_peripherals_computers_computer_id",
                        column: x => x.computer_id,
                        principalTable: "computers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_computers_peripherals_peripherals_peripheral_id",
                        column: x => x.peripheral_id,
                        principalTable: "peripherals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_peripherals_category_peripheral_id",
                table: "peripherals",
                column: "category_peripheral_id");

            migrationBuilder.CreateIndex(
                name: "IX_components_category_component_id",
                table: "components",
                column: "category_component_id");

            migrationBuilder.CreateIndex(
                name: "IX_computers_components_component_id",
                table: "computers_components",
                column: "component_id");

            migrationBuilder.CreateIndex(
                name: "IX_computers_components_computer_id",
                table: "computers_components",
                column: "computer_id");

            migrationBuilder.CreateIndex(
                name: "IX_computers_peripherals_computer_id",
                table: "computers_peripherals",
                column: "computer_id");

            migrationBuilder.CreateIndex(
                name: "IX_computers_peripherals_peripheral_id",
                table: "computers_peripherals",
                column: "peripheral_id");

            migrationBuilder.AddForeignKey(
                name: "FK_components_categories_components_category_component_id",
                table: "components",
                column: "category_component_id",
                principalTable: "categories_components",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_peripherals_categories_peripherals_category_peripheral_id",
                table: "peripherals",
                column: "category_peripheral_id",
                principalTable: "categories_peripherals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_components_categories_components_category_component_id",
                table: "components");

            migrationBuilder.DropForeignKey(
                name: "FK_peripherals_categories_peripherals_category_peripheral_id",
                table: "peripherals");

            migrationBuilder.DropTable(
                name: "computers_components");

            migrationBuilder.DropTable(
                name: "computers_peripherals");

            migrationBuilder.DropIndex(
                name: "IX_peripherals_category_peripheral_id",
                table: "peripherals");

            migrationBuilder.DropIndex(
                name: "IX_components_category_component_id",
                table: "components");

            migrationBuilder.DropColumn(
                name: "category_peripheral_id",
                table: "peripherals");

            migrationBuilder.DropColumn(
                name: "category_component_id",
                table: "components");
        }
    }
}
