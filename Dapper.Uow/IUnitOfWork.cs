using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dapper.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        Guid Id { get; }

        IDbConnection GetDbConnection { get; }
        IDbTransaction GetDbTransaction { get; }

        IUnitOfWork Outer { get; }

        bool IsReserved { get; }

        bool IsDisposed { get; }

        bool IsCompleted { get; }

        void Commit();
        void RollBack();

        void SetOuter(IUnitOfWork unitOfWork);

        void Initialize(UnitOfWorkOptions unitOfWorkOptions);

        //TODO: Switch to OnFailed (sync) and OnDisposed (sync) methods to be compatible with OnCompleted
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        event EventHandler<UnitOfWorkEventArgs> Disposed;
    }
}
