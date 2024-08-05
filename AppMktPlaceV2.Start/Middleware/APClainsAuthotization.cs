using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        public RequestClainFilther(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // REMOVER COMENTARIO APOS IMPLEMENTAR AUTORIZACAO
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

            var userEmail = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("identity/claims/emailaddress"))?.Value;

            if (userEmail == null)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            //var result = Task.Run(async () => await _userRepository.GetUserByEmail(userEmail)).GetAwaiter().GetResult();
            //var user = result.First();

            //if (!user.Department.ToUpper().Contains("COMPLIANCE"))
            //{
            //    context.Result = new StatusCodeResult(403);
            //    return;
            //}
        }
    }
}
