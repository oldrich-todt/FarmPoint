using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPoint.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFarmOwnerIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Farms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Farms_OwnerId",
                table: "Farms",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Farms_OwnerId",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Farms");
        }
    }
}
