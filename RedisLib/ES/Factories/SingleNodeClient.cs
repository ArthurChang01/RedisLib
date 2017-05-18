using Nest;
using System;

namespace CoreLib.ES.Factories
{
    public static class SingleNodeClient
    {
        public static ElasticClient GetClient(string urlString)
        {
            ElasticClient client = null;

            ConnectionSettings con = new ConnectionSettings(new Uri(urlString));
            client = new ElasticClient(con);

            return client;
        }
    }
}
