namespace AppMktPlaceV2.Start.Domain.Interfaces.Data
{
    public interface IAPDWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
