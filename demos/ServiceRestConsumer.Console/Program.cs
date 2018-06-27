using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceRestConsumer.Business;
using ServiceRestConsumer.Entities;

namespace ServiceRestConsumer.ConsoleApp
{
    class Program
    {
        private static string _apiUri;
        
        static void Main(string[] args)
        {
            Console.Title = $"Consume Rest API Demo";

            LoadConfiguration();

            JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                settings.Formatting = Formatting.Indented;

                return settings;
            });
            
            Console.WriteLine($"Consuming: {_apiUri}");

            MainAsync().Wait();
        }

        private static void LoadConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            _apiUri = configuration["RestApiuri"];
        }

        private static async Task MainAsync()
        {
            var id = await AddCategory();
        }

        private static async Task<Guid?> AddCategory()
        {
            var service = new ServiceRestActions(_apiUri);
            var category = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = $"New Category at {DateTimeOffset.UtcNow.ToLocalTime()}",
                Active = true
            };

            var result = await service.AddCategoryAsync(category);

            Console.WriteLine(
                $"Adding category with result, Meta.DateServer: {result.Response.Meta.Date}, Meta.DateLocal: {result.Response.Meta.Date.ToLocalTime()}");
            var jsonResult = JsonConvert.SerializeObject(result);
            Console.WriteLine(jsonResult);

            return category.CategoryId;
        }
    }
}