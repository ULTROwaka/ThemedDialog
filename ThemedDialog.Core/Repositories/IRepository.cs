using System;
using System.Collections.Generic;
using System.Text;

namespace ThemedDialog.Core.Repositories
{
    public interface IRepository<T, KeyT>
    {
        bool Add(T value);
        T Get(KeyT key);
    }
}
