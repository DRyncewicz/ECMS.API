using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationAndConfigurationOneToOneBetweenSupplierOrderAndMessageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                schema: "ecms",
                table: "SupplierOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_MessageId",
                schema: "ecms",
                table: "SupplierOrders",
                column: "MessageId",
                unique: true,
                filter: "[MessageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrders_Messages_MessageId",
                schema: "ecms",
                table: "SupplierOrders",
                column: "MessageId",
                principalSchema: "ecms",
                principalTable: "Messages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrders_Messages_MessageId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropIndex(
                name: "IX_SupplierOrders_MessageId",
                schema: "ecms",
                table: "SupplierOrders");

            migrationBuilder.DropColumn(
                name: "MessageId",
                schema: "ecms",
                table: "SupplierOrders");
        }
    }
}
