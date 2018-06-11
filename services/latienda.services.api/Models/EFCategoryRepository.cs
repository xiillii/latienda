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
    }
}
