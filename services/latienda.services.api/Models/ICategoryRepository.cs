using System;
using System.Linq;

namespace latienda.services.api.Models
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
    }
}
