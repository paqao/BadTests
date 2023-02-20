using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class ExternalClient : IExternalClient
    {
        private readonly HttpClient _client;

        public ExternalClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<int> CountItems()
        {
            var item = await _client.GetAsync("localhost:12000");
            item.EnsureSuccessStatusCode();

            var content = await item.Content.ReadAsStringAsync();

            return int.Parse(content);
        }
    }
}
