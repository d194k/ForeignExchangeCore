using System;
using System.Collections.Generic;
using System.Text;

namespace Forex.Entities.UOW
{
    public interface IUnitOfWork:IDisposable
    {
        void Commit();
    }
}
