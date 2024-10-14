using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicializedCategorySeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "ecms",
                table: "Categories",
                columns: new[] { "Id", "FileGuid", "HierarchyId", "Name" },
                values: new object[] { 1, null, Microsoft.SqlServer.Types.SqlHierarchyId.Parse("/"), "Main" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ecms",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}