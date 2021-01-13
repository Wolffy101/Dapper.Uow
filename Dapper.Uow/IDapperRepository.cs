using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dapper.Uow
{
    public interface IDapperRepository : IDisposable
    {
        IDbConnection GetDbConnection();
        IDbTransaction GetDbTransaction();
    }
}
