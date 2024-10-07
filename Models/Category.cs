using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMS_Hino.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }

        // Navigation
        public ICollection<Document> Documents { get; set; }
    }

}
