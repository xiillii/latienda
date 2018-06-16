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

        /// <summary>
        /// Get a list of categories
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Add a new category item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CategoryResponse AddCategory(Category request)
        {
            var result = new CategoryResponse
            {
                Meta = new Meta
                {
                    ResponseIdentifier = Guid.NewGuid(),
                    Date = DateTimeOffset.UtcNow.LocalDateTime
                }
            };

            try
            {
                context.Categories.Add(request);
                context.SaveChanges();
                result.Data = request;
            }
            catch (Exception e)
            {
                result.Meta.Messages = new List<string> {e.Message};
            }


            
            return result;
        }

        /// <summary>
        /// Delete a category item
        /// </summary>
        /// <param name="categoryIdentifier"></param>
        /// <returns></returns>
        public CategoryResponse DeleteCategory(string categoryIdentifier)
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

            var item = context.Categories
                .SingleOrDefault(c => c.CategoryId == _theId);

            if (item != null)
            {
                context.Categories.Remove(item);
                context.SaveChanges();

                result.Data = item;
            }
            else
            {
                result.Meta.Messages = new List<string>
                {
                    "Category Identifier not found"
                };
            }

            return result;
        }

        /// <summary>
        /// Update a category item
        /// </summary>
        /// <param name="request"></param>
        /// <param name="categoryIdentifier"></param>
        /// <returns></returns>
        public CategoryResponse UpdateCategory(Category request, string categoryIdentifier)
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

            if (item != null)
            {
                item.Name = request.Name;
                item.Active = request.Active;
                context.SaveChanges();

                
                result.Data = item;
            }
            else
            {
                result.Meta.Messages = new List<string>
                {
                    "Category Identifier not found"
                };
            }

            return result;
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
