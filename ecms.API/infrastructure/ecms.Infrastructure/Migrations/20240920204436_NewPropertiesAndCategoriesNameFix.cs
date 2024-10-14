using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewPropertiesAndCategoriesNameFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categoires_CategoryId",
                schema: "ecms",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categoires",
                schema: "ecms",
                table: "Categoires");

            migrationBuilder.RenameTable(
                name: "Categoires",
                schema: "ecms",
                newName: "Categories",
                newSchema: "ecms");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "ecms",
                table: "ProductVariants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "ecms",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                schema: "ecms",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductMaterialHistoryEntity",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductMaterialId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDateTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatorUsedId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMaterialHistoryEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMaterialHistoryEntity_ProductMaterialEntity_ProductMaterialId",
                        column: x => x.ProductMaterialId,
                        principalSchema: "ecms",
                        principalTable: "ProductMaterialEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterialHistoryEntity_ProductMaterialId",
                schema: "ecms",
                table: "ProductMaterialHistoryEntity",
                column: "ProductMaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "ecms",
                table: "Products",
                column: "CategoryId",
                principalSchema: "ecms",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "ecms",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductMaterialHistoryEntity",
                schema: "ecms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                schema: "ecms",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "ecms",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "ecms",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "ecms",
                newName: "Categoires",
                newSchema: "ecms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categoires",
                schema: "ecms",
                table: "Categoires",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categoires_CategoryId",
                schema: "ecms",
                table: "Products",
                column: "CategoryId",
                principalSchema: "ecms",
                principalTable: "Categoires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}