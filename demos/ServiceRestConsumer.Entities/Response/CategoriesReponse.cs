using System.Collections.Generic;

namespace ServiceRestConsumer.Entities.Response
{
    public class CategoriesReponse : BaseResponse
    {
        public List<Category> Data { get; set; }
    }
}