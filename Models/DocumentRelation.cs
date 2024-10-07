using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMS_Hino.Models
{
    public class DocumentRelation
    {
        public string Id { get; set; }

        // Foreign Keys
        public string DocumentId { get; set; }
        public Document Document { get; set; }

        public string RelationDocumentId { get; set; }
        public Document RelatedDocument { get; set; }
    }

}
