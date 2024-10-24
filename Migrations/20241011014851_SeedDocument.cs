using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DMS_Hino.Migrations
{
    /// <inheritdoc />
    public partial class SeedDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "category1");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "category2");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc1");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc10");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc2");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc3");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc4");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc5");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc6");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc7");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc8");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc9");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "1", "Category1" },
                    { "2", "Category2" }
                });

            migrationBuilder.InsertData(
                table: "Divisions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "1", "Division1" },
                    { "2", "Division2" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DivisionId", "Name" },
                values: new object[,]
                {
                    { "1", "1", "Department1" },
                    { "2", "2", "Department2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DepartmentId", "DivisionId", "DocumentUpload", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { "1", "1", "1", null, "hashedpassword", "Admin", "creatorUser" },
                    { "2", "2", "2", null, "hashedpassword", "User", "modifierUser" }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "IsPublic", "Location", "ModifiedAt", "ModifiedById", "Name", "Number", "ReleasedDate", "Reminder", "Tag", "VersionName" },
                values: new object[] { "1", "1", new DateTime(2024, 10, 11, 8, 48, 49, 867, DateTimeKind.Local).AddTicks(2815), "1", "Item1", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "A-1", new DateTime(2024, 10, 11, 8, 48, 49, 867, DateTimeKind.Local).AddTicks(2827), "2", "Document1", "Doc001", new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Important", "v1.0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "category1", "Legal" },
                    { "category2", "Technical" }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "IsPublic", "Location", "ModifiedAt", "ModifiedById", "Name", "Number", "ReleasedDate", "Reminder", "Tag", "VersionName" },
                values: new object[,]
                {
                    { "doc1", "existing-category1", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user1", "Legal Agreement", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Locker A-1", null, null, "Service Agreement", "LA-2024-01", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Contract", "v1.0" },
                    { "doc10", "existing-category1", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user10", "Presentation", new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Locker J-10", null, null, "Project Presentation", "PR-2024-10", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Presentation", "v1.0" },
                    { "doc2", "existing-category2", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user2", "Project Report", new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Locker B-2", null, null, "Annual Report", "PR-2024-02", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, "Report", "v2.0" },
                    { "doc3", "existing-category3", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user3", "User Guide", null, false, "Locker C-3", null, null, "System User Guide", "UG-2024-03", new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Manual", "v3.0" },
                    { "doc4", "existing-category1", new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user4", "Policy Document", new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Locker D-4", null, null, "Company Policy", "PD-2024-04", new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, "Policy", "v1.1" },
                    { "doc5", "existing-category2", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user5", "Invoice", new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Locker E-5", null, null, "Invoice for Services", "IN-2024-05", new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Invoice", "v1.0" },
                    { "doc6", "existing-category3", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user6", "Audit Report", new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Locker F-6", null, null, "Internal Audit", "AR-2024-06", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, "Audit", "v1.0" },
                    { "doc7", "existing-category1", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user7", "Technical Specification", new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Locker G-7", null, null, "Product Specification", "TS-2024-07", new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "Specification", "v2.1" },
                    { "doc8", "existing-category2", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user8", "Memo", new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Locker H-8", null, null, "Internal Memo", "ME-2024-08", new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Memo", "v1.0" },
                    { "doc9", "existing-category3", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user9", "Financial Statement", null, true, "Locker I-9", null, null, "Quarterly Financials", "FS-2024-09", new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Finance", "v1.2" }
                });
        }
    }
}
