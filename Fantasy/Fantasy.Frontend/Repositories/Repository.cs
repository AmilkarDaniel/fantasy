using System.Text;
using System.Text.Json;

namespace Fantasy.Frontend.Repositories;

public class Repository : IRepository
{
    private readonly HttpClient _httpClient;

    public Repository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private JsonSerializerOptions _jsonDefaultOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };

    public async Task<HttpReponseWrapper<T>> GetAsync<T>(string url)
    {
        var responseHttp = await _httpClient.GetAsync(url);
        if (responseHttp.IsSuccessStatusCode)
        {
            var response = await UnserializeAnswer<T>(responseHttp);
            return new HttpReponseWrapper<T>(response, false, responseHttp);
        }
        return new HttpReponseWrapper<T>(default, true, responseHttp);
    }

    public async Task<HttpReponseWrapper<object>> PostAsync<T>(string url, T model)
    {
        var messageJSON = JsonSerializer.Serialize(model);
        var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
        var responseHttp = await _httpClient.PostAsync(url, messageContent);
        return new HttpReponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
    }

    public async Task<HttpReponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model)
    {
        var messageJSON = JsonSerializer.Serialize(model);
        var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
        var responseHttp = await _httpClient.PostAsync(url, messageContent);
        if (responseHttp.IsSuccessStatusCode)
        {
            var response = await UnserializeAnswer<TActionResponse>(responseHttp);
            return new HttpReponseWrapper<TActionResponse>(response, false, responseHttp);
        }
        return new HttpReponseWrapper<TActionResponse>(default, !responseHttp.IsSuccessStatusCode, responseHttp);
    }

    public async Task<HttpReponseWrapper<object>> DeleteAsync(string url)
    {
        var responseHttp = await _httpClient.DeleteAsync(url);
        return new HttpReponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
    }

    public async Task<HttpReponseWrapper<object>> PutAsync<T>(string url, T model)
    {
        var messageJSON = JsonSerializer.Serialize(model);
        var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
        var responseHttp = await _httpClient.PutAsync(url, messageContent);
        return new HttpReponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
    }

    public async Task<HttpReponseWrapper<TActionResponse>> PutAsync<T, TActionResponse>(string url, T model)
    {
        var messageJSON = JsonSerializer.Serialize(model);
        var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
        var responseHttp = await _httpClient.PutAsync(url, messageContent);
        if (responseHttp.IsSuccessStatusCode)
        {
            var response = await UnserializeAnswer<TActionResponse>(responseHttp);
            return new HttpReponseWrapper<TActionResponse>(response, false, responseHttp);
        }
        return new HttpReponseWrapper<TActionResponse>(default, !responseHttp.IsSuccessStatusCode, responseHttp);
    }

    private async Task<T> UnserializeAnswer<T>(HttpResponseMessage responseHttp)
    {
        var response = await responseHttp.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(response, _jsonDefaultOptions)!;
    }
}