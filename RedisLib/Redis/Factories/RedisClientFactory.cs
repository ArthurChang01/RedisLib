using CoreLib.Redis.Enums;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Jil;
using StackExchange.Redis.Extensions.Newtonsoft;
using StackExchange.Redis.Extensions.Protobuf;
using System.Diagnostics.CodeAnalysis;

namespace CoreLib.Redis.Factories
{
    [ExcludeFromCodeCoverage]
    class RedisClientFactory
    {
        public static ICacheClient ConcretRedisClient(IConnectionMultiplexer con, SerializerType serializerType = SerializerType.JillJson)
        {
            ICacheClient client = null;

            if (serializerType == SerializerType.JillJson)
                client = new StackExchangeRedisCacheClient(con, new JilSerializer());
            else if (serializerType == SerializerType.ProtoBuf)
                client = new StackExchangeRedisCacheClient(con, new ProtobufSerializer());
            else if (serializerType == SerializerType.NewtonJson)
                client = new StackExchangeRedisCacheClient(con, new NewtonsoftSerializer());

            return client;
        }
    }
}
