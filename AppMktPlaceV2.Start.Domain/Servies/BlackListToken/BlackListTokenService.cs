#region REFERENCES
using AppMktPlaceV2.Start.Domain.Interfaces.Repository.BlackListToken;
using AppMktPlaceV2.Start.Domain.Interfaces.Services.BlackListToken;
#endregion REFERENCES

namespace AppMktPlaceV2.Start.Domain.Servies.BlackListToken
{
    public class BlackListTokenService : IBlackListTokenService
    {
        #region ATRIBUTTES
        private readonly IBlackListTokenRepository _repository;
        #endregion ATRIBUTTES

        #region CONTRUCTORS
        public BlackListTokenService(IBlackListTokenRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region CHECK BLACK LIST TOKEN
        public async Task<bool> CheckTokenInBlackList(string token)
        {
            try
            {
                var result = await _repository.CheckTokenInBlackList(token);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao buscar o registro desejado!" + ex.Message);
            }
        }
        #endregion CHECK BLACK LIST TOKEN
    }
}
