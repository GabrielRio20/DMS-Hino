using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMS_Hino.Models
{
    public class Document
    {
        public string Id { get; set; }
        public string DocumentItem { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string VersionName { get; set; }
        public DateTime ReleasedDate { get; set; }
        public string Tag { get; set; }
        public string Location { get; set; }
        public bool IsPublic { get; set; }

        // Foreign Keys
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public string? CreatedById { get; set; } // Foreign Key to User
        public User CreatedBy { get; set; }

        public string? ModifiedById { get; set; } // Foreign Key to User (nullable)
        public User? ModifiedBy { get; set; }

        public DateTime? ExpDate { get; set; }
        public int? Reminder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        // Navigation Properties
        public ICollection<DocumentRelation> RelatedDocuments { get; set; }
        public ICollection<DocumentSharing> SharedDocuments { get; set; }
    }


}
