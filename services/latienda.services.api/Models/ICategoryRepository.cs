using System;
using System.Linq;

namespace latienda.services.api.Models
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        Category AddCategory(Category request);
        Category DeleteCategory(string categoryIdentifier);
        Category UpdateCategory(Category request, string categoryIdentifier);
    }
}
