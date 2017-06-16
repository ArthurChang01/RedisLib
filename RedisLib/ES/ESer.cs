using CoreLib.ES.Factories;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLib.ES
{
    public class ESer : IESer
    {
        private static ElasticClient _client = null;

        #region Constructor
        public ESer(string urlString)
        {
            if (_client == null)
                _client = SingleNodeClient.GetClient(urlString);
        }

        public ESer(params string[] urlString)
        {
            if (_client == null)
                _client = MultiNodeClient.GetClient(urlString);
        }
        #endregion

        #region BulkInsert
        public void BulkInsert<T>(IEnumerable<T> input) where T : class, new()
        {
            _client.Bulk(new BulkRequest()
            {
                Operations = input.Select(o =>
                    (new BulkIndexOperation<T>(o)) as IBulkOperation
                ).ToList()
            });
        }

        public async Task BulkInsertAsync<T>(IEnumerable<T> input) where T : class, new()
        {
            await _client.BulkAsync(new BulkRequest()
            {
                Operations = input.Select(o =>
                    (new BulkIndexOperation<T>(o)) as IBulkOperation
                ).ToList()
            });
        }
        #endregion

        #region FetchById
        public T FetchById<T>(string idxName, DocumentPath<T> id)
            where T : class, new()
        {
            T result = null;

            var resp = _client.Get<T>(id, idx => idx.Index(idxName));
            result = resp.Source;

            return result;
        }

        public async Task<T> FetchByIdAsync<T>(string idxName, DocumentPath<T> id)
            where T : class, new()
        {
            T result = null;

            var resp = await _client.GetAsync<T>(id, idx => idx.Index(idxName));
            result = resp.Source;

            return result;
        }
        #endregion

        #region FetchByPaging
        public IEnumerable<T> FetchByPaging<T>(int start, int pageSize, string idxName,
            Func<QueryContainerDescriptor<T>, QueryContainer> query, Func<ISearchResponse<T>, IEnumerable<T>> mapping)
            where T : class, new()
        {
            IEnumerable<T> result = null;

            var resp = _client.Search<T>(s =>
                s.Index(idxName)
                 .From(start)
                 .Size(pageSize)
                 .Query(query));

            result = mapping(resp);

            return result;
        }

        public async Task<IEnumerable<T>> FetchByPagingAsync<T>(int start, int pageSize, string idxName,
            Func<QueryContainerDescriptor<T>, QueryContainer> query, Func<ISearchResponse<T>, IEnumerable<T>> mapping)
            where T : class, new()
        {
            IEnumerable<T> result = null;

            var resp = await _client.SearchAsync<T>(s =>
                s.Index(idxName)
                 .From(start)
                 .Size(pageSize)
                 .Query(query));

            result = mapping(resp);

            return result;
        }
        #endregion



    }
}
