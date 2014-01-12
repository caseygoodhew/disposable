using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disposable.Packages.Core;

namespace Disposable.DataAccess
{
    internal interface IStoredProcedureAdapter<T> where T : IPreparedStoredProcedure
    {
        T Prepare(IStoredProcedure storedProcedure);
    }
}
