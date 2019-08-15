using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TestWebAppV3.Models;

namespace TestWebAppV3.JsonPlaceHolderData
{
    public class JsonPlaceHolderClient
    {
        private readonly HttpClient httpClient;

        public JsonPlaceHolderClient(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<Album>> GetAlbumsAsync()
        {
            return await ReadResponseAsync<Album>(await httpClient.GetAsync("/albums"));
        }

        public async Task<IEnumerable<Photo>> GetPhotosAsync()
        {
            return await ReadResponseAsync<Photo>(await httpClient.GetAsync("/photos"));
        }

        private async Task<IEnumerable<T>> ReadResponseAsync<T>(HttpResponseMessage responseMessage) where T : class
        {
            if (responseMessage.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<T>>(await responseMessage.Content.ReadAsStringAsync());
            else
                throw new Exception("Message failed to get data from jsonplaceholder");
        }
    }
}
