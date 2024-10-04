namespace Fantasy.Frontend.Repositories;

public interface IRepository
{
	Task<HttpReponseWrapper<T>> GetAsync<T>(string url);

	Task<HttpReponseWrapper<T>> PostAsync<T>(string url, T model);

	Task<HttpReponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model);
}