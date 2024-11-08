﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMS_Hino.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string? DocumentUpload { get; set; }
        public string DivisionId { get; set; }
        public Division Division { get; set; }

        public string DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Document> CreatedDocuments { get; set; } = new List<Document>();
        public ICollection<Document> ModifiedDocuments { get; set; } = new List<Document>();

        public ICollection<DocumentSharing> SharedDocuments { get; set; } = new List<DocumentSharing>();
    }
}
