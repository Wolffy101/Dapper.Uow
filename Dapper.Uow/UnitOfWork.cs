using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dapper.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;
        public UnitOfWork(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public IUnitOfWorkOptions Options { get; private set; }

        public IUnitOfWork Outer { get; private set; }

        public bool IsReserved { get; set; }

        public bool IsDisposed { get; private set; }

        public bool IsCompleted { get; private set; }

        public IDbConnection GetDbConnection => _dbConnection;

        private IDbTransaction _dbTransaction;
        public IDbTransaction GetDbTransaction => _dbTransaction;

        public Guid Id { get; } = Guid.NewGuid();

        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;
        public event EventHandler<UnitOfWorkEventArgs> Disposed;


        public void Commit()
        {
            _dbTransaction.Commit();
        }

        public void Initialize(UnitOfWorkOptions options)
        {
            if (Options != null)
            {
                throw new Exception("This unit of work is already initialized before!");
            }
            Options = options.Clone();
            IsReserved = false;
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
                _dbTransaction = Options.IsolationLevel == null ? _dbConnection.BeginTransaction() : _dbConnection.BeginTransaction(Options.IsolationLevel.GetValueOrDefault());
            }
        }

        public void RollBack()
        {
            _dbTransaction.Rollback();
        }

        public void SetOuter(IUnitOfWork outer)
        {
            Outer = outer;
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;
            _dbTransaction?.Dispose();
            _dbTransaction?.Dispose();
            Disposed?.Invoke(this, new UnitOfWorkEventArgs(this));
        }
    }
}
