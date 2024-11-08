﻿using DMS_Hino.Models;

namespace DMS_Hino.ViewModel
{
    public class DocumentViewModel
    {
        public string Id { get; set; }
        public string SelectedCategoryId { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public string CategoryTitle { get; set; }

        public List<Document> Documents { get; set; } = new List<Document>();

        public IFormFile DocumentItem { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string VersionName { get; set; }
        public DateTime ReleasedDate { get; set; }
        public string Tag { get; set; }
        public string Location { get; set; }
        public bool IsPublic { get; set; }
        public string CategoryId { get; set; }
        public string CreatedById { get; set; }
        public string? ModifiedById { get; set; }
        public DateTime? ExpDate { get; set; }
        public int? Reminder { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public List<User> Users { get; set; } = new List<User>();

        //public List<Division> Divisions { get; set; } = new List<Division>();
        public List<Division> Divisions { get; set; }
        public List<Department> Departments { get; set; }
        public DivisionDepartmentViewModel DivisionDepartmentViewModel { get; set; }
    }

}
