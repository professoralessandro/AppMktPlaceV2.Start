#region IMPORTS
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Json;
using AppMktPlaceV2.Start.Domain.Interfaces.Repository.Base;
#endregion

namespace AppMktPlaceV2.Start.Infrastructure.Repositorys.Base
{
    public class HttpClientBase<TEntity> : IDisposable, IHttpClientBase
    {
        #region ATRIBUTTES
        private readonly HttpClient _httpClient;
        #endregion

        #region CONSTRUCTOR
        public HttpClientBase(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public HttpClientBase()
        {
            _httpClient = new HttpClient();
        }
        #endregion

        #region GETASYNC
        public async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null)
        {
            // CREATING HEADER REQUEST
            if (headers != null)
                foreach (var header in headers)
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        #endregion

        #region POST ASYNC
        public async Task<T> PostAsync<T>(string url, object data, Dictionary<string, string> headers = null)
        {
            try
            {
                // CREATING HEADER REQUEST
                if (headers != null)
                    foreach (var header in headers)
                        _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, data);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<T> PostAsync<T>(string url, NameValueCollection data, Dictionary<string, string> headers = null)
        {
            // CREATING HEADER REQUEST
            if (headers != null)
                foreach (var header in headers)
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        #endregion

        #region PUTASYNC
        public async Task<T> PutAsync<T>(string url, T data, Dictionary<string, string> headers = null)
        {
            // CREATING HEADER REQUEST
            if (headers != null)
                foreach (var header in headers)
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        #endregion

        #region DELETE ASYNC
        public async Task DeleteAsync(string url, Dictionary<string, string> headers = null)
        {
            // CREATING HEADER REQUEST
            if (headers != null)
                foreach (var header in headers)
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

            HttpResponseMessage response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
        #endregion

        #region DISPOSE
        public void Dispose()
        {
            // _httpClient.Dispose();
        }
        #endregion
    }
}
