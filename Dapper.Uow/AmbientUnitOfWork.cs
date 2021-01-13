using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dapper.Uow
{
    public class AmbientUnitOfWork : IAmbientUnitOfWork
    {
        public IUnitOfWork UnitOfWork => _currentUow.Value;

        private readonly AsyncLocal<IUnitOfWork> _currentUow;

        public AmbientUnitOfWork()
        {
            _currentUow = new AsyncLocal<IUnitOfWork>();
        }

        public void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            _currentUow.Value = unitOfWork;
        }
    }
}
