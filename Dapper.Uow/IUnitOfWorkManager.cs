using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Uow
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork Current { get; }

        IUnitOfWork Begin(UnitOfWorkOptions options, bool requiresNew = false);
    }
}
