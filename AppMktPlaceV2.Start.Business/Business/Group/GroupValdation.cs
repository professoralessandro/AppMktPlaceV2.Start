namespace AppMktPlaceV2.Start.Business.Business.Group
{
    public static class GroupValdation
    {
        #region INSERT
        public static async Task<string> ValidInsert(this GroupDto model, IGroupService _service)
        {
            return string.Empty;
        }
        #endregion

        #region UPDATE
        public static string ValidUpdate(this GroupDto model)
        {
            return string.Empty;
        }
        #endregion

        #region DELETE
        public static string ValidDelete(this Grupo model)
        {
            return string.Empty;
        }
        #endregion
    }
}
