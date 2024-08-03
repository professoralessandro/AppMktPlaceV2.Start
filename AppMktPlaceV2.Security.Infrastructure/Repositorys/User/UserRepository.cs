#region ATRIBUTTES
using AppMktPlaceV2.Security.Infrastructure.Repositorys.Base;
using AppMktPlaceV2.Security.Application.Helper.Static.Generic;
using AppMktPlaceV2.Security.Domain.Context.SQLServer;
using AppMktPlaceV2.Security.Domain.Entities;
using Dapper;
using AppMktPlaceV2.Security.Domain.Interfaces.Repository.User;
using AppMktPlaceV2.Security.Domain.Connector;
#endregion

namespace AppMktPlaceV2.Security.Infrastructure.Repositorys.User
{
    public class UserRepository : RepositoryBase<Usuario>, IUserRepository
    {
        #region CONSTRUCTOR
        public UserRepository(AppDbContext context, APConnector session) : base(context, session)
        {
            _context = context;
            _session = session;
        }
        #endregion

        #region RETURN LIST WITH PARAMETERS PAGINATED
        public async Task<IEnumerable<T>> ReturnListWithParametersPaginated<T>(Guid? userId = null, Guid? id = null, string? userName = null, string? nome = null, string? nmrDocumento = null, string? email = null, bool? ativo = null, int? pageNumber = null, int? rowspPage = null)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@Id", !id.HasValue ? null : id);
            parameters.Add("@UserId", !userId.HasValue ? null : userId);
            parameters.Add("@Nome", nome == null || string.IsNullOrEmpty(nome) ? null : nome.RemoveInjections());
            parameters.Add("@UserName", userName == null || string.IsNullOrEmpty(userName) ? null : userName.RemoveInjections());
            parameters.Add("@NmrDocumento", nmrDocumento == null || string.IsNullOrEmpty(nmrDocumento) ? null : nmrDocumento.RemoveInjections());
            parameters.Add("@Email", email == null || string.IsNullOrEmpty(email) ? null : email.RemoveAllSpecialCaracterFromEmail());
            parameters.Add("@Ativo", !ativo.HasValue ? null : ativo.Value);
            parameters.Add("@PageNumber", pageNumber.HasValue ? pageNumber.Value : 1);
            parameters.Add("@RowspPage", rowspPage.HasValue ? rowspPage.Value : 10);

            var storedProcedure = "[seg].[UsuariosPaginated] @Id, @UserId, @UserName, @Nome, @NmrDocumento, @Email, @Ativo, @PageNumber, @RowspPage";

            return await this.ReturnListFromQueryAsync<T>(storedProcedure, parameters);
        }
        #endregion

        #region FIND USERS TO SELECT RETURN KEY AND VALUE
        public async Task<IEnumerable<T>> ReturnUsersToSelectAsync<T>(string? param = null, int? pageNumber = null, int? rowspPage = null)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@Param", param == null || string.IsNullOrEmpty(param) ? null : param.RemoveInjections());
            parameters.Add("@PageNumber", pageNumber.HasValue ? pageNumber.Value : 1);
            parameters.Add("@RowspPage", rowspPage.HasValue ? rowspPage.Value : 10);

            var storedProcedure = $@"[seg].[ReturnUsersToSelect] @Param, @PageNumber,@RowspPage";

            return await this.ReturnListFromQueryAsync<T>(storedProcedure, parameters);
        }
        #endregion
    }
}
