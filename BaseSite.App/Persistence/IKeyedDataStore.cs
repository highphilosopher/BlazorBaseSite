using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSite.App.Persistence
{
    interface IKeyedDataStore<TKey, TValue>: IDictionary<TKey, TValue>
    {
        event EventHandler Updated;

    }
}
