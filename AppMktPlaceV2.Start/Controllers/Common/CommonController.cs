#region REFERENCES
using AppMktPlaceV2.Start.Api.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
#endregion REFERENCES

namespace AppMktPlaceV2.Start.Api.Controllers.Common
{
    [ApiController, Route("api/[controller]"), Authorize, APClainsAuthorize("CommonControllerUser", "CommonControllerValue"), ApiVersion("1.0")]
    public class CommonController : ControllerBase
    {
    }
}
