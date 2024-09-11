#region REFERENCES
using AppMktPlaceV2.Start.Application.Dtos.Base.Response.Common;
using AppMktPlaceV2.Start.Application.Helper.Settings;
using AppMktPlaceV2.Start.Domain.Interfaces.Repository.BlackListToken;
using AppMktPlaceV2.Start.Infrastructure.Repositorys.Base;
using Microsoft.Extensions.Options;
#endregion REFERENCES

namespace AppMktPlaceV2.Start.Infrastructure.Repositorys.BlackListToken
{
    public class BlackListTokenRepository : HttpClientBase<object>, IBlackListTokenRepository
    {
        #region ATRIBUTTES
        private readonly HttpRumtimeSettings _setings;
        #endregion

        #region CONSTRUCTORES
        public BlackListTokenRepository(IOptions<HttpRumtimeSettings> setings)
        {
            _setings = setings.Value;
        }
        #endregion

        #region VALIDATE IF BLACK LIST TOKEN
        public async Task<bool> CheckTokenInBlackList(string token)
        {
            string url = string.Concat(_setings.SecurityApiUrl, _setings.SecurityBlackList);

            var result = await this.PostAsync<BaseResponseDto>(url, data: token);

            return bool.TryParse(result.JsonObject?.ToString(), out bool isInBlackList) ? isInBlackList : true;
        }
        #endregion VALIDATE IF BLACK LIST TOKEN
    }
}
