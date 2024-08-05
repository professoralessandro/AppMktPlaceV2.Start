#region ATRIBUTTES
using System.Collections.Specialized;
using System.Net.Http.Headers;
#endregion

namespace AppMktPlaceV2.Start.Domain.Interfaces.Repository.Base
{
    public interface IHttpClientBase
    {
        #region GET ASYNC
        Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null);
        #endregion

        #region POST ASYNC
        Task<T> PostAsync<T>(string url, object data, Dictionary<string, string> headers = null);
        Task<T> PostAsync<T>(string url, NameValueCollection data, Dictionary<string, string> headers = null);
        #endregion

        #region PUT ASYNC
        Task<T> PutAsync<T>(string url, T data, Dictionary<string, string> headers = null);
        #endregion

        #region DELETE ASYNC
        Task DeleteAsync(string url, Dictionary<string, string> headers = null);
        #endregion
    }
}
