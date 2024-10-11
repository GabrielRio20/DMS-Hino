using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMS_Hino.Models
{
    public class Division
    {
        public string Id { get; set; }
        public string Name { get; set; }

        // One division can have many departments
        public ICollection<Department> Departments { get; set; }

        // Navigation property to Users
        public ICollection<User> Users { get; set; }
    }


}
