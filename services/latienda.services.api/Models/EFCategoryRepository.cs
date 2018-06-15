using System;
using System.Linq;

namespace latienda.services.api.Models
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly CategoryDbContext context;

        public EFCategoryRepository(CategoryDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Category> Categories => context.Categories;

        public Category AddCategory(Category request)
        {
            context.Categories.Add(request);
            context.SaveChanges();

            return request;
        }

        public Category DeleteCategory(string categoryIdentifier)
        {
            Guid.TryParse(categoryIdentifier, out var _theId);

            var item = context.Categories
                              .SingleOrDefault(c => c.CategoryId == _theId);

            if (item != null)
            {
                context.Categories.Remove(item);
                context.SaveChanges();
            }

            return item;
        }

        public Category UpdateCategory(Category request, string categoryIdentifier)
        {
            Guid.TryParse(categoryIdentifier, out var _theId);
            var item = context.Categories.SingleOrDefault(c => c.CategoryId == _theId);

            if (item != null)
            {
                item.Name = request.Name;
                item.Active = request.Active;
                context.SaveChanges();
            }

            return item;
        }
    }
}
