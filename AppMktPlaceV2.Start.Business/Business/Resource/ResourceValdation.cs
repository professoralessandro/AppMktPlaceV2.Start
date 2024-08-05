using AppMktPlaceV2.Application.Dtos;
using AppMktPlaceV2.Start.Business.Entities;
using AppMktPlaceV2.Start.Business.Interfaces.Services.Resource;

namespace AppMktPlaceV2.Start.Business.Business.Resource
{
    public static class ResourceValdation
    {
        #region INSERT
        public static async Task<string> ValidInsert(this ResourceDto model, IResourceService _service)
        {
            return string.Empty;
        }
        #endregion

        #region UPDATE
        public static string ValidUpdate(this ResourceDto model)
        {
            return string.Empty;
        }
        #endregion

        #region DELETE
        public static string ValidDelete(this Recurso model)
        {
            return string.Empty;
        }
        #endregion
    }
}
