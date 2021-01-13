using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;

namespace Dapper.Uow
{
    public interface IDis : IDisposable
    {

    }
    public class Dis : IDis
    {
        public void Dispose()
        {
            Console.WriteLine("-----------------------");
        }
    }
    public class DapperRepository : IDapperRepository
    {
        private readonly IAmbientUnitOfWork _ambientUnitOfWork;

        private readonly IServiceScope _serviceScope;
        public DapperRepository(IAmbientUnitOfWork ambientUnitOfWork, IServiceScopeFactory serviceScopeFactory)
        {
            _ambientUnitOfWork = ambientUnitOfWork;
            _serviceScope = serviceScopeFactory.CreateScope();
        }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }

        public IDbConnection GetDbConnection()
        {
            if (_ambientUnitOfWork.UnitOfWork == null)
            {
                _serviceScope.GetRequiredService<IDis>();
                return _serviceScope.GetRequiredService<IDbConnection>();
            }
            return _ambientUnitOfWork.UnitOfWork.GetDbConnection;
        }

        public IDbTransaction GetDbTransaction()
        {
            return _ambientUnitOfWork.UnitOfWork?.GetDbTransaction;
        }
    }
}
