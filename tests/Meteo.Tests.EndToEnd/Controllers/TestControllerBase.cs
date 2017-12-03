using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Meteo.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace Meteo.Tests.EndToEnd.Controllers
{
    public class TestControllerBase
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;

        public TestControllerBase()
        {
            Server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            Client = Server.CreateClient();            
        }

        private async Task<T> GetAsync<T>(string endpoint)
            => await DeserializeAsync<T>(await Client.GetAsync(endpoint));

        private async Task<HttpResponseMessage> GetAsync(string endpoint)
            => await Client.GetAsync(endpoint);

        private async Task<HttpResponseMessage> PostAsync(string endpoint, object data)
            => await Client.PostAsync(endpoint, GeyPayload(data));

        private async Task<T> DeserializeAsync<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        private StringContent GeyPayload(object data)
            => new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
    }
}