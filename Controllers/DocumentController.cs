using DMS_Hino.Data;
using DMS_Hino.Models;
using DMS_Hino.Service;
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
        private readonly DocumentService _documentService;
        public DocumentController(DatabaseContext context, DocumentService documentService)
        {
            _context = context;
            _documentService = documentService;
        }

        public IActionResult UserDashboard()
        {
            return View("UserDashboard");
        }

        public async Task<IActionResult> PublicDocument(string id)
        {
            // Ambil dokumen berdasarkan kategori
            var documents = await _context.Documents.ToListAsync();
            var model = new DocumentViewModel
            {
                Documents = documents
            };

            return View("DocumentView", model);
        }

        [HttpGet]
        public IActionResult GetChildCategories(string parentId)
        {
            var childCategories = _context.Categories
                .Where(c => c.ParentId == parentId)
                .Select(c => new { c.Id, c.Name }) // Pastikan Id dan Name ini sesuai dengan yang diharapkan
                .ToList();

            return Json(childCategories);
        }

        public async Task<IActionResult> CategoryPage(string id)
        {
            var documents = string.IsNullOrEmpty(id)
        ? await _context.Documents.ToListAsync()
        : await _context.Documents.Where(d => d.CategoryId == id).ToListAsync();

            // Ambil kategori dengan struktur parent-child
            var categories = await _context.Categories
                .Where(c => c.ParentId == null)
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ParentId = c.ParentId,
                    Children = _context.Categories
                        .Where(child => child.ParentId == c.Id)
                        .Select(child => new CategoryViewModel
                        {
                            Id = child.Id,
                            Name = child.Name,
                            ParentId = child.ParentId,
                            Documents = _context.Documents.Where(d => d.CategoryId == child.Id).ToList()
                        }).ToList()
                }).ToListAsync();

            // Jika ada `id` kategori, ambil nama kategori untuk judul halaman
            string categoryTitle = string.IsNullOrEmpty(id)
                ? "All Documents"
                : (await _context.Categories.FindAsync(id))?.Name ?? "Category";

            var model = new DocumentViewModel
            {
                Documents = documents,
                Categories = categories,
                CategoryTitle = categoryTitle, // Kirim judul ke view
                CategoryId = id
            };

            return View("CategoryPage", model);
        }

        public async Task<IActionResult> DocumentAddView(string categoryId)
        {
            // Load categories from the database
            var categories = await _context.Categories
                .Where(c => c.ParentId == null)
                .Include(c => c.Children)
                    .ThenInclude(c => c.Children)
                .ToListAsync();

            // Load divisions and departments from the database
            var divisions = await _context.Divisions.Include(d => d.Departments).ToListAsync();

            // Map categories to CategoryViewModel
            var categoryViewModels = MapCategoriesToViewModels(categories);

            // Populate DivisionDepartmentViewModel
            var divisionDepartmentViewModel = new DivisionDepartmentViewModel
            {
                Divisions = divisions,
                Departments = divisions.SelectMany(d => d.Departments).ToList()
            };

            // Create the DocumentViewModel and set DivisionDepartmentViewModel
            var model = new DocumentViewModel
            {
                Categories = categoryViewModels,
                SelectedCategoryId = categoryId,
                DivisionDepartmentViewModel = divisionDepartmentViewModel
            };

            return View("DocumentAdd", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartmentsByDivision(string divisionId)
        {
            if (string.IsNullOrEmpty(divisionId))
            {
                return BadRequest("Division ID is required.");
            }

            var departments = await _context.Departments
                .Where(d => d.DivisionId == divisionId)
                .Select(d => new { id = d.Id, name = d.Name })
                .ToListAsync();

            return Json(departments);
        }

        private List<CategoryViewModel> MapCategoriesToViewModels(List<Category> categories)
        {
            return categories.Select(category => new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                Children = category.Children != null ? MapCategoriesToViewModels(category.Children.ToList()) : new List<CategoryViewModel>()
            }).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> DocumentAdd(DocumentViewModel model)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (userId == null)
            {
                return Unauthorized();
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

            return PartialView("_DocumentListPartial", model); 
        }

        public async Task<IActionResult> GetDocumentDetail(string id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            return PartialView("DocumentSidebar", document); 
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
            var docUpdate = _context.Documents.Where(x => x.Id == DocumentId).FirstOrDefault();

            if (docUpdate == null)
            {
                return NotFound();
            }

            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            docUpdate.Name = model.Name;
            docUpdate.Number = model.Number;
            docUpdate.VersionName = model.VersionName;
            docUpdate.ReleasedDate = model.ReleasedDate;
            docUpdate.Tag = model.Tag;
            docUpdate.Location = model.Location;
            docUpdate.CategoryId = model.CategoryId;
            docUpdate.ExpDate = model.ExpDate;

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

                docUpdate.DocumentItem = uniqueFileName;
            }

            _context.Update(docUpdate);
            _context.SaveChanges();

            return RedirectToAction("DocumentDetail", new { id = DocumentId });
        }

        public IActionResult AllCategory()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        // POST: Document/AddCategory
        [HttpPost]
        public IActionResult AddCategory(string name, string? parentId)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Json(new { success = false, message = "Category name is required." });
            }

            var category = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                ParentId = parentId
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return Json(new { success = true, message = "Category added successfully.", category = category });
        }

        // POST: Document/EditCategory
        [HttpPost]
        public IActionResult EditCategory(string id, string name)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return Json(new { success = false, message = "Category not found." });
            }

            if (string.IsNullOrEmpty(name))
            {
                return Json(new { success = false, message = "Category name is required." });
            }

            category.Name = name;
            _context.SaveChanges();

            return Json(new { success = true, message = "Category updated successfully." });
        }

        // POST: Document/DeleteCategory
        [HttpPost]
        public IActionResult DeleteCategory(string id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return Json(new { success = false, message = "Category not found." });
            }

            var hasChildren = _context.Categories.Any(c => c.ParentId == id);
            if (hasChildren)
            {
                return Json(new { success = false, message = "Cannot delete category with existing subcategories." });
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Json(new { success = true, message = "Category deleted successfully." });
        }
    }
}