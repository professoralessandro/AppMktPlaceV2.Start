using AppMktPlaceV2.Domain.Interfaces.Services.Log;
using AppMktPlaceV2.Start.Application.Dtos.Log;

namespace AppMktPlaceV2.Start.Business.Business.Log
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
        public static string ValidDelete(this LogDto model)
        {
            return string.Empty;
        }
        #endregion
    }
}
