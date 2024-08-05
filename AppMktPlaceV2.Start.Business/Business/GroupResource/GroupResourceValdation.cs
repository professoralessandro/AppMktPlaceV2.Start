namespace AppMktPlaceV2.Start.Business.Business.GroupResource
{
    public static class GroupResourceValdation
    {
        #region INSERT
        public static async Task<string> ValidInsert(this GroupResourceDto model, IGroupResourceService _service)
        {
            return string.Empty;
        }
        #endregion

        #region UPDATE
        public static string ValidUpdate(this GroupResourceDto model)
        {
            return string.Empty;
        }
        #endregion

        #region DELETE
        public static string ValidDelete(this GrupoRecurso model)
        {
            return string.Empty;
        }
        #endregion
    }
}
