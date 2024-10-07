using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS_Hino.Migrations
{
    /// <inheritdoc />
    public partial class NewAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "268c02d7-93ab-4691-897a-0ad514bf6d14");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "DivisionId", "DocumentUpload", "Password", "Role", "Username" },
                values: new object[] { "268c02d7-93ab-4691-897a-0ad514bf6d14", "department1", "division1", null, "AQAAAAIAAYagAAAAEBO92qqBSwi+Ft7FDEiuKZA7HifEvGtT5rm4anjqgV5SLE0nrZh8QlmOOmK+RBDjOw==", "Admin", "admin" });
        }
    }
}
