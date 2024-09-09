using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntity_CategoryEntity_CategoryId",
                schema: "ecms",
                table: "ProductEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistoryEntity_ProductEntity_ProductId",
                schema: "ecms",
                table: "ProductHistoryEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterialEntity_ProductEntity_ProductId",
                schema: "ecms",
                table: "ProductMaterialEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantEntity_ProductEntity_ProductId",
                schema: "ecms",
                table: "ProductVariantEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantHistoryEntity_ProductVariantEntity_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantHistoryEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantHistoryEntity",
                schema: "ecms",
                table: "ProductVariantHistoryEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantEntity",
                schema: "ecms",
                table: "ProductVariantEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductHistoryEntity",
                schema: "ecms",
                table: "ProductHistoryEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductEntity",
                schema: "ecms",
                table: "ProductEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEntity",
                schema: "ecms",
                table: "CategoryEntity");

            migrationBuilder.RenameTable(
                name: "ProductVariantHistoryEntity",
                schema: "ecms",
                newName: "ProductVariantHistories",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "ProductVariantEntity",
                schema: "ecms",
                newName: "ProductVariants",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "ProductHistoryEntity",
                schema: "ecms",
                newName: "ProductHistories",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "ProductEntity",
                schema: "ecms",
                newName: "Products",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "CategoryEntity",
                schema: "ecms",
                newName: "Categoires",
                newSchema: "ecms");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantHistoryEntity_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantHistories",
                newName: "IX_ProductVariantHistories_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantEntity_ProductId",
                schema: "ecms",
                table: "ProductVariants",
                newName: "IX_ProductVariants_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductHistoryEntity_ProductId",
                schema: "ecms",
                table: "ProductHistories",
                newName: "IX_ProductHistories_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductEntity_CategoryId",
                schema: "ecms",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantHistories",
                schema: "ecms",
                table: "ProductVariantHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariants",
                schema: "ecms",
                table: "ProductVariants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductHistories",
                schema: "ecms",
                table: "ProductHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "ecms",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categoires",
                schema: "ecms",
                table: "Categoires",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistories_Products_ProductId",
                schema: "ecms",
                table: "ProductHistories",
                column: "ProductId",
                principalSchema: "ecms",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterialEntity_Products_ProductId",
                schema: "ecms",
                table: "ProductMaterialEntity",
                column: "ProductId",
                principalSchema: "ecms",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categoires_CategoryId",
                schema: "ecms",
                table: "Products",
                column: "CategoryId",
                principalSchema: "ecms",
                principalTable: "Categoires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantHistories_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantHistories",
                column: "ProductVariantId",
                principalSchema: "ecms",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                schema: "ecms",
                table: "ProductVariants",
                column: "ProductId",
                principalSchema: "ecms",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistories_Products_ProductId",
                schema: "ecms",
                table: "ProductHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterialEntity_Products_ProductId",
                schema: "ecms",
                table: "ProductMaterialEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categoires_CategoryId",
                schema: "ecms",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantHistories_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                schema: "ecms",
                table: "ProductVariants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariants",
                schema: "ecms",
                table: "ProductVariants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantHistories",
                schema: "ecms",
                table: "ProductVariantHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "ecms",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductHistories",
                schema: "ecms",
                table: "ProductHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categoires",
                schema: "ecms",
                table: "Categoires");

            migrationBuilder.RenameTable(
                name: "ProductVariants",
                schema: "ecms",
                newName: "ProductVariantEntity",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "ProductVariantHistories",
                schema: "ecms",
                newName: "ProductVariantHistoryEntity",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "ecms",
                newName: "ProductEntity",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "ProductHistories",
                schema: "ecms",
                newName: "ProductHistoryEntity",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Categoires",
                schema: "ecms",
                newName: "CategoryEntity",
                newSchema: "ecms");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_ProductId",
                schema: "ecms",
                table: "ProductVariantEntity",
                newName: "IX_ProductVariantEntity_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantHistories_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantHistoryEntity",
                newName: "IX_ProductVariantHistoryEntity_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                schema: "ecms",
                table: "ProductEntity",
                newName: "IX_ProductEntity_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductHistories_ProductId",
                schema: "ecms",
                table: "ProductHistoryEntity",
                newName: "IX_ProductHistoryEntity_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantEntity",
                schema: "ecms",
                table: "ProductVariantEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantHistoryEntity",
                schema: "ecms",
                table: "ProductVariantHistoryEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductEntity",
                schema: "ecms",
                table: "ProductEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductHistoryEntity",
                schema: "ecms",
                table: "ProductHistoryEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEntity",
                schema: "ecms",
                table: "CategoryEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntity_CategoryEntity_CategoryId",
                schema: "ecms",
                table: "ProductEntity",
                column: "CategoryId",
                principalSchema: "ecms",
                principalTable: "CategoryEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistoryEntity_ProductEntity_ProductId",
                schema: "ecms",
                table: "ProductHistoryEntity",
                column: "ProductId",
                principalSchema: "ecms",
                principalTable: "ProductEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterialEntity_ProductEntity_ProductId",
                schema: "ecms",
                table: "ProductMaterialEntity",
                column: "ProductId",
                principalSchema: "ecms",
                principalTable: "ProductEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantEntity_ProductEntity_ProductId",
                schema: "ecms",
                table: "ProductVariantEntity",
                column: "ProductId",
                principalSchema: "ecms",
                principalTable: "ProductEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantHistoryEntity_ProductVariantEntity_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantHistoryEntity",
                column: "ProductVariantId",
                principalSchema: "ecms",
                principalTable: "ProductVariantEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
