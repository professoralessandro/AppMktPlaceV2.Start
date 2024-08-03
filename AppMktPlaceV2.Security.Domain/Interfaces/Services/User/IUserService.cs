#region REFERENCES
using AppMktPlaceV2.Security.Application.Dtos.User.Request;
using AppMktPlaceV2.Security.Application.Models.Dto.Autenticate;
using AppMktPlaceV2.Security.Application.Models.Dto.User;
using AppMktPlaceV2.Security.Application.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
#endregion REFERENCES

namespace AppMktPlaceV2.Security.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        #region FIND BY ID
        Task<UserDto> GetByIdAsync(Guid userId);
        #endregion

        #region FIND USERS TO SELECT RETURN KEY AND VALUE
        Task<IEnumerable<object>> ReturnUsersToSelectAsync(string? param = null, int? pageNumber = null, int? rowspPage = null);
        #endregion

        #region RETURN LIST WITH PARAMETERS PAGINATED
        Task<IEnumerable<UserDto>> ReturnListWithParametersPaginated(Guid? userId, Guid? id = null, string? userName = null, string? nome = null, string? nmrDocumento = null, string? email = null, bool? ativo = null, int? pageNumber = null, int? rowspPage = null);
        #endregion

        #region GET ALL ASYNC
        Task<IEnumerable<UserDto>> GetAllAsync();
        #endregion

        #region INSERT
        Task<UserDto> InsertAsync(UserDto model);
        #endregion

        #region UPDATE
        Task<UserUpdateRequest> UpdateAsync(UserUpdateRequest model, Guid userUpdationgId);
        #endregion

        #region DELETE SERVIÇO DE DELETE
        Task<UserDto> DeleteAsync(Guid avaliacaoId);
        #endregion

        #region VALIDATE IF THE USER IS A ADMIN
        Task<bool> UserServiceValidateAdminUser(Guid userId);
        #endregion

        #region REFRESH TOKEN
        Task<TokenApiDto> RefreshToken(TokenApiDto tokenApiDto);
        #endregion REFRESH TOKEN

        #region AUTHENTICATE
        Task<TokenApiDto> Authenticate([FromBody] AuthenticateRequest userObj);
        #endregion AUTHENTICATE

        #region REGISTER USER
        Task RegisterUser([FromBody] UserDto userObj);
        #endregion REGISTER USER
    }
}
