using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static class ApiHelper
{
    private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };

    public static async Task<T> GetAsync<T>(string endpoint)
    {
        var response = await client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        string result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(result);
    }

    public static async Task<bool> PostAsync<T>(string endpoint, T data)
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(endpoint, content);
        return response.IsSuccessStatusCode;
    }
}
