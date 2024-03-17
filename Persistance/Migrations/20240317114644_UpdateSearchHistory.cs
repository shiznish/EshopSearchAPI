using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSearchHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SearchHistory_Customers_CustomerId",
                table: "SearchHistory");

            migrationBuilder.DropIndex(
                name: "IX_SearchHistory_CustomerId",
                table: "SearchHistory");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "SearchHistory");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SearchHistory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SearchHistory_UserId",
                table: "SearchHistory",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SearchHistory_UserId",
                table: "SearchHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SearchHistory");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "SearchHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SearchHistory_CustomerId",
                table: "SearchHistory",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SearchHistory_Customers_CustomerId",
                table: "SearchHistory",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
