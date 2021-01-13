using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Uow
{
    internal class ChildUnitOfWork : IUnitOfWork
    {
        private readonly IUnitOfWork _parent;
        public ChildUnitOfWork(IUnitOfWork parent)
        {
            _parent = parent;

            _parent.Failed += (sender, args) => { Failed?.Invoke(sender, args); };
            _parent.Disposed += (sender, args) => { Disposed?.Invoke(sender, args); };
        }

        public IUnitOfWork Outer => _parent.Outer;

        public bool IsReserved => _parent.IsReserved;

        public bool IsDisposed => _parent.IsDisposed;

        public bool IsCompleted => _parent.IsCompleted;

        public IDbConnection GetDbConnection => _parent.GetDbConnection;

        public IDbTransaction GetDbTransaction => _parent.GetDbTransaction;

        public Guid Id => _parent.Id;

        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;
        public event EventHandler<UnitOfWorkEventArgs> Disposed;

        public void Commit()
        {
        }

        public void Dispose()
        {
        }

        public void Initialize(UnitOfWorkOptions options)
        {
            _parent.Initialize(options);
        }

        public void RollBack()
        {
            _parent.RollBack();
        }

        public void SetOuter(IUnitOfWork outer)
        {
            _parent.SetOuter(outer);
        }
    }
}
