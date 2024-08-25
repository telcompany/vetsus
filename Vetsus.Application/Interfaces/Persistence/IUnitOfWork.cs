namespace Vetsus.Application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        public ICustomerRepository Customers { get; }

        void BeginTransaction();
        void Commit();
        void CommitAndCloseConnection();
        void Rollback();
    }
}
