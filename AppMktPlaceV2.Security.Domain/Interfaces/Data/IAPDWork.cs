namespace AppMktPlaceV2.Security.Domain.Interfaces.Data
{
    public interface IAPDWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
