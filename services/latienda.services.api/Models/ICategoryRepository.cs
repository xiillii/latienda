using System;
using System.Collections.Generic;
using System.Linq;

namespace latienda.services.api.Models
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        CategoriesResponse ListCategories();
        Category AddCategory(Category request);
        Category DeleteCategory(string categoryIdentifier);
        Category UpdateCategory(Category request, string categoryIdentifier);
        CategoryResponse Get(string categoryIdentifier);
    }
}
