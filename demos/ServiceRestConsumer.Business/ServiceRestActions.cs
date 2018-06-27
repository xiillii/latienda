using System;
using System.Net.Http;
using System.Threading.Tasks;
using ServiceRestConsumer;
using ServiceRestConsumer.Business.Model;
using ServiceRestConsumer.Entities;
using ServiceRestConsumer.Entities.Response;

namespace ServiceRestConsumer.Business
{
    public class ServiceRestActions
    {
        private readonly string _baseUri;
        private ServiceRest _consumer;

        public ServiceRestActions(string baseUri)
        {
            _baseUri = baseUri;
            _consumer = new ServiceRest(_baseUri);
        }

        public async Task<CategoryBusinessResponse> AddCategoryAsync(Category request)
        {
            var action = $"api/category";
            var response = new CategoryBusinessResponse();

            try
            {
                var result = await _consumer.Execute<Category, CategoryResponse>(request, action, HttpMethod.Post);

                if (result != null && result.Meta.Status == ResponseTypes.Success)
                {
                    response.Success = true;
                    response.Response = result;
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Exception = e;
            }

            return response;
        }
    }
}