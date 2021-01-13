using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Uow
{
    public interface IAmbientUnitOfWork
    {
        IUnitOfWork UnitOfWork { get; }

        void SetUnitOfWork(IUnitOfWork unitOfWork);
    }
}
