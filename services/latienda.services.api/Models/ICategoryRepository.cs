using System;
using System.Linq;

namespace latienda.services.api.Models
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        Category AddCategory(Category request);
        Category DeleteCategory(Guid categoryIdentifier);
        Category UpdateCategory(Category request, string categoryIdentifier);
    }
}
