using AppMktPlaceV2.Security.Domain.Entities;
using AppMktPlaceV2.Security.Domain.Entities;

namespace AppMktPlaceV2.Security.Domain.Interfaces.Repository.User
{
    public interface IUserRepository
    {
        #region FIND BY ID
        Task<Usuario> GetByIdAsync(Guid id);
        #endregion

        #region FIND USERS TO SELECT RETURN KEY AND VALUE
        Task<IEnumerable<T>> ReturnUsersToSelectAsync<T>(string? param = null, int? pageNumber = null, int? rowspPage = null);
        #endregion

        #region RETURN LIST WITH PARAMETERS PAGINATED
        Task<IEnumerable<T>> ReturnListFromQueryAsync<T>(string query, object? param = null);
        #endregion

        #region RETURN SINGLE WITH PARAMETERS PAGINATED
        Task<T> ReturnObjectSingleFromQueryAsync<T>(string query, object? param = null);
        #endregion

        #region RETURN LIST WITH PARAMETERS PAGINATED
        Task<IEnumerable<T>> ReturnListWithParametersPaginated<T>(Guid? userId = null, Guid? id = null, string? userName = null, string? nome = null, string? nmrDocumento = null, string? email = null, bool? ativo = null, int? pageNumber = null, int? rowspPage = null);
        #endregion

        #region GET ALL
        Task<IEnumerable<Usuario>> GetAllAsync();
        #endregion

        #region INSERT
        Task AddAsync(Usuario obj);
        #endregion

        #region UPDATE
        Task UpdateAsync(Usuario obj);
        #endregion

        #region DELETE
        Task RemoveAsync(Usuario obj);
        #endregion
    }
}
