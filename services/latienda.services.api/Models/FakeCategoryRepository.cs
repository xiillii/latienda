using System;
using System.Collections.Generic;
using System.Linq;

namespace latienda.services.api.Models
{
    public class FakeCategoryRepository : ICategoryRepository
    {
        public IQueryable<Category> Categories =>
        new List<Category>{
            new Category{CategoryId = Guid.NewGuid(), Name = "Category 1", Active = true},
            new Category{CategoryId = Guid.NewGuid(), Name = "Category 2", Active = true},
            new Category{CategoryId = Guid.NewGuid(), Name = "Category 3", Active = true}
        }.AsQueryable();

        public CategoryResponse AddCategory(Category request) => null;

        public CategoryResponse DeleteCategory(string categoryIdentifier) => null;

        public CategoryResponse UpdateCategory(Category request, string categoryIdentifier) => null;
        
        public CategoryResponse Get(string categoryIdentifier) => null;

        public CategoriesResponse ListCategories() => null;
    }
}
