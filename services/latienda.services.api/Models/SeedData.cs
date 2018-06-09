using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace latienda.services.api.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app){
            CategoryDbContext context = app.ApplicationServices.GetRequiredService<CategoryDbContext>();
            context.Database.Migrate();

            if (!context.Categories.Any()){
                context.Categories.AddRange(
                    new Category { CategoryId = Guid.NewGuid(), Name = "Memoria RAM", Active = true },
                    new Category { CategoryId = Guid.NewGuid(), Name = "Discos Duros", Active = true },
                    new Category { CategoryId = Guid.NewGuid(), Name = "Monitores", Active = true },
                    new Category { CategoryId = Guid.NewGuid(), Name = "Accesorios", Active = true },
                    new Category { CategoryId = Guid.NewGuid(), Name = "Limpieza", Active = true }
                );
                context.SaveChanges();
            }
        }
    }
}
