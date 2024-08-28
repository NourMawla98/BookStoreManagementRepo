using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreManagement.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_book_publisher_PublisherId",
                table: "book");

            migrationBuilder.DropIndex(
                name: "IX_book_PublisherId",
                table: "book");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "book");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "publisher",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "book_publisher",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "book_publisher",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "book",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "author",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    PurchaseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookPublisherId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_Purchase_book_publisher_BookPublisherId",
                        column: x => x.BookPublisherId,
                        principalTable: "book_publisher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_BookPublisherId",
                table: "Purchase",
                column: "BookPublisherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropColumn(
                name: "price",
                table: "book_publisher");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "publisher",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "book_publisher",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "book",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "book",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "author",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_book_PublisherId",
                table: "book",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_book_publisher_PublisherId",
                table: "book",
                column: "PublisherId",
                principalTable: "publisher",
                principalColumn: "id");
        }
    }
}
