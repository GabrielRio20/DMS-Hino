using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMS_Hino.Models
{
    public class Department
    {
        public string Id { get; set; }
        public string Name { get; set; }

        // Foreign key to Division
        public string DivisionId { get; set; }
        public Division Division { get; set; }

        // Navigation property to Users
        public ICollection<User> Users { get; set; }
    }



}
