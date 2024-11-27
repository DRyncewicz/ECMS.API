using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedPropToSupplierEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "ecms",
                table: "Suppliers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "ecms",
                table: "Suppliers");
        }
    }
}
