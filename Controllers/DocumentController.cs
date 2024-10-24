using DMS_Hino.Data;
using DMS_Hino.Models;
using DMS_Hino.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DMS_Hino.Controllers
{
    public class DocumentController : Controller
    {
        private readonly DatabaseContext _context;
        public DocumentController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult PublicDocument()
        {
            var documents = _context.Documents.ToList();

            var model = new DocumentViewModel
            {
                Documents = documents
            };

            return View("DocumentView", model);
        }

        // GET: Render the form
        public IActionResult DocumentAddView()
        {
            var model = new DocumentViewModel
            {
                Categories = _context.Categories.ToList(),
                Users = _context.Users.ToList()
            };
            return View("DocumentAdd", model);
        }

        [HttpPost]
        public async Task<IActionResult> DocumentAdd(DocumentViewModel model)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            if (model.DocumentItem == null || model.DocumentItem.Length == 0)
            {
                ModelState.AddModelError("DocumentItem", "Please upload a document.");
                model.Categories = _context.Categories.ToList(); 
                return View("DocumentAddView", model);
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.DocumentItem.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.DocumentItem.CopyToAsync(stream);
            }

            var document = new Document
            {
                Id = Guid.NewGuid().ToString(),
                DocumentItem = fileName, 
                Number = model.Number,
                Name = model.Name,
                VersionName = model.VersionName,
                ReleasedDate = model.ReleasedDate,
                Tag = model.Tag,
                Location = model.Location,
                IsPublic = model.IsPublic,
                CategoryId = model.CategoryId,
                CreatedById = userId,
                ModifiedById = null,
                ExpDate = model.ExpDate,
                Reminder = model.Reminder,
                CreatedAt = DateTime.Now,
                ModifiedAt = null
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return RedirectToAction("PublicDocument", "Document");
        }

        public IActionResult SearchDocuments(string searchTerm)
        {
            var documents = _context.Documents.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                documents = documents.Where(d => d.Name.Contains(searchTerm) || 
                                                 d.Tag.Contains(searchTerm) ||
                                                 d.Location.Contains(searchTerm));
            }

            var model = new DocumentViewModel
            {
                Documents = documents.ToList()
            };

            return PartialView("_DocumentListPartial", model); // Return a partial view to update the document list dynamically
        }

        public async Task<IActionResult> GetDocumentDetail(string id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            return PartialView("DocumentSidebar", document);  // Mengembalikan partial view
        }

        public IActionResult DocumentDetail(string id)
        {
            var document = _context.Documents.FirstOrDefault(d => d.Id == id);
            if (document == null)
            {
                return NotFound();
            }
            var categories = _context.Categories.ToList();

            ViewBag.Categories = categories;

            return View(document);
        }

        [HttpPost]
        public IActionResult EditDocument(string DocumentId, Models.Document model, IFormFile DocumentFile)
        {
            var documentToUpdate = _context.Documents
                        .Include(d => d.Category)
                        .FirstOrDefault(d => d.Id == DocumentId);
            if (documentToUpdate == null)
            {
                return NotFound();
            }

            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            documentToUpdate.Name = model.Name;
            documentToUpdate.Number = model.Number;
            documentToUpdate.VersionName = model.VersionName;
            documentToUpdate.ReleasedDate = model.ReleasedDate;
            documentToUpdate.Tag = model.Tag;
            documentToUpdate.Location = model.Location      ;
            documentToUpdate.Category = model.Category;

            if (DocumentFile != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + DocumentFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    DocumentFile.CopyTo(stream);
                }

                documentToUpdate.DocumentItem = uniqueFileName;
            }

            _context.SaveChanges();

            return RedirectToAction("DocumentDetail", new { id = DocumentId });
        }
    }
}