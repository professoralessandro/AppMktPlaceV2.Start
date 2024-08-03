#region REFERENCES
using AppMktPlaceV2.Security.Application.Dtos.Log;
using AppMktPlaceV2.Security.Domain.Interfaces.Services.Log;
#endregion REFERENCES

namespace AppMktPlaceV2.Security.Domain.Business.Log
{
    public static class LogValdation
    {
        #region INSERT
        public static async Task<string> ValidInsert(this LogDto model, ILogService _service)
        {
            return string.Empty;
        }
        #endregion

        #region UPDATE
        public static string ValidUpdate(this LogDto model)
        {
            return string.Empty;
        }
        #endregion

        #region DELETE
        public static string ValidDelete(this AppMktPlaceV2.Security.Domain.Entities.Log model)
        {
            return string.Empty;
        }
        #endregion
    }
}
