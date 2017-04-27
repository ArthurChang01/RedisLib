using RedisLib.Core.Enums;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Jil;
using StackExchange.Redis.Extensions.Protobuf;

namespace RedisLib.Core.Factories.Client
{
    class RedisClientFactory
    {
        public static ICacheClient ConcretRedisClient(IConnectionMultiplexer con, SerializerType serializerType = SerializerType.Json)
        {
            ICacheClient client = null;

            if (serializerType == SerializerType.Json)
                client = new StackExchangeRedisCacheClient(con, new JilSerializer());
            else if (serializerType == SerializerType.ProtoBuf)
                client = new StackExchangeRedisCacheClient(con, new ProtobufSerializer());

            return client;
        }
    }
}
