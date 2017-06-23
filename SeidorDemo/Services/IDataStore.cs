using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeidorDemo
{
    public interface IDataStore<T>
    {
        Task<IEnumerable<T>> GetItemsAsync();
    }
}
