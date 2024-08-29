using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreManagement.Domain.Migrations
{
    /// <inheritdoc />
    public partial class aug29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_book_publisher_BookPublisherId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "PurchaseDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Purchases");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "purchase");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_BookPublisherId",
                table: "purchase",
                newName: "IX_purchase_BookPublisherId");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseId",
                table: "purchase",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "book_id",
                table: "purchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "book_price",
                table: "purchase",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "purchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_purchase",
                table: "purchase",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_book_id",
                table: "purchase",
                column: "book_id");

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_book_book_id",
                table: "purchase",
                column: "book_id",
                principalTable: "book",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_book_publisher_BookPublisherId",
                table: "purchase",
                column: "BookPublisherId",
                principalTable: "book_publisher",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchase_book_book_id",
                table: "purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_purchase_book_publisher_BookPublisherId",
                table: "purchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_purchase",
                table: "purchase");

            migrationBuilder.DropIndex(
                name: "IX_purchase_book_id",
                table: "purchase");

            migrationBuilder.DropColumn(
                name: "book_id",
                table: "purchase");

            migrationBuilder.DropColumn(
                name: "book_price",
                table: "purchase");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "purchase");

            migrationBuilder.RenameTable(
                name: "purchase",
                newName: "Purchases");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_BookPublisherId",
                table: "Purchases",
                newName: "IX_Purchases_BookPublisherId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseId",
                table: "Purchases",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Purchases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                column: "PurchaseId");

            migrationBuilder.CreateTable(
                name: "PurchaseDetails",
                columns: table => new
                {
                    PurchaseDetailId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PurchaseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
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
    }
}
