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
            con.DefaultIndex(DateTime.Now.ToString("yyyyMM"));

            client = new ElasticClient(con);

            return client;
        }
    }
}
