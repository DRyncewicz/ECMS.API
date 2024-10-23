using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicializedAllergen_Invoice_Materials_Message_Orders_Products_Stocks_Suppliers_EntitiesAndTheirEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterialEntity_Products_ProductId",
                schema: "ecms",
                table: "ProductMaterialEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterialHistoryEntity_ProductMaterialEntity_ProductMaterialId",
                schema: "ecms",
                table: "ProductMaterialHistoryEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMaterialHistoryEntity",
                schema: "ecms",
                table: "ProductMaterialHistoryEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMaterialEntity",
                schema: "ecms",
                table: "ProductMaterialEntity");

            migrationBuilder.RenameTable(
                name: "ProductMaterialHistoryEntity",
                schema: "ecms",
                newName: "ProductMaterialHistory",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "ProductMaterialEntity",
                schema: "ecms",
                newName: "ProductMaterial",
                newSchema: "ecms");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterialHistoryEntity_ProductMaterialId",
                schema: "ecms",
                table: "ProductMaterialHistory",
                newName: "IX_ProductMaterialHistory_ProductMaterialId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "ecms",
                table: "ProductMaterial",
                newName: "ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterialEntity_ProductId",
                schema: "ecms",
                table: "ProductMaterial",
                newName: "IX_ProductMaterial_ProductVariantId");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUsedId",
                schema: "ecms",
                table: "ProductMaterialHistory",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDateTimeUtc",
                schema: "ecms",
                table: "ProductMaterialHistory",
                type: "datetimeoffset(7)",
                precision: 7,
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "ecms",
                table: "ProductMaterial",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProductEntityId",
                schema: "ecms",
                table: "ProductMaterial",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMaterialHistory",
                schema: "ecms",
                table: "ProductMaterialHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMaterial",
                schema: "ecms",
                table: "ProductMaterial",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Allergen",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PriceAmount = table.Column<double>(type: "float", nullable: false),
                    PriceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDateTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", precision: 7, nullable: false),
                    FileGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MinStockLevel = table.Column<double>(type: "float", nullable: false),
                    MaxStockLevel = table.Column<double>(type: "float", nullable: false),
                    ReorderLevel = table.Column<double>(type: "float", nullable: false),
                    FileGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SentDateTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", precision: 7, nullable: false),
                    MessageStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorCode = table.Column<int>(type: "int", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ErrorAttempts = table.Column<int>(type: "int", nullable: false),
                    CreateDateTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", precision: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", precision: 7, nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    TableNumber = table.Column<int>(type: "int", nullable: true),
                    PriceAmount = table.Column<double>(type: "float", nullable: false),
                    PriceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ecms",
                        principalTable: "Addresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ecms",
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplier_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ecms",
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariantAllergen",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllergenId = table.Column<int>(type: "int", nullable: false),
                    ProductVariantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariantAllergen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariantAllergen_Allergen_AllergenId",
                        column: x => x.AllergenId,
                        principalSchema: "ecms",
                        principalTable: "Allergen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariantAllergen_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalSchema: "ecms",
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialHistory",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MinStockLevel = table.Column<double>(type: "float", nullable: false),
                    MaxStockLevel = table.Column<double>(type: "float", nullable: false),
                    ReorderLevel = table.Column<double>(type: "float", nullable: false),
                    FileGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialHistory_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalSchema: "ecms",
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProductVariant",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductVariantId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProductVariant_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "ecms",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProductVariant_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalSchema: "ecms",
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockLevel",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    CreateDateTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", precision: 7, nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", precision: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockLevel_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalSchema: "ecms",
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockLevel_Stock_StockId",
                        column: x => x.StockId,
                        principalSchema: "ecms",
                        principalTable: "Stock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierContact",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsCommon = table.Column<bool>(type: "bit", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    RepresentativeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(22)", maxLength: 22, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierContact_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "ecms",
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierHistory",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreateDateTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", precision: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierHistory_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "ecms",
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrder",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierOrder_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "ecms",
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierOrder_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "ecms",
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrderMaterial",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierOrderId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    PriceAmount = table.Column<double>(type: "float", nullable: false),
                    PriceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOrderMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierOrderMaterial_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalSchema: "ecms",
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierOrderMaterial_SupplierOrder_SupplierOrderId",
                        column: x => x.SupplierOrderId,
                        principalSchema: "ecms",
                        principalTable: "SupplierOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTransaction",
                schema: "ecms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    StockLevelId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierOrderMaterialId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    BatchNumber = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    ExpiryDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", precision: 7, nullable: true),
                    CreateDateTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", precision: 7, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransaction_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "ecms",
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockTransaction_StockLevel_StockLevelId",
                        column: x => x.StockLevelId,
                        principalSchema: "ecms",
                        principalTable: "StockLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransaction_SupplierOrderMaterial_SupplierOrderMaterialId",
                        column: x => x.SupplierOrderMaterialId,
                        principalSchema: "ecms",
                        principalTable: "SupplierOrderMaterial",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterial_MaterialId",
                schema: "ecms",
                table: "ProductMaterial",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterial_ProductEntityId",
                schema: "ecms",
                table: "ProductMaterial",
                column: "ProductEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialHistory_MaterialId",
                schema: "ecms",
                table: "MaterialHistory",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_AddressId",
                schema: "ecms",
                table: "Order",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductVariant_OrderId",
                schema: "ecms",
                table: "OrderProductVariant",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductVariant_ProductVariantId",
                schema: "ecms",
                table: "OrderProductVariant",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantAllergen_AllergenId",
                schema: "ecms",
                table: "ProductVariantAllergen",
                column: "AllergenId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantAllergen_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantAllergen",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_AddressId",
                schema: "ecms",
                table: "Stock",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_StockLevel_MaterialId",
                schema: "ecms",
                table: "StockLevel",
                column: "MaterialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockLevel_StockId",
                schema: "ecms",
                table: "StockLevel",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_OrderId",
                schema: "ecms",
                table: "StockTransaction",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_StockLevelId",
                schema: "ecms",
                table: "StockTransaction",
                column: "StockLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_SupplierOrderMaterialId",
                schema: "ecms",
                table: "StockTransaction",
                column: "SupplierOrderMaterialId",
                unique: true,
                filter: "[SupplierOrderMaterialId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_AddressId",
                schema: "ecms",
                table: "Supplier",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContact_SupplierId",
                schema: "ecms",
                table: "SupplierContact",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierHistory_SupplierId",
                schema: "ecms",
                table: "SupplierHistory",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrder_InvoiceId",
                schema: "ecms",
                table: "SupplierOrder",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrder_SupplierId",
                schema: "ecms",
                table: "SupplierOrder",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrderMaterial_MaterialId",
                schema: "ecms",
                table: "SupplierOrderMaterial",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrderMaterial_SupplierOrderId",
                schema: "ecms",
                table: "SupplierOrderMaterial",
                column: "SupplierOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterial_Material_MaterialId",
                schema: "ecms",
                table: "ProductMaterial",
                column: "MaterialId",
                principalSchema: "ecms",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterial_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "ProductMaterial",
                column: "ProductVariantId",
                principalSchema: "ecms",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterial_Products_ProductEntityId",
                schema: "ecms",
                table: "ProductMaterial",
                column: "ProductEntityId",
                principalSchema: "ecms",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterialHistory_ProductMaterial_ProductMaterialId",
                schema: "ecms",
                table: "ProductMaterialHistory",
                column: "ProductMaterialId",
                principalSchema: "ecms",
                principalTable: "ProductMaterial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterial_Material_MaterialId",
                schema: "ecms",
                table: "ProductMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterial_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "ProductMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterial_Products_ProductEntityId",
                schema: "ecms",
                table: "ProductMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterialHistory_ProductMaterial_ProductMaterialId",
                schema: "ecms",
                table: "ProductMaterialHistory");

            migrationBuilder.DropTable(
                name: "MaterialHistory",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "Message",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "OrderProductVariant",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "ProductVariantAllergen",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "StockTransaction",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "SupplierContact",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "SupplierHistory",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "Allergen",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "StockLevel",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "SupplierOrderMaterial",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "Stock",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "Material",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "SupplierOrder",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "Invoice",
                schema: "ecms");

            migrationBuilder.DropTable(
                name: "Supplier",
                schema: "ecms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMaterialHistory",
                schema: "ecms",
                table: "ProductMaterialHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMaterial",
                schema: "ecms",
                table: "ProductMaterial");

            migrationBuilder.DropIndex(
                name: "IX_ProductMaterial_MaterialId",
                schema: "ecms",
                table: "ProductMaterial");

            migrationBuilder.DropIndex(
                name: "IX_ProductMaterial_ProductEntityId",
                schema: "ecms",
                table: "ProductMaterial");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "ecms",
                table: "ProductMaterial");

            migrationBuilder.DropColumn(
                name: "ProductEntityId",
                schema: "ecms",
                table: "ProductMaterial");

            migrationBuilder.RenameTable(
                name: "ProductMaterialHistory",
                schema: "ecms",
                newName: "ProductMaterialHistoryEntity",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "ProductMaterial",
                schema: "ecms",
                newName: "ProductMaterialEntity",
                newSchema: "ecms");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterialHistory_ProductMaterialId",
                schema: "ecms",
                table: "ProductMaterialHistoryEntity",
                newName: "IX_ProductMaterialHistoryEntity_ProductMaterialId");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                schema: "ecms",
                table: "ProductMaterialEntity",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterial_ProductVariantId",
                schema: "ecms",
                table: "ProductMaterialEntity",
                newName: "IX_ProductMaterialEntity_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUsedId",
                schema: "ecms",
                table: "ProductMaterialHistoryEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDateTimeUtc",
                schema: "ecms",
                table: "ProductMaterialHistoryEntity",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(7)",
                oldPrecision: 7);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMaterialHistoryEntity",
                schema: "ecms",
                table: "ProductMaterialHistoryEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMaterialEntity",
                schema: "ecms",
                table: "ProductMaterialEntity",
                column: "Id");

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
                name: "FK_ProductMaterialHistoryEntity_ProductMaterialEntity_ProductMaterialId",
                schema: "ecms",
                table: "ProductMaterialHistoryEntity",
                column: "ProductMaterialId",
                principalSchema: "ecms",
                principalTable: "ProductMaterialEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
