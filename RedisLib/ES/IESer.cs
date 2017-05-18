using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLib.ES
{
    public interface IESer
    {
        void BulkInsert<T>(IEnumerable<T> input) where T : class, new();
        Task BulkInsertAsync<T>(IEnumerable<T> input) where T : class, new();
    }
}