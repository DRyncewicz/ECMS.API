using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertiesToSupplierOrderEntityAndRelationToSupplierContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrders_Messages_MessageId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrders_Suppliers_SupplierId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                schema: "ecms",
                table: "SupplierOrders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTimeUtc",
                schema: "ecms",
                table: "SupplierOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EditDateTimeUtc",
                schema: "ecms",
                table: "SupplierOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierContactId",
                schema: "ecms",
                table: "SupplierOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_SupplierContactId",
                schema: "ecms",
                table: "SupplierOrders",
                column: "SupplierContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrders_Messages_MessageId",
                schema: "ecms",
                table: "SupplierOrders",
                column: "MessageId",
                principalSchema: "ecms",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrders_SupplierContacts_SupplierContactId",
                schema: "ecms",
                table: "SupplierOrders",
                column: "SupplierContactId",
                principalSchema: "ecms",
                principalTable: "SupplierContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrders_Suppliers_SupplierId",
                schema: "ecms",
                table: "SupplierOrders",
                column: "SupplierId",
                principalSchema: "ecms",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrders_Messages_MessageId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrders_SupplierContacts_SupplierContactId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrders_Suppliers_SupplierId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropIndex(
                name: "IX_SupplierOrders_SupplierContactId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropColumn(
                name: "CreateDateTimeUtc",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropColumn(
                name: "EditDateTimeUtc",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropColumn(
                name: "SupplierContactId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeliveryDate",
                schema: "ecms",
                table: "SupplierOrders",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrders_Messages_MessageId",
                schema: "ecms",
                table: "SupplierOrders",
                column: "MessageId",
                principalSchema: "ecms",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrders_Suppliers_SupplierId",
                schema: "ecms",
                table: "SupplierOrders",
                column: "SupplierId",
                principalSchema: "ecms",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
