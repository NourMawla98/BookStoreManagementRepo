using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreManagement.Domain.Migrations
{
    /// <inheritdoc />
    public partial class fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PurchaseDate",
                table: "purchase",
                newName: "purchase_date");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "purchase",
                newName: "id");

            migrationBuilder.AlterColumn<decimal>(
                name: "book_price",
                table: "purchase",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "purchase_date",
                table: "purchase",
                newName: "PurchaseDate");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "purchase",
                newName: "PurchaseId");

            migrationBuilder.AlterColumn<double>(
                name: "book_price",
                table: "purchase",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }
    }
}
