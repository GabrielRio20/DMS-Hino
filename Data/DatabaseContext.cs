using DMS_Hino.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DMS_Hino.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Models.Document> Documents { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DocumentRelation> DocumentRelations { get; set; }
        public DbSet<DocumentSharing> DocumentSharings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<User>()
                .HasOne(u => u.Division)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DivisionId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Models.Document>()
                .HasOne(d => d.CreatedBy)
                .WithMany(u => u.CreatedDocuments)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Models.Document>()
                .HasOne(d => d.ModifiedBy)
                .WithMany(u => u.ModifiedDocuments)
                .HasForeignKey(d => d.ModifiedById)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Models.Document>()
                .HasOne(d => d.Category)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.CategoryId);

            modelBuilder.Entity<DocumentRelation>()
                .HasOne(dr => dr.Document)
                .WithMany(d => d.RelatedDocuments)
                .HasForeignKey(dr => dr.DocumentId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<DocumentRelation>()
                .HasOne(dr => dr.RelatedDocument)
                .WithMany()
                .HasForeignKey(dr => dr.RelationDocumentId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<DocumentSharing>()
                .HasOne(ds => ds.Document)
                .WithMany(d => d.SharedDocuments)
                .HasForeignKey(ds => ds.DocumentId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<DocumentSharing>()
                .HasOne(ds => ds.User)
                .WithMany(u => u.SharedDocuments)
                .HasForeignKey(ds => ds.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = "category1", Name = "Legal" },
                new Category { Id = "category2", Name = "Technical" }
            );

            modelBuilder.Entity<Models.Document>().HasData(
                new Models.Document
                {
                    Id = "doc1",
                    DocumentItem = "Legal Agreement",
                    Number = "LA-2024-01",
                    Name = "Service Agreement",
                    VersionName = "v1.0",
                    ReleasedDate = new DateTime(2024, 1, 15),
                    Tag = "Contract",
                    Location = "Locker A-1",
                    IsPublic = true,
                    CategoryId = "existing-category1", // Ensure this ID exists in your Category table
                    CreatedById = "existing-user1", // Ensure this ID exists in your User table
                    ExpDate = new DateTime(2025, 1, 15),
                    Reminder = 30,
                    CreatedAt = new DateTime(2024, 1, 10)
                },
                new Models.Document
                {
                    Id = "doc2",
                    DocumentItem = "Project Report",
                    Number = "PR-2024-02",
                    Name = "Annual Report",
                    VersionName = "v2.0",
                    ReleasedDate = new DateTime(2024, 2, 10),
                    Tag = "Report",
                    Location = "Locker B-2",
                    IsPublic = true,
                    CategoryId = "existing-category2",
                    CreatedById = "existing-user2",
                    ExpDate = new DateTime(2026, 2, 10),
                    Reminder = 60,
                    CreatedAt = new DateTime(2024, 2, 1)
                },
                new Models.Document
                {
                    Id = "doc3",
                    DocumentItem = "User Guide",
                    Number = "UG-2024-03",
                    Name = "System User Guide",
                    VersionName = "v3.0",
                    ReleasedDate = new DateTime(2024, 3, 5),
                    Tag = "Manual",
                    Location = "Locker C-3",
                    IsPublic = false,
                    CategoryId = "existing-category3",
                    CreatedById = "existing-user3",
                    ExpDate = null,
                    Reminder = null,
                    CreatedAt = new DateTime(2024, 3, 1)
                },
                new Models.Document
                {
                    Id = "doc4",
                    DocumentItem = "Policy Document",
                    Number = "PD-2024-04",
                    Name = "Company Policy",
                    VersionName = "v1.1",
                    ReleasedDate = new DateTime(2024, 4, 15),
                    Tag = "Policy",
                    Location = "Locker D-4",
                    IsPublic = true,
                    CategoryId = "existing-category1",
                    CreatedById = "existing-user4",
                    ExpDate = new DateTime(2025, 4, 15),
                    Reminder = 45,
                    CreatedAt = new DateTime(2024, 4, 1)
                },
                new Models.Document
                {
                    Id = "doc5",
                    DocumentItem = "Invoice",
                    Number = "IN-2024-05",
                    Name = "Invoice for Services",
                    VersionName = "v1.0",
                    ReleasedDate = new DateTime(2024, 5, 20),
                    Tag = "Invoice",
                    Location = "Locker E-5",
                    IsPublic = false,
                    CategoryId = "existing-category2",
                    CreatedById = "existing-user5",
                    ExpDate = new DateTime(2025, 5, 20),
                    Reminder = 30,
                    CreatedAt = new DateTime(2024, 5, 1)
                },
                new Models.Document
                {
                    Id = "doc6",
                    DocumentItem = "Audit Report",
                    Number = "AR-2024-06",
                    Name = "Internal Audit",
                    VersionName = "v1.0",
                    ReleasedDate = new DateTime(2024, 6, 25),
                    Tag = "Audit",
                    Location = "Locker F-6",
                    IsPublic = true,
                    CategoryId = "existing-category3",
                    CreatedById = "existing-user6",
                    ExpDate = new DateTime(2025, 6, 25),
                    Reminder = 60,
                    CreatedAt = new DateTime(2024, 6, 1)
                },
                new Models.Document
                {
                    Id = "doc7",
                    DocumentItem = "Technical Specification",
                    Number = "TS-2024-07",
                    Name = "Product Specification",
                    VersionName = "v2.1",
                    ReleasedDate = new DateTime(2024, 7, 10),
                    Tag = "Specification",
                    Location = "Locker G-7",
                    IsPublic = true,
                    CategoryId = "existing-category1",
                    CreatedById = "existing-user7",
                    ExpDate = new DateTime(2025, 7, 10),
                    Reminder = 15,
                    CreatedAt = new DateTime(2024, 7, 1)
                },
                new Models.Document
                {
                    Id = "doc8",
                    DocumentItem = "Memo",
                    Number = "ME-2024-08",
                    Name = "Internal Memo",
                    VersionName = "v1.0",
                    ReleasedDate = new DateTime(2024, 8, 5),
                    Tag = "Memo",
                    Location = "Locker H-8",
                    IsPublic = false,
                    CategoryId = "existing-category2",
                    CreatedById = "existing-user8",
                    ExpDate = new DateTime(2025, 8, 5),
                    Reminder = 30,
                    CreatedAt = new DateTime(2024, 8, 1)
                },
                new Models.Document
                {
                    Id = "doc9",
                    DocumentItem = "Financial Statement",
                    Number = "FS-2024-09",
                    Name = "Quarterly Financials",
                    VersionName = "v1.2",
                    ReleasedDate = new DateTime(2024, 9, 15),
                    Tag = "Finance",
                    Location = "Locker I-9",
                    IsPublic = true,
                    CategoryId = "existing-category3",
                    CreatedById = "existing-user9",
                    ExpDate = null,
                    Reminder = null,
                    CreatedAt = new DateTime(2024, 9, 1)
                },
                new Models.Document
                {
                    Id = "doc10",
                    DocumentItem = "Presentation",
                    Number = "PR-2024-10",
                    Name = "Project Presentation",
                    VersionName = "v1.0",
                    ReleasedDate = new DateTime(2024, 10, 10),
                    Tag = "Presentation",
                    Location = "Locker J-10",
                    IsPublic = false,
                    CategoryId = "existing-category1",
                    CreatedById = "existing-user10",
                    ExpDate = new DateTime(2025, 10, 10),
                    Reminder = 30,
                    CreatedAt = new DateTime(2024, 10, 1)
                }
            );

        }
    }
}

