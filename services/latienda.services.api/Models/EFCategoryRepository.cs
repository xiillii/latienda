using System;
using System.Collections.Generic;
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

        public CategoriesResponse ListCategories()
        {
            var result = new CategoriesResponse
            {
                Meta = new Meta
                {
                    ResponseIdentifier = Guid.NewGuid(),
                    Date = DateTimeOffset.UtcNow.LocalDateTime,
                    Status = ResponseTypes.Success
                },
                Data = Categories
            };

            if (Categories == null || !Categories.Any())
            {
                result.Meta.Messages = new List<string> {"Categories list empty"};
            }


            return result;
        }

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

        /// <summary>
        /// Obtiene un solo valor de categoría
        /// </summary>
        /// <param name="categoryIdentifier"></param>
        /// <returns></returns>
        public CategoryResponse Get(string categoryIdentifier)
        {
            var result = new CategoryResponse
            {
                Meta = new Meta
                {
                    ResponseIdentifier = Guid.NewGuid(),
                    Date = DateTimeOffset.UtcNow.LocalDateTime
                }
            };
            
            Guid.TryParse(categoryIdentifier, out var _theId);
            var item = context.Categories.SingleOrDefault(c => c.CategoryId == _theId);

            if (item == null)
            {
                result.Meta.Messages = new List<string>
                {
                    "Category Identifier not found"
                };
            }
            else
            {
                result.Data = item;
            }

            

            return result;
        }
    }
}
