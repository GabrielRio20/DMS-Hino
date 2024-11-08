using DMS_Hino.Data;

namespace DMS_Hino.Service
{
    public class DocumentService
    {
        private readonly DatabaseContext _context;  // Ganti sesuai konteks Anda

        public DocumentService(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.Document> GetDocumentsByCategoryName(string categoryName)
        {
            return _context.Documents
                           .Where(d => d.Category.Name == categoryName)
                           .ToList();
        }

        public IEnumerable<Models.Document> GetDocumentsByCategoryId(string categoryId)
        {
            return _context.Documents
                           .Where(d => d.CategoryId == categoryId)
                           .ToList();
        }
    }
}
