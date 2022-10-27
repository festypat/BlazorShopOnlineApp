using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto> GetItem(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(ProductDto);

                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }

                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetItems()
        {
            //var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
            var response = await _httpClient.GetAsync("api/Product");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return Enumerable.Empty<ProductDto>();

                return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }
}
