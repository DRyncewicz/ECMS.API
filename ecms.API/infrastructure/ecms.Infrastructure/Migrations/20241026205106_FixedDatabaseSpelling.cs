using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedDatabaseSpelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialHistory_Material_MaterialId",
                schema: "ecms",
                table: "MaterialHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Addresses_AddressId",
                schema: "ecms",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProductVariant_Order_OrderId",
                schema: "ecms",
                table: "OrderProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProductVariant_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "OrderProductVariant");

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

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantAllergen_Allergen_AllergenId",
                schema: "ecms",
                table: "ProductVariantAllergen");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantAllergen_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantAllergen");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Addresses_AddressId",
                schema: "ecms",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_StockLevel_Material_MaterialId",
                schema: "ecms",
                table: "StockLevel");

            migrationBuilder.DropForeignKey(
                name: "FK_StockLevel_Stock_StockId",
                schema: "ecms",
                table: "StockLevel");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_Order_OrderId",
                schema: "ecms",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_StockLevel_StockLevelId",
                schema: "ecms",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_SupplierOrderMaterial_SupplierOrderMaterialId",
                schema: "ecms",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Addresses_AddressId",
                schema: "ecms",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierContact_Supplier_SupplierId",
                schema: "ecms",
                table: "SupplierContact");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierHistory_Supplier_SupplierId",
                schema: "ecms",
                table: "SupplierHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrder_Invoice_InvoiceId",
                schema: "ecms",
                table: "SupplierOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrder_Supplier_SupplierId",
                schema: "ecms",
                table: "SupplierOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrderMaterial_Material_MaterialId",
                schema: "ecms",
                table: "SupplierOrderMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrderMaterial_SupplierOrder_SupplierOrderId",
                schema: "ecms",
                table: "SupplierOrderMaterial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierOrderMaterial",
                schema: "ecms",
                table: "SupplierOrderMaterial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierOrder",
                schema: "ecms",
                table: "SupplierOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierHistory",
                schema: "ecms",
                table: "SupplierHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierContact",
                schema: "ecms",
                table: "SupplierContact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                schema: "ecms",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockTransaction",
                schema: "ecms",
                table: "StockTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockLevel",
                schema: "ecms",
                table: "StockLevel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                schema: "ecms",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantAllergen",
                schema: "ecms",
                table: "ProductVariantAllergen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMaterialHistory",
                schema: "ecms",
                table: "ProductMaterialHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMaterial",
                schema: "ecms",
                table: "ProductMaterial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProductVariant",
                schema: "ecms",
                table: "OrderProductVariant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                schema: "ecms",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                schema: "ecms",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialHistory",
                schema: "ecms",
                table: "MaterialHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Material",
                schema: "ecms",
                table: "Material");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                schema: "ecms",
                table: "Invoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allergen",
                schema: "ecms",
                table: "Allergen");

            migrationBuilder.RenameTable(
                name: "SupplierOrderMaterial",
                schema: "ecms",
                newName: "SupplierOrderMaterials",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "SupplierOrder",
                schema: "ecms",
                newName: "SupplierOrders",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "SupplierHistory",
                schema: "ecms",
                newName: "SupplierHistories",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "SupplierContact",
                schema: "ecms",
                newName: "SupplierContacts",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Supplier",
                schema: "ecms",
                newName: "Suppliers",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "StockTransaction",
                schema: "ecms",
                newName: "StockTransactions",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "StockLevel",
                schema: "ecms",
                newName: "StockLevels",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Stock",
                schema: "ecms",
                newName: "Stocks",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "ProductVariantAllergen",
                schema: "ecms",
                newName: "ProductVariantAllergens",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "ProductMaterialHistory",
                schema: "ecms",
                newName: "ProductMaterialHistories",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "ProductMaterial",
                schema: "ecms",
                newName: "ProductMaterials",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "OrderProductVariant",
                schema: "ecms",
                newName: "OrderProductVariants",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Order",
                schema: "ecms",
                newName: "Orders",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Message",
                schema: "ecms",
                newName: "Messages",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "MaterialHistory",
                schema: "ecms",
                newName: "MaterialHistories",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Material",
                schema: "ecms",
                newName: "Materials",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Invoice",
                schema: "ecms",
                newName: "Invoices",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Allergen",
                schema: "ecms",
                newName: "Allergens",
                newSchema: "ecms");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOrderMaterial_SupplierOrderId",
                schema: "ecms",
                table: "SupplierOrderMaterials",
                newName: "IX_SupplierOrderMaterials_SupplierOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOrderMaterial_MaterialId",
                schema: "ecms",
                table: "SupplierOrderMaterials",
                newName: "IX_SupplierOrderMaterials_MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOrder_SupplierId",
                schema: "ecms",
                table: "SupplierOrders",
                newName: "IX_SupplierOrders_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOrder_InvoiceId",
                schema: "ecms",
                table: "SupplierOrders",
                newName: "IX_SupplierOrders_InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierHistory_SupplierId",
                schema: "ecms",
                table: "SupplierHistories",
                newName: "IX_SupplierHistories_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierContact_SupplierId",
                schema: "ecms",
                table: "SupplierContacts",
                newName: "IX_SupplierContacts_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_AddressId",
                schema: "ecms",
                table: "Suppliers",
                newName: "IX_Suppliers_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransaction_SupplierOrderMaterialId",
                schema: "ecms",
                table: "StockTransactions",
                newName: "IX_StockTransactions_SupplierOrderMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransaction_StockLevelId",
                schema: "ecms",
                table: "StockTransactions",
                newName: "IX_StockTransactions_StockLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransaction_OrderId",
                schema: "ecms",
                table: "StockTransactions",
                newName: "IX_StockTransactions_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_StockLevel_StockId",
                schema: "ecms",
                table: "StockLevels",
                newName: "IX_StockLevels_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_StockLevel_MaterialId",
                schema: "ecms",
                table: "StockLevels",
                newName: "IX_StockLevels_MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_AddressId",
                schema: "ecms",
                table: "Stocks",
                newName: "IX_Stocks_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAllergen_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantAllergens",
                newName: "IX_ProductVariantAllergens_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAllergen_AllergenId",
                schema: "ecms",
                table: "ProductVariantAllergens",
                newName: "IX_ProductVariantAllergens_AllergenId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterialHistory_ProductMaterialId",
                schema: "ecms",
                table: "ProductMaterialHistories",
                newName: "IX_ProductMaterialHistories_ProductMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterial_ProductVariantId",
                schema: "ecms",
                table: "ProductMaterials",
                newName: "IX_ProductMaterials_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterial_ProductEntityId",
                schema: "ecms",
                table: "ProductMaterials",
                newName: "IX_ProductMaterials_ProductEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterial_MaterialId",
                schema: "ecms",
                table: "ProductMaterials",
                newName: "IX_ProductMaterials_MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProductVariant_ProductVariantId",
                schema: "ecms",
                table: "OrderProductVariants",
                newName: "IX_OrderProductVariants_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProductVariant_OrderId",
                schema: "ecms",
                table: "OrderProductVariants",
                newName: "IX_OrderProductVariants_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AddressId",
                schema: "ecms",
                table: "Orders",
                newName: "IX_Orders_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialHistory_MaterialId",
                schema: "ecms",
                table: "MaterialHistories",
                newName: "IX_MaterialHistories_MaterialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierOrderMaterials",
                schema: "ecms",
                table: "SupplierOrderMaterials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierOrders",
                schema: "ecms",
                table: "SupplierOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierHistories",
                schema: "ecms",
                table: "SupplierHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierContacts",
                schema: "ecms",
                table: "SupplierContacts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                schema: "ecms",
                table: "Suppliers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockTransactions",
                schema: "ecms",
                table: "StockTransactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockLevels",
                schema: "ecms",
                table: "StockLevels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                schema: "ecms",
                table: "Stocks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantAllergens",
                schema: "ecms",
                table: "ProductVariantAllergens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMaterialHistories",
                schema: "ecms",
                table: "ProductMaterialHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMaterials",
                schema: "ecms",
                table: "ProductMaterials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProductVariants",
                schema: "ecms",
                table: "OrderProductVariants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                schema: "ecms",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                schema: "ecms",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialHistories",
                schema: "ecms",
                table: "MaterialHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                schema: "ecms",
                table: "Materials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                schema: "ecms",
                table: "Invoices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allergens",
                schema: "ecms",
                table: "Allergens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialHistories_Materials_MaterialId",
                schema: "ecms",
                table: "MaterialHistories",
                column: "MaterialId",
                principalSchema: "ecms",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProductVariants_Orders_OrderId",
                schema: "ecms",
                table: "OrderProductVariants",
                column: "OrderId",
                principalSchema: "ecms",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProductVariants_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "OrderProductVariants",
                column: "ProductVariantId",
                principalSchema: "ecms",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_AddressId",
                schema: "ecms",
                table: "Orders",
                column: "AddressId",
                principalSchema: "ecms",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterialHistories_ProductMaterials_ProductMaterialId",
                schema: "ecms",
                table: "ProductMaterialHistories",
                column: "ProductMaterialId",
                principalSchema: "ecms",
                principalTable: "ProductMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterials_Materials_MaterialId",
                schema: "ecms",
                table: "ProductMaterials",
                column: "MaterialId",
                principalSchema: "ecms",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterials_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "ProductMaterials",
                column: "ProductVariantId",
                principalSchema: "ecms",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterials_Products_ProductEntityId",
                schema: "ecms",
                table: "ProductMaterials",
                column: "ProductEntityId",
                principalSchema: "ecms",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantAllergens_Allergens_AllergenId",
                schema: "ecms",
                table: "ProductVariantAllergens",
                column: "AllergenId",
                principalSchema: "ecms",
                principalTable: "Allergens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantAllergens_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantAllergens",
                column: "ProductVariantId",
                principalSchema: "ecms",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockLevels_Materials_MaterialId",
                schema: "ecms",
                table: "StockLevels",
                column: "MaterialId",
                principalSchema: "ecms",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockLevels_Stocks_StockId",
                schema: "ecms",
                table: "StockLevels",
                column: "StockId",
                principalSchema: "ecms",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Addresses_AddressId",
                schema: "ecms",
                table: "Stocks",
                column: "AddressId",
                principalSchema: "ecms",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Orders_OrderId",
                schema: "ecms",
                table: "StockTransactions",
                column: "OrderId",
                principalSchema: "ecms",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_StockLevels_StockLevelId",
                schema: "ecms",
                table: "StockTransactions",
                column: "StockLevelId",
                principalSchema: "ecms",
                principalTable: "StockLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_SupplierOrderMaterials_SupplierOrderMaterialId",
                schema: "ecms",
                table: "StockTransactions",
                column: "SupplierOrderMaterialId",
                principalSchema: "ecms",
                principalTable: "SupplierOrderMaterials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierContacts_Suppliers_SupplierId",
                schema: "ecms",
                table: "SupplierContacts",
                column: "SupplierId",
                principalSchema: "ecms",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierHistories_Suppliers_SupplierId",
                schema: "ecms",
                table: "SupplierHistories",
                column: "SupplierId",
                principalSchema: "ecms",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrderMaterials_Materials_MaterialId",
                schema: "ecms",
                table: "SupplierOrderMaterials",
                column: "MaterialId",
                principalSchema: "ecms",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrderMaterials_SupplierOrders_SupplierOrderId",
                schema: "ecms",
                table: "SupplierOrderMaterials",
                column: "SupplierOrderId",
                principalSchema: "ecms",
                principalTable: "SupplierOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrders_Invoices_InvoiceId",
                schema: "ecms",
                table: "SupplierOrders",
                column: "InvoiceId",
                principalSchema: "ecms",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrders_Suppliers_SupplierId",
                schema: "ecms",
                table: "SupplierOrders",
                column: "SupplierId",
                principalSchema: "ecms",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Addresses_AddressId",
                schema: "ecms",
                table: "Suppliers",
                column: "AddressId",
                principalSchema: "ecms",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialHistories_Materials_MaterialId",
                schema: "ecms",
                table: "MaterialHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProductVariants_Orders_OrderId",
                schema: "ecms",
                table: "OrderProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProductVariants_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "OrderProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_AddressId",
                schema: "ecms",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterialHistories_ProductMaterials_ProductMaterialId",
                schema: "ecms",
                table: "ProductMaterialHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterials_Materials_MaterialId",
                schema: "ecms",
                table: "ProductMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterials_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "ProductMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterials_Products_ProductEntityId",
                schema: "ecms",
                table: "ProductMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantAllergens_Allergens_AllergenId",
                schema: "ecms",
                table: "ProductVariantAllergens");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantAllergens_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantAllergens");

            migrationBuilder.DropForeignKey(
                name: "FK_StockLevels_Materials_MaterialId",
                schema: "ecms",
                table: "StockLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_StockLevels_Stocks_StockId",
                schema: "ecms",
                table: "StockLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Addresses_AddressId",
                schema: "ecms",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Orders_OrderId",
                schema: "ecms",
                table: "StockTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_StockLevels_StockLevelId",
                schema: "ecms",
                table: "StockTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_SupplierOrderMaterials_SupplierOrderMaterialId",
                schema: "ecms",
                table: "StockTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierContacts_Suppliers_SupplierId",
                schema: "ecms",
                table: "SupplierContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierHistories_Suppliers_SupplierId",
                schema: "ecms",
                table: "SupplierHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrderMaterials_Materials_MaterialId",
                schema: "ecms",
                table: "SupplierOrderMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrderMaterials_SupplierOrders_SupplierOrderId",
                schema: "ecms",
                table: "SupplierOrderMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrders_Invoices_InvoiceId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrders_Suppliers_SupplierId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Addresses_AddressId",
                schema: "ecms",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                schema: "ecms",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierOrders",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierOrderMaterials",
                schema: "ecms",
                table: "SupplierOrderMaterials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierHistories",
                schema: "ecms",
                table: "SupplierHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierContacts",
                schema: "ecms",
                table: "SupplierContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockTransactions",
                schema: "ecms",
                table: "StockTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                schema: "ecms",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockLevels",
                schema: "ecms",
                table: "StockLevels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantAllergens",
                schema: "ecms",
                table: "ProductVariantAllergens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMaterials",
                schema: "ecms",
                table: "ProductMaterials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMaterialHistories",
                schema: "ecms",
                table: "ProductMaterialHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                schema: "ecms",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProductVariants",
                schema: "ecms",
                table: "OrderProductVariants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                schema: "ecms",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                schema: "ecms",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialHistories",
                schema: "ecms",
                table: "MaterialHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                schema: "ecms",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allergens",
                schema: "ecms",
                table: "Allergens");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                schema: "ecms",
                newName: "Supplier",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "SupplierOrders",
                schema: "ecms",
                newName: "SupplierOrder",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "SupplierOrderMaterials",
                schema: "ecms",
                newName: "SupplierOrderMaterial",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "SupplierHistories",
                schema: "ecms",
                newName: "SupplierHistory",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "SupplierContacts",
                schema: "ecms",
                newName: "SupplierContact",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "StockTransactions",
                schema: "ecms",
                newName: "StockTransaction",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Stocks",
                schema: "ecms",
                newName: "Stock",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "StockLevels",
                schema: "ecms",
                newName: "StockLevel",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "ProductVariantAllergens",
                schema: "ecms",
                newName: "ProductVariantAllergen",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "ProductMaterials",
                schema: "ecms",
                newName: "ProductMaterial",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "ProductMaterialHistories",
                schema: "ecms",
                newName: "ProductMaterialHistory",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "ecms",
                newName: "Order",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "OrderProductVariants",
                schema: "ecms",
                newName: "OrderProductVariant",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Messages",
                schema: "ecms",
                newName: "Message",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Materials",
                schema: "ecms",
                newName: "Material",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "MaterialHistories",
                schema: "ecms",
                newName: "MaterialHistory",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Invoices",
                schema: "ecms",
                newName: "Invoice",
                newSchema: "ecms");

            migrationBuilder.RenameTable(
                name: "Allergens",
                schema: "ecms",
                newName: "Allergen",
                newSchema: "ecms");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_AddressId",
                schema: "ecms",
                table: "Supplier",
                newName: "IX_Supplier_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOrders_SupplierId",
                schema: "ecms",
                table: "SupplierOrder",
                newName: "IX_SupplierOrder_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOrders_InvoiceId",
                schema: "ecms",
                table: "SupplierOrder",
                newName: "IX_SupplierOrder_InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOrderMaterials_SupplierOrderId",
                schema: "ecms",
                table: "SupplierOrderMaterial",
                newName: "IX_SupplierOrderMaterial_SupplierOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierOrderMaterials_MaterialId",
                schema: "ecms",
                table: "SupplierOrderMaterial",
                newName: "IX_SupplierOrderMaterial_MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierHistories_SupplierId",
                schema: "ecms",
                table: "SupplierHistory",
                newName: "IX_SupplierHistory_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierContacts_SupplierId",
                schema: "ecms",
                table: "SupplierContact",
                newName: "IX_SupplierContact_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransactions_SupplierOrderMaterialId",
                schema: "ecms",
                table: "StockTransaction",
                newName: "IX_StockTransaction_SupplierOrderMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransactions_StockLevelId",
                schema: "ecms",
                table: "StockTransaction",
                newName: "IX_StockTransaction_StockLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransactions_OrderId",
                schema: "ecms",
                table: "StockTransaction",
                newName: "IX_StockTransaction_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_AddressId",
                schema: "ecms",
                table: "Stock",
                newName: "IX_Stock_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_StockLevels_StockId",
                schema: "ecms",
                table: "StockLevel",
                newName: "IX_StockLevel_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_StockLevels_MaterialId",
                schema: "ecms",
                table: "StockLevel",
                newName: "IX_StockLevel_MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAllergens_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantAllergen",
                newName: "IX_ProductVariantAllergen_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAllergens_AllergenId",
                schema: "ecms",
                table: "ProductVariantAllergen",
                newName: "IX_ProductVariantAllergen_AllergenId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterials_ProductVariantId",
                schema: "ecms",
                table: "ProductMaterial",
                newName: "IX_ProductMaterial_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterials_ProductEntityId",
                schema: "ecms",
                table: "ProductMaterial",
                newName: "IX_ProductMaterial_ProductEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterials_MaterialId",
                schema: "ecms",
                table: "ProductMaterial",
                newName: "IX_ProductMaterial_MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMaterialHistories_ProductMaterialId",
                schema: "ecms",
                table: "ProductMaterialHistory",
                newName: "IX_ProductMaterialHistory_ProductMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AddressId",
                schema: "ecms",
                table: "Order",
                newName: "IX_Order_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProductVariants_ProductVariantId",
                schema: "ecms",
                table: "OrderProductVariant",
                newName: "IX_OrderProductVariant_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProductVariants_OrderId",
                schema: "ecms",
                table: "OrderProductVariant",
                newName: "IX_OrderProductVariant_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialHistories_MaterialId",
                schema: "ecms",
                table: "MaterialHistory",
                newName: "IX_MaterialHistory_MaterialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                schema: "ecms",
                table: "Supplier",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierOrder",
                schema: "ecms",
                table: "SupplierOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierOrderMaterial",
                schema: "ecms",
                table: "SupplierOrderMaterial",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierHistory",
                schema: "ecms",
                table: "SupplierHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierContact",
                schema: "ecms",
                table: "SupplierContact",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockTransaction",
                schema: "ecms",
                table: "StockTransaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                schema: "ecms",
                table: "Stock",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockLevel",
                schema: "ecms",
                table: "StockLevel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantAllergen",
                schema: "ecms",
                table: "ProductVariantAllergen",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMaterial",
                schema: "ecms",
                table: "ProductMaterial",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMaterialHistory",
                schema: "ecms",
                table: "ProductMaterialHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                schema: "ecms",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProductVariant",
                schema: "ecms",
                table: "OrderProductVariant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                schema: "ecms",
                table: "Message",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Material",
                schema: "ecms",
                table: "Material",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialHistory",
                schema: "ecms",
                table: "MaterialHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                schema: "ecms",
                table: "Invoice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allergen",
                schema: "ecms",
                table: "Allergen",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialHistory_Material_MaterialId",
                schema: "ecms",
                table: "MaterialHistory",
                column: "MaterialId",
                principalSchema: "ecms",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Addresses_AddressId",
                schema: "ecms",
                table: "Order",
                column: "AddressId",
                principalSchema: "ecms",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProductVariant_Order_OrderId",
                schema: "ecms",
                table: "OrderProductVariant",
                column: "OrderId",
                principalSchema: "ecms",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProductVariant_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "OrderProductVariant",
                column: "ProductVariantId",
                principalSchema: "ecms",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantAllergen_Allergen_AllergenId",
                schema: "ecms",
                table: "ProductVariantAllergen",
                column: "AllergenId",
                principalSchema: "ecms",
                principalTable: "Allergen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantAllergen_ProductVariants_ProductVariantId",
                schema: "ecms",
                table: "ProductVariantAllergen",
                column: "ProductVariantId",
                principalSchema: "ecms",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Addresses_AddressId",
                schema: "ecms",
                table: "Stock",
                column: "AddressId",
                principalSchema: "ecms",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockLevel_Material_MaterialId",
                schema: "ecms",
                table: "StockLevel",
                column: "MaterialId",
                principalSchema: "ecms",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockLevel_Stock_StockId",
                schema: "ecms",
                table: "StockLevel",
                column: "StockId",
                principalSchema: "ecms",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_Order_OrderId",
                schema: "ecms",
                table: "StockTransaction",
                column: "OrderId",
                principalSchema: "ecms",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_StockLevel_StockLevelId",
                schema: "ecms",
                table: "StockTransaction",
                column: "StockLevelId",
                principalSchema: "ecms",
                principalTable: "StockLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_SupplierOrderMaterial_SupplierOrderMaterialId",
                schema: "ecms",
                table: "StockTransaction",
                column: "SupplierOrderMaterialId",
                principalSchema: "ecms",
                principalTable: "SupplierOrderMaterial",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Addresses_AddressId",
                schema: "ecms",
                table: "Supplier",
                column: "AddressId",
                principalSchema: "ecms",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierContact_Supplier_SupplierId",
                schema: "ecms",
                table: "SupplierContact",
                column: "SupplierId",
                principalSchema: "ecms",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierHistory_Supplier_SupplierId",
                schema: "ecms",
                table: "SupplierHistory",
                column: "SupplierId",
                principalSchema: "ecms",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrder_Invoice_InvoiceId",
                schema: "ecms",
                table: "SupplierOrder",
                column: "InvoiceId",
                principalSchema: "ecms",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrder_Supplier_SupplierId",
                schema: "ecms",
                table: "SupplierOrder",
                column: "SupplierId",
                principalSchema: "ecms",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrderMaterial_Material_MaterialId",
                schema: "ecms",
                table: "SupplierOrderMaterial",
                column: "MaterialId",
                principalSchema: "ecms",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrderMaterial_SupplierOrder_SupplierOrderId",
                schema: "ecms",
                table: "SupplierOrderMaterial",
                column: "SupplierOrderId",
                principalSchema: "ecms",
                principalTable: "SupplierOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
