using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.SqlServer.Types;

#nullable disable

namespace ecms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicializedProductRelatedEntitesAndTheirEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ecms");

            migrationBuilder.CreateTable(
                name: "CategoryEntity",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HierarchyId = table.Column<SqlHierarchyId>(type: "hierarchyid", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FileGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductEntity",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Vat = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlcoholContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GtuCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductEntity_CategoryEntity_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "ecms",
                        principalTable: "CategoryEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductHistoryEntity",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Vat = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlcoholContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GtuCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDateTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", precision: 7, nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistoryEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductHistoryEntity_ProductEntity_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "ecms",
                        principalTable: "ProductEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMaterialEntity",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMaterialEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMaterialEntity_ProductEntity_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "ecms",
                        principalTable: "ProductEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariantEntity",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PriceAmount = table.Column<double>(type: "float", nullable: false),
                    PriceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariantEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariantEntity_ProductEntity_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "ecms",
                        principalTable: "ProductEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariantHistoryEntity",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductVariantId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PriceAmount = table.Column<double>(type: "float", nullable: false),
                    PriceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreateDateTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", precision: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariantHistoryEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariantHistoryEntity_ProductVariantEntity_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalSchema: "ecms",
                        principalTable: "ProductVariantEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntity_CategoryId",
                schema: "ecms",
                table: "ProductEntity",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistoryEntity_ProductId",
                schema: "ecms",
                table: "ProductHistoryEntity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterialEntity_ProductId",
                schema: "ecms",
                table: "ProductMaterialEntity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantEntity_ProductId",
                schema: "ecms",
                table: "ProductVariantEntity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantHistoryEntity_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantHistoryEntity",
                column: "ProductVariantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductHistoryEntity",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "ProductMaterialEntity",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "ProductVariantHistoryEntity",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "ProductVariantEntity",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "ProductEntity",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "CategoryEntity",
                schema: "ecms");
        }
    }
}
