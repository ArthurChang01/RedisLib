using RedisLib.Core.Enums;
using RedisLib.Core.Exceptions;
using RedisLib.Core.Factories.Client;
using RedisLib.Core.Factories.Connction;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisLib.Core
{
    public class Rediser
    {
        #region Members
        private static int counter = 0;
        ICacheClient _client = null;
        #endregion

        #region Constructor
        public Rediser(string conString) : this(conString, SerializerType.Json) { }

        public Rediser(string conString, SerializerType serializerType)
        {
            IConnectionMultiplexer opt = ConnectionFactory.ConcretConnection(conString);
            this._client = RedisClientFactory.ConcretRedisClient(opt, serializerType);
        }
        #endregion

        #region Property
        public ICacheClient Client { get { return _client; } private set { _client = value; } }
        #endregion

        #region Public Methods
        public void PublishMessage<T>(string channelName, T message)
        {
            try
            {
                this._client.Publish<T>(channelName, message, CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new PublishException(ex);
            }
        }

        public async Task PublishMessageAsync<T>(string channelName, T message)
        {
            try
            {
                await this._client.PublishAsync<T>(channelName, message, CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new PublishException(ex);
            }
        }

        public void SubscribeMessage<T>(string channelName, Action<T> action)
        {
            try
            {
                this._client.Subscribe<T>(channelName, action, CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new SubscribeException(ex);
            }
        }

        public async Task SubscribeMessageAsync<T>(string channelName, Func<T, Task> action)
        {
            try
            {
                await this._client.SubscribeAsync<T>(channelName, action, CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new SubscribeException(ex);
            }
        }

        public void UnSubscribeMessage<T>(string channelName, Action<T> action)
        {
            try
            {
                this._client.Unsubscribe<T>(channelName, action, CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new SubscribeException(ex);
            }
        }

        public async Task UnSubscribeMessageAsync<T>(string channelName, Func<T, Task> action)
        {
            try
            {
                await this._client.UnsubscribeAsync<T>(channelName, action, CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new SubscribeException(ex);
            }
        }

        public void Save<T>(string key, T value, TimeSpan? expiredTime = null)
        {
            try
            {
                if (expiredTime.HasValue)
                    this._client.Add<T>(key, value, expiredTime.Value);
                else
                    this._client.Add<T>(key, value);
            }
            catch (Exception ex)
            {
                throw new SaveException(ex);
            }
        }

        public async Task SaveAsync<T>(string key, T value, TimeSpan? expiredTime = null)
        {
            try
            {
                if (expiredTime.HasValue)
                    await this._client.AddAsync<T>(key, value, expiredTime.Value);
                else
                    await this._client.AddAsync<T>(key, value);
            }
            catch (Exception ex)
            {
                throw new SaveException(ex);
            }
        }

        public IEnumerable<string> Receive(string keyPattern)
        {
            IEnumerable<string> result = null;

            try
            {
                IEnumerable<string> ieKeys = this._client.SearchKeys(keyPattern);
                result = this._client.GetAll<string>(ieKeys).Values;
                this._client.RemoveAll(ieKeys);
            }
            catch (Exception ex)
            {
                throw new ReceiveException(ex);
            }

            return result;
        }

        public async Task<IEnumerable<T>> ReceiveAsync<T>(string keyPattern)
        {
            IEnumerable<T> result = null;

            try
            {
                IEnumerable<string> ieKeys = await this._client.SearchKeysAsync(keyPattern);
                result = (await this._client.GetAllAsync<T>(ieKeys)).Values;
            }
            catch (Exception ex)
            {
                throw new ReceiveException(ex);
            }

            return result;
        }

        public bool KeyExist(string key)
        {
            bool blExisit = false;

            try
            {
                IEnumerable<string> ieKeys = this._client.SearchKeys(key);
                blExisit = ieKeys != null && ieKeys.LongCount() > 0;
            }
            catch (Exception ex)
            {
                throw new KeyExistException(ex);
            }

            return blExisit;
        }

        public async Task<bool> KeyExistAsync(string key)
        {
            bool blExist = false;

            try
            {
                IEnumerable<string> ieKeys = await this._client.SearchKeysAsync(key);
                blExist = ieKeys != null && ieKeys.LongCount() > 0;
            }
            catch (Exception ex)
            {
                throw new KeyExistException(ex);
            }

            return blExist;
        }

        public IDictionary<string, T> GetHashTable<T>(string hashKey)
        {
            IDictionary<string, T> result = null;

            try
            {
                result = this._client.HashGetAll<T>(hashKey);
            }
            catch (Exception ex)
            {
                throw new GetHashTableException(ex);
            }

            return result;
        }

        public async Task<IDictionary<string, T>> GetHashTableAsync<T>(string hashKey)
        {
            IDictionary<string, T> result = null;

            try
            {
                result = await this._client.HashGetAllAsync<T>(hashKey);
            }
            catch (Exception ex)
            {
                throw new GetHashTableException(ex);
            }

            return result;
        }

        public void SetHashTable_Plus(string hashKey, string key, int value = 1)
        {
            try
            {
                this._client.HashIncerementBy(hashKey, key, value, CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new SetHashTablePlusException(ex);
            }
        }

        public async Task SetHashTable_PlusAsync(string hashKey, string key, int value = 1)
        {
            try
            {
                await this._client.HashIncerementByAsync(hashKey, key, value, CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new SetHashTablePlusException(ex);
            }
        }

        public void SetHashTable<T>(string hashKey, string key, T value)
        {
            try
            {
                _client.HashSet<T>(hashKey, key, value, commandFlags: CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new SetHashTable(ex);
            }
        }

        public async Task SetHashTableAsync<T>(string hashKey, string key, T value)
        {
            try
            {
                await _client.HashSetAsync<T>(hashKey, key, value, commandFlags: CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new SetHashTable(ex);
            }
        }
        #endregion
    }
}
