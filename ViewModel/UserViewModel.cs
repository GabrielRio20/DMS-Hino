using DMS_Hino.Models;
using System.ComponentModel.DataAnnotations;

namespace DMS_Hino.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // You can choose to omit this if not needed
        public string Role { get; set; }

        // Foreign Keys
        public string DivisionId { get; set; }
        public string DepartmentId { get; set; }

        // Navigation Properties
        public IEnumerable<Division> Divisions { get; set; }
        public IEnumerable<Department> Departments { get; set; }

        // To display selected Division and Department names
        public Division Division { get; set; }
        public Department Department { get; set; }
    }


}
