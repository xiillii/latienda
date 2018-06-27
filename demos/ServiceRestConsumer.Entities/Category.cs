using System;

namespace ServiceRestConsumer.Entities
{
    public class Category
    {
        public Guid? CategoryId { get; set; }
        
        public string Name { get; set; }
        
        public bool? Active { get; set; }
    }
}