using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangingTheRelationshipBetweenSupplierOrdersAndInvoicesAndFixedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrders_Invoices_InvoiceId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropIndex(
                name: "IX_SupplierOrders_InvoiceId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.AddColumn<int>(
                name: "SupplierOrderId",
                schema: "ecms",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SupplierOrderId",
                schema: "ecms",
                table: "Invoices",
                column: "SupplierOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_SupplierOrders_SupplierOrderId",
                schema: "ecms",
                table: "Invoices",
                column: "SupplierOrderId",
                principalSchema: "ecms",
                principalTable: "SupplierOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_SupplierOrders_SupplierOrderId",
                schema: "ecms",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_SupplierOrderId",
                schema: "ecms",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SupplierOrderId",
                schema: "ecms",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                schema: "ecms",
                table: "SupplierOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_InvoiceId",
                schema: "ecms",
                table: "SupplierOrders",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrders_Invoices_InvoiceId",
                schema: "ecms",
                table: "SupplierOrders",
                column: "InvoiceId",
                principalSchema: "ecms",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
