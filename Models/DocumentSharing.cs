using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMS_Hino.Models
{
    public class DocumentSharing
    {
        public string Id { get; set; }

        // Foreign Keys
        public string DocumentId { get; set; }
        public Document Document { get; set; }

        public string UserId { get; set; } // Foreign Key to User
        public User User { get; set; } // Navigation Property
    }


}
