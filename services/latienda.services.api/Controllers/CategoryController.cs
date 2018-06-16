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
            var result = repository.ListCategories();

            if (result.Data != null)
            {
                result.Meta.Status = ResponseTypes.Success;
                return Ok(result);
            }
            
            result.Meta.Status = ResponseTypes.Failed;
            return NotFound(result);
        }

        /// <summary>
        /// Add a new category item
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="request">Request.</param>
        [HttpPost]
        public ActionResult Post([FromBody]Category request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = repository.AddCategory(request);
            

            if (result.Data != null)
            {
                result.Meta.Status = ResponseTypes.Success;
                return Ok(result);
            }
            
            result.Meta.Status = ResponseTypes.Failed;
            return NotFound(result);
        }


        [HttpPut("{categoryIdentifier}")]
        public ActionResult Put([FromBody]Category request, string categoryIdentifier)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = repository.UpdateCategory(request, categoryIdentifier);

            if (result.Data != null)
            {
                result.Meta.Status = ResponseTypes.Success;
                return Ok(result);
            }
            
            result.Meta.Status = ResponseTypes.Failed;
            return NotFound(result);
        }

        [HttpDelete("{categoryIdentifier}")]
        public ActionResult Delete(string categoryIdentifier)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = repository.DeleteCategory(categoryIdentifier);

            if (result.Data != null)
            {
                result.Meta.Status = ResponseTypes.Success;
                return Ok(result);
            }
            
            result.Meta.Status = ResponseTypes.Failed;
            return NotFound(result);

        }

        [HttpGet("{categoryIdentifier}")]
        public ActionResult GetOne(string categoryIdentifier)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = repository.Get(categoryIdentifier);

            if (result.Data != null)
            {
                result.Meta.Status = ResponseTypes.Success;
                return Ok(result);
            }
            
            result.Meta.Status = ResponseTypes.Failed;
            return NotFound(result);
        }
    }
}
