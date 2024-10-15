namespace Fantasy.Frontend.Repositories;

public interface IRepository
{
    Task<HttpReponseWrapper<object>> DeleteAsync(string url);

    Task<HttpReponseWrapper<object>> PutAsync<T>(string url, T model);

    Task<HttpReponseWrapper<TActionResponse>> PutAsync<T, TActionResponse>(string url, T model);

    Task<HttpReponseWrapper<T>> GetAsync<T>(string url);

    Task<HttpReponseWrapper<object>> PostAsync<T>(string url, T model);

    Task<HttpReponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model);
}