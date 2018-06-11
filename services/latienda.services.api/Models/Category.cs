using System;
using System.ComponentModel.DataAnnotations;

namespace latienda.services.api.Models
{
    public class Category
    {
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
