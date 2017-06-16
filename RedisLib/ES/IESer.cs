using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace CoreLib.ES
{
    public interface IESer
    {
        void BulkInsert<T>(IEnumerable<T> input) where T : class, new();
        Task BulkInsertAsync<T>(IEnumerable<T> input) where T : class, new();
        T FetchById<T>(string idxName, DocumentPath<T> id) where T : class, new();
        Task<T> FetchByIdAsync<T>(string idxName, DocumentPath<T> id) where T : class, new();
        IEnumerable<T> FetchByPaging<T>(int start, int pageSize, string idxName, Func<QueryContainerDescriptor<T>, QueryContainer> query, Func<ISearchResponse<T>, IEnumerable<T>> mapping) where T : class, new();
        Task<IEnumerable<T>> FetchByPagingAsync<T>(int start, int pageSize, string idxName, Func<QueryContainerDescriptor<T>, QueryContainer> query, Func<ISearchResponse<T>, IEnumerable<T>> mapping) where T : class, new();
    }
}