using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS_Hino.Migrations
{
    /// <inheritdoc />
    public partial class SeedDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc1",
                columns: new[] { "CategoryId", "CreatedById" },
                values: new object[] { "existing-category1", "existing-user1" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc10",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "Name", "Number", "ReleasedDate", "Reminder", "Tag", "VersionName" },
                values: new object[] { "existing-category1", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user10", "Presentation", new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Project Presentation", "PR-2024-10", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Presentation", "v1.0" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc2",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "IsPublic", "ModifiedAt", "ModifiedById", "Name", "Number", "ReleasedDate", "Reminder", "VersionName" },
                values: new object[] { "existing-category2", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user2", "Project Report", new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, "Annual Report", "PR-2024-02", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, "v2.0" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc3",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "IsPublic", "Name", "Number", "ReleasedDate", "Tag", "VersionName" },
                values: new object[] { "existing-category3", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user3", "User Guide", false, "System User Guide", "UG-2024-03", new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manual", "v3.0" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc4",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "IsPublic", "Name", "Number", "ReleasedDate", "Reminder", "Tag", "VersionName" },
                values: new object[] { "existing-category1", new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user4", "Policy Document", new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Company Policy", "PD-2024-04", new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, "Policy", "v1.1" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc5",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "IsPublic", "Name", "Number", "ReleasedDate", "Reminder", "Tag" },
                values: new object[] { "existing-category2", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user5", "Invoice", new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Invoice for Services", "IN-2024-05", new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Invoice" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc6",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "Name", "Number", "ReleasedDate", "Reminder", "Tag", "VersionName" },
                values: new object[] { "existing-category3", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user6", "Audit Report", new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Internal Audit", "AR-2024-06", new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, "Audit", "v1.0" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc7",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "Name", "Number", "Reminder", "Tag", "VersionName" },
                values: new object[] { "existing-category1", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user7", "Technical Specification", new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Product Specification", "TS-2024-07", 15, "Specification", "v2.1" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc8",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "ModifiedAt", "ModifiedById", "Name", "Number", "ReleasedDate", "Reminder", "Tag", "VersionName" },
                values: new object[] { "existing-category2", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user8", "Memo", new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Internal Memo", "ME-2024-08", new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Memo", "v1.0" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc9",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "Name", "Number", "ReleasedDate", "Tag", "VersionName" },
                values: new object[] { "existing-category3", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "existing-user9", "Financial Statement", "Quarterly Financials", "FS-2024-09", new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finance", "v1.2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc1",
                columns: new[] { "CategoryId", "CreatedById" },
                values: new object[] { "category1", "user1" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc10",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "Name", "Number", "ReleasedDate", "Reminder", "Tag", "VersionName" },
                values: new object[] { "category2", new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2", "Marketing Plan", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Q3 Marketing Strategy", "MP-2024-11", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, "Plan", "v3.2" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc2",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "IsPublic", "ModifiedAt", "ModifiedById", "Name", "Number", "ReleasedDate", "Reminder", "VersionName" },
                values: new object[] { "category2", new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2", "Technical Report", null, false, new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1", "System Design Report", "TR-2024-02", new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "v2.3" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc3",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "IsPublic", "Name", "Number", "ReleasedDate", "Tag", "VersionName" },
                values: new object[] { "category1", new DateTime(2023, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1", "Financial Report", true, "Q1 Financials", "FR-2023-07", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finance", "v1.1" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc4",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "IsPublic", "Name", "Number", "ReleasedDate", "Reminder", "Tag", "VersionName" },
                values: new object[] { "category2", new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2", "Product Specification", null, false, "Product X Specification", "PS-2024-08", new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Product", "v2.0" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc5",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "IsPublic", "Name", "Number", "ReleasedDate", "Reminder", "Tag" },
                values: new object[] { "category1", new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1", "HR Policy", null, true, "Leave Policy", "HRP-2024-03", new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Policy" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc6",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "Name", "Number", "ReleasedDate", "Reminder", "Tag", "VersionName" },
                values: new object[] { "category2", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2", "Training Manual", null, "Software Training Guide", "TM-2024-09", new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Manual", "v3.1" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc7",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "Name", "Number", "Reminder", "Tag", "VersionName" },
                values: new object[] { "category1", new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1", "Meeting Minutes", null, "Board Meeting", "MM-2024-07", null, "Minutes", "v1.0" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc8",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "ExpDate", "ModifiedAt", "ModifiedById", "Name", "Number", "ReleasedDate", "Reminder", "Tag", "VersionName" },
                values: new object[] { "category2", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2", "Audit Report", null, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1", "Internal Audit 2024", "AR-2024-06", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Audit", "v2.0" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "doc9",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedById", "DocumentItem", "Name", "Number", "ReleasedDate", "Tag", "VersionName" },
                values: new object[] { "category1", new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1", "Legal Document", "NDA Agreement", "LD-2023-12", new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Agreement", "v1.0" });
        }
    }
}
