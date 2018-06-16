using System;
using System.Collections.Generic;
using System.Linq;

namespace latienda.services.api.Models
{
    /// <summary>
    /// Interface to implement a category repository
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// A list of categories
        /// </summary>
        IQueryable<Category> Categories { get; }

        
        CategoriesResponse ListCategories();
        CategoryResponse AddCategory(Category request);
        CategoryResponse DeleteCategory(string categoryIdentifier);
        CategoryResponse UpdateCategory(Category request, string categoryIdentifier);
        CategoryResponse Get(string categoryIdentifier);
    }
}
