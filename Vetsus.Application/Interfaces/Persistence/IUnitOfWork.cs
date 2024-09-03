namespace Vetsus.Application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        public IOwnerRepository Owners { get; }
        public IUserRepository Users { get; }
        public IPetRepository Pets { get; }
        public ISpeciesRepository Species { get; }
        public IVetRepository Vets { get; }

        void BeginTransaction();
        void Commit();
        void CommitAndCloseConnection();
        void Rollback();
    }
}
