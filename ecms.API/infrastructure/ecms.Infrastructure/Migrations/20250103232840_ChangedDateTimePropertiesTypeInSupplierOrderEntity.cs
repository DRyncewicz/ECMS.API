using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDateTimePropertiesTypeInSupplierOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EditDateTimeUtc",
                schema: "ecms",
                table: "SupplierOrders",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDateTimeUtc",
                schema: "ecms",
                table: "SupplierOrders",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDateTimeUtc",
                schema: "ecms",
                table: "SupplierOrders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDateTimeUtc",
                schema: "ecms",
                table: "SupplierOrders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }
    }
}
