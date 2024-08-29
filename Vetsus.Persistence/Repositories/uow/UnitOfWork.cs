using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Persistence.Contexts;

namespace Vetsus.Persistence.Repositories.uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly DapperDataContext _dapperDataContext;
        public IOwnerRepository Owners { get; private set; }
        public IUserRepository Users { get; private set; }
        public IPetRepository Pets { get; private set; }
        public ISpeciesRepository Species { get; private set; }
        public IVetRepository Vets { get; private set; }

        public UnitOfWork(DapperDataContext dapperDataContext)
        {
            _dapperDataContext = dapperDataContext;
            Init();
        }

        private void Init()
        {
            Owners = new OwnerRepository(_dapperDataContext);
            Users = new UserRepository(_dapperDataContext);
            Pets = new PetRepository(_dapperDataContext);
            Species = new SpeciesRepository(_dapperDataContext);
            Vets = new VetRepository(_dapperDataContext);
        }

        public void BeginTransaction()
        {
            _dapperDataContext.Connection?.Open();
            _dapperDataContext.Transaction = _dapperDataContext.Connection?.BeginTransaction();
        }

        public void Commit()
        {
            _dapperDataContext.Transaction?.Commit();
            _dapperDataContext.Transaction?.Dispose();
            _dapperDataContext.Transaction = null;
        }

        public void CommitAndCloseConnection()
        {
            _dapperDataContext.Transaction?.Commit();
            _dapperDataContext.Transaction?.Dispose();
            _dapperDataContext.Transaction = null;
            _dapperDataContext.Connection?.Close();
            _dapperDataContext.Connection?.Dispose();
        }

        public void Rollback()
        {
            _dapperDataContext.Transaction?.Rollback();
            _dapperDataContext.Transaction?.Dispose();
            _dapperDataContext.Transaction = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dapperDataContext.Transaction?.Dispose();
                    _dapperDataContext.Connection?.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
