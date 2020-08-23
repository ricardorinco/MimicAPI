namespace Mimic.Infra.Data.Transactions
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
