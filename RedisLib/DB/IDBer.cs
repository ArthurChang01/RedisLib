using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLib.DB
{
    public interface IDBer
    {
        void BulkInsert<T>(IList<T> collection);
        Task BulkInsertAsync<T>(IList<T> collection);
        Task<IEnumerable<T>> Fetch<T>(string sql, object parm = null, Func<T> objFactory = null) where T : class, new();
        Task<T> FetchByIdAsync<T>(string sql, object param);
    }
}