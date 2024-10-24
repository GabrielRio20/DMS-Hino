using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS_Hino.Migrations
{
    /// <inheritdoc />
    public partial class createdby_nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Documents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Documents",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "IsPublic", "Location", "ModifiedAt", "ModifiedById", "Name", "Number", "ReleasedDate", "Reminder", "Tag", "VersionName" },
                values: new object[] { "1", "1", new DateTime(2024, 10, 11, 8, 48, 49, 867, DateTimeKind.Local).AddTicks(2815), "1", "Item1", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "A-1", new DateTime(2024, 10, 11, 8, 48, 49, 867, DateTimeKind.Local).AddTicks(2827), "2", "Document1", "Doc001", new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Important", "v1.0" });
        }
    }
}
