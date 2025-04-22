using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputersAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddModelBuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_computers_components_components_component_id",
                table: "computers_components");

            migrationBuilder.DropForeignKey(
                name: "FK_computers_peripherals_peripherals_peripheral_id",
                table: "computers_peripherals");

            migrationBuilder.AddForeignKey(
                name: "FK_computers_components_components_component_id",
                table: "computers_components",
                column: "component_id",
                principalTable: "components",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_computers_peripherals_peripherals_peripheral_id",
                table: "computers_peripherals",
                column: "peripheral_id",
                principalTable: "peripherals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_computers_components_components_component_id",
                table: "computers_components");

            migrationBuilder.DropForeignKey(
                name: "FK_computers_peripherals_peripherals_peripheral_id",
                table: "computers_peripherals");

            migrationBuilder.AddForeignKey(
                name: "FK_computers_components_components_component_id",
                table: "computers_components",
                column: "component_id",
                principalTable: "components",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_computers_peripherals_peripherals_peripheral_id",
                table: "computers_peripherals",
                column: "peripheral_id",
                principalTable: "peripherals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
