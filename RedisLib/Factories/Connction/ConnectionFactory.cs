using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Jil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisLoggerLib.Factories.Connction
{
    class ConnectionFactory
    {
        public static IConnectionMultiplexer ConcretConnection(string conString="") {
            IConnectionMultiplexer con = null;

            ConfigurationOptions opt = null;
            if (string.IsNullOrEmpty(conString))
                opt = ConfigurationOptions.Parse(conString);
            else
                opt = new ConfigurationOptions()
            {
                AllowAdmin = true,
                KeepAlive = 180,
                DefaultDatabase = 0,
                ConnectRetry = 3,
                ConnectTimeout = 3000,
                EndPoints = {
                    { "localhost",7000},
                    { "localhost",7001},
                    { "localhost",7002},
                    { "localhost",7003},
                    { "localhost",7004},
                    { "localhost",7005},
                },
                DefaultVersion = new Version("3.2.0")
            };

            con = ConnectionMultiplexer.Connect(opt);

            return con;
        }
    }
}
