public class CategoryViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ParentId { get; set; }
    public ICollection<CategoryViewModel> Children { get; set; }
    public List<DMS_Hino.Models.Document> Documents { get; set; }
    public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
}

