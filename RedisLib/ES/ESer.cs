using CoreLib.ES.Factories;
using Nest;
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

    }
}
