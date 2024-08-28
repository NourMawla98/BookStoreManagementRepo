using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreManagement.Domain.Migrations
{
    /// <inheritdoc />
    public partial class migo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_book_publisher_BookPublisherId",
                table: "Purchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchase",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Purchase");

            migrationBuilder.RenameTable(
                name: "Purchase",
                newName: "Purchases");

            migrationBuilder.RenameIndex(
                name: "IX_Purchase_BookPublisherId",
                table: "Purchases",
                newName: "IX_Purchases_BookPublisherId");

            migrationBuilder.AlterColumn<int>(
                name: "BookPublisherId",
                table: "Purchases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                column: "PurchaseId");

            migrationBuilder.CreateTable(
                name: "PurchaseDetails",
                columns: table => new
                {
                    PurchaseDetailId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetails", x => x.PurchaseDetailId);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_PurchaseId",
                table: "PurchaseDetails",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_book_publisher_BookPublisherId",
                table: "Purchases",
                column: "BookPublisherId",
                principalTable: "book_publisher",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_book_publisher_BookPublisherId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "PurchaseDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "Purchase");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_BookPublisherId",
                table: "Purchase",
                newName: "IX_Purchase_BookPublisherId");

            migrationBuilder.AlterColumn<int>(
                name: "BookPublisherId",
                table: "Purchase",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Purchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchase",
                table: "Purchase",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_book_publisher_BookPublisherId",
                table: "Purchase",
                column: "BookPublisherId",
                principalTable: "book_publisher",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
