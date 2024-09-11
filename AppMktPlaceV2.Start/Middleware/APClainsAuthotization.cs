#region ATRIBUTTES
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AppMktPlaceV2.Start.Application.Helper.Static.User;
using AppMktPlaceV2.Start.Domain.Interfaces.Services.BlackListToken;
using AppMktPlaceV2.Start.Api.Enums.Enum;
#endregion ATRIBUTTES

namespace AppMktPlaceV2.Start.Api.Middleware
{
    public class APClainsAuthotization
    {
        // ESTE METODO PEGA A CLAIN DE HTTPCONTEXT E VALIDA O VALOR SEPARADO POR VIRGULA
        // CASO O VALOR EXISTA NA CLAIN SEPARADA POR VIRGULA ENTAO O USUARIO ESTA PERMITIRO A USAR O METODO
        // METODO QUE CHAMA ESTA VALIDACAO COMENTADA, PARA USO FUTURO

        public static bool ValidateUserAutorization(HttpContext httpContext, string clainName, string clainValue)
        {
            return httpContext.User.Identity.IsAuthenticated
                && httpContext.User.Claims.Any(c => c.Type == clainName && c.Value.Split(",").Contains(clainValue));
        }
    }

    public class APClainsAuthorizeAttribute : TypeFilterAttribute
    {
        public APClainsAuthorizeAttribute(string clainName, string clainValue) : base(typeof(RequestClainFilther))
        {
            Arguments = new object[] { new Claim(clainName, clainValue) };
        }

        public APClainsAuthorizeAttribute() : this("", "")
        {

        }
    }

    public class RequestClainFilther : IAuthorizationFilter
    {
        private readonly Claim _claim;
        private readonly IBlackListTokenService _blackListService;

        public RequestClainFilther(Claim claim, IBlackListTokenService blackListService)
        {
            _claim = claim;
            _blackListService = blackListService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // VALIDATE IF IS ANNONYMUS USER
            if (this._claim.Type == ClainEnum.AnonymousType && this._claim.Value == ClainEnum.AnonymousValue && !context.HttpContext.User.Identity.IsAuthenticated) return;

            // VALIDATE IF IS COMMON CONTROLLER AUTORIZE
            if (this._claim.Type == ClainEnum.CommonControllerClainType && this._claim.Value == ClainEnum.CommonControllerClainValue && !context.HttpContext.User.Identity.IsAuthenticated) return;

            var result = _blackListService.CheckTokenInBlackList(
                    context.HttpContext.Request.Headers["Authorization"]
                    .ToString().Replace("Bearer ", string.Empty)).Result;

            // BLACK LIST TOKEN VALIDATION
            if (result)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            // USER ID VALIDATION
            if (!Guid.TryParse(context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value, out Guid userId))
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            // SETTING USER ID
            UserHelper.UserId = userId;

            //TODO: REMOVER COMENTARIO APOS IMPLEMENTAR AUTORIZACAO
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            ////if (!CtmCustomAuthorization.ValidateUserAutorization(context.HttpContext, _clain.Type, _clain.Value))
            ////{
            ////    context.Result = new StatusCodeResult(403);
            ////    return;
            ////}


            //var result = Task.Run(async () => await _userRepository.GetUserByEmail(userEmail)).GetAwaiter().GetResult();
            //var user = result.First();
        }
    }
}
