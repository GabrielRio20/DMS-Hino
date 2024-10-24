using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMS_Hino.Models
{
    public class Division
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<User> Users { get; set; }
    }


}
