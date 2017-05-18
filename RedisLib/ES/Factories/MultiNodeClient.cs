using Elasticsearch.Net;
using Nest;
using System;
using System.Linq;

namespace CoreLib.ES.Factories
{
    public static class MultiNodeClient
    {
        public static ElasticClient GetClient(string[] urls)
        {
            ElasticClient client = null;

            var elasticNodes = new SniffingConnectionPool(urls.Select(o => new Uri(o)));

            ConnectionSettings con = new ConnectionSettings(elasticNodes);
            client = new ElasticClient(con);

            return client;
        }
    }
}
