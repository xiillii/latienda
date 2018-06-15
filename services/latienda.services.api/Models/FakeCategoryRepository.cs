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

        public Category AddCategory(Category request)
        {
            

            return request;
        }

        public Category DeleteCategory(string categoryIdentifier)
        {
            

            return null;
        }

        public Category UpdateCategory(Category request, string categoryIdentifier)
        {
            

            return null;
        }
    }
}
