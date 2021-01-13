using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Uow
{
    public interface IServiceScope : IDisposable
    {
        T GetRequiredService<T>();
    }
}
