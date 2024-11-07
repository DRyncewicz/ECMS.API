using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserIdFromProductEntityAndAddUserIdAndDateToMaterialHistoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "ecms",
                table: "Products");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreateDateTimeUtc",
                schema: "ecms",
                table: "MaterialHistories",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                schema: "ecms",
                table: "MaterialHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDateTimeUtc",
                schema: "ecms",
                table: "MaterialHistories");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "ecms",
                table: "MaterialHistories");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "ecms",
                table: "Products",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }
    }
}
