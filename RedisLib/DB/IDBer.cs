using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLib.DB
{
    public interface IDBer
    {
        void BulkInsert<T>(IList<T> collection);
        Task BulkInsertAsync<T>(IList<T> collection);
    }
}