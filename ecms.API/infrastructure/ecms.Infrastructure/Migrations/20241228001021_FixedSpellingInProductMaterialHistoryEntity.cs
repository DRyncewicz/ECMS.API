using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedSpellingInProductMaterialHistoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "ecms",
                table: "ProductMaterialHistories",
                newName: "ProductVariantId");

            migrationBuilder.RenameColumn(
                name: "CreatorUsedId",
                schema: "ecms",
                table: "ProductMaterialHistories",
                newName: "CreatorUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                schema: "ecms",
                table: "ProductMaterialHistories",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "CreatorUserId",
                schema: "ecms",
                table: "ProductMaterialHistories",
                newName: "CreatorUsedId");
        }
    }
}
