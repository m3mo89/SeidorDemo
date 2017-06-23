using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace SeidorDemo
{
    public class CloudDataStore : IDataStore<Document>
    {
        HttpClient client;
        IEnumerable<Document> items;

        public CloudDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.BackendUrl}/");

            items = new List<Document>();
        }

        public async Task<IEnumerable<Document>> GetItemsAsync()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/Document");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Document>>(json));
            }

            return items;
        }
    }
}
