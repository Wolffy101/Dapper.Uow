using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Uow
{
    public class UnitOfWorkEventArgs : EventArgs
    {
        /// <summary>
        /// Reference to the unit of work related to this event.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        public UnitOfWorkEventArgs( IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
