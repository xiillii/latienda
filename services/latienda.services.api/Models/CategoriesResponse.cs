using System.Collections.Generic;
using System.Linq;

namespace latienda.services.api.Models
{
    public class CategoriesResponse : BaseResponse
    {
        public IQueryable<Category> Data { get; set; }
    }
}