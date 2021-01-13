using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Uow
{
    public interface IServiceScopeFactory
    {
        IServiceScope CreateScope();
    }
}
