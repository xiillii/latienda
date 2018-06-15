using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using latienda.services.api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace latienda.services.api.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository repository;

        public CategoryController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Listado de categorias
        /// </summary>
        /// <returns>The get.</returns>
        [HttpGet]
        public ActionResult Get()
        {
            var result = repository.Categories;

            return Json(result);
        }

        /// <summary>
        /// Agregar nueva categoria
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="request">Request.</param>
        [HttpPost]
        public ActionResult Post([FromBody]Category request)
        {
            if (ModelState.IsValid)
            {
                var result = repository.AddCategory(request);

                return Json(result);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{categoryIdentifier}")]
        public ActionResult Put([FromBody]Category request, string categoryIdentifier)
        {
            if (ModelState.IsValid)
            {
                var result = repository.UpdateCategory(request, categoryIdentifier);

                return Json(result);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{categoryIdentifier}")]
        public ActionResult Delete(string categoryIdentifier)
        {
            if (ModelState.IsValid)
            {
                var result = repository.DeleteCategory(categoryIdentifier);

                return Json(result);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("{categoryIdentifier}")]
        public ActionResult GetOne(string categoryIdentifier)
        {
            if (ModelState.IsValid)
            {
                var result = repository.Get(categoryIdentifier);

                return Json(result);
            }

            return BadRequest(ModelState);
        }
    }
}
