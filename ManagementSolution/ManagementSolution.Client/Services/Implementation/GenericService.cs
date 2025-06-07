using ManagementSolution.Application.Responses;
using ManagementSolution.Client.Helpers;
using ManagementSolution.Client.Services.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace ManagementSolution.Client.Services.Implementation
{
    public class GenericService<T>(GetHttpClient getHttpClient) : IGenericService<T>
    {
        /// <summary>
        /// Deletes an item by the identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public async Task<GeneralResponse> DeleteById(string id, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.DeleteAsync($"{baseUrl}/delete/{id}");
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        /// <summary>
        /// Checks if item exists.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public async Task<bool> Exists(string id, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.GetAsync($"{baseUrl}/exists/{id}");
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAll(string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            return await httpClient.GetFromJsonAsync<List<T>>($"{baseUrl}/all"); 
        }

        /// <summary>
        /// Gets a single item by the identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public async Task<T> GetById(string id, string baseUrl)
        {
                var httpClient = await getHttpClient.GetPrivateHttpClient();
                var result = await httpClient.GetFromJsonAsync<T>($"{baseUrl}/single/{id}");
                return result!;
        }

        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="baseUrl"></param>
        /// <returns>The item created.</returns>
        public async Task<GeneralResponse> Insert(T item, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}/add", item);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        /// <summary>
        /// Updates an existing item.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public async Task<GeneralResponse> Update(T item, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PutAsJsonAsync($"{baseUrl}/update", item);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
    }
}
