using DMS_Hino.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DMS_Hino.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Models.Document> Documents { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DocumentRelation> DocumentRelations { get; set; }
        public DbSet<DocumentSharing> DocumentSharings { get; set; }

        public DatabaseContext() { }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

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
        }
    }
}

