using Jil;
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
    public class Rediser : IRediser
    {
        #region Members
        private static int counter = 0;
        ICacheClient _client = null;
        private static readonly object _asyncObj = new object();
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

        #region Transaction
        public ITransaction CreateTransaction()
        {
            return this._client.Database.CreateTransaction(_asyncObj);
        }

        public async Task ExecuteTransaction(ITransaction tran)
        {
            await tran.ExecuteAsync(CommandFlags.FireAndForget);
        }
        #endregion

        #region Publish
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
        #endregion

        #region Subscribe
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
        #endregion

        #region Save data
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

        public async Task SaveAsyncWithTran<T>(string key, T value, ITransaction tran, TimeSpan? expiredTime = null)
        {
            if (tran == null)
                throw new ArgumentException("need ITransaction instance");

            string strVal = Jil.JSON.Serialize<T>(value);

            try
            {
                if (expiredTime.HasValue)
                    await tran.StringSetAsync(key, strVal, expiredTime.Value);
                else
                    await tran.StringSetAsync(key, strVal);
            }
            catch (Exception ex)
            {
                throw new SaveException(ex);
            }
        }
        #endregion

        #region Fetch data
        public IEnumerable<string> Fetch(string keyPattern)
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

        public async Task<IEnumerable<T>> FetchAsync<T>(string keyPattern)
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

        public async Task<IEnumerable<T>> FetchAsyncWithTran<T>(string keyPattern, ITransaction tran)
        {
            if (tran == null)
                throw new ArgumentException("need ITransaction instance");

            IEnumerable<T> result = null;

            try
            {
                string strVal = await tran.StringGetAsync(keyPattern);
                result = JSON.Deserialize<IEnumerable<T>>(strVal);
            }
            catch (Exception ex)
            {
                throw new ReceiveException(ex);
            }

            return result;
        }
        #endregion

        #region Key exist operation
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
                blExist = await this._client.Database.KeyExistsAsync(key);
            }
            catch (Exception ex)
            {
                throw new KeyExistException(ex);
            }

            return blExist;
        }

        public async Task<bool> KeyExistAsyncWithTran(string key, ITransaction tran)
        {
            if (tran == null)
                throw new ArgumentException("need ITransaction instance");

            bool blExist = false;

            try
            {
                blExist = await tran.KeyExistsAsync(key);
            }
            catch (Exception ex)
            {
                throw new KeyExistException(ex);
            }

            return blExist;
        }
        #endregion

        #region Get hash table
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

        public async Task<IDictionary<string, T>> GetHashTableAsyncWithTran<T>(string hashKey, ITransaction tran)
        {
            if (tran == null)
                throw new ArgumentException("need ITransaction instance");

            IDictionary<string, T> result = new Dictionary<string, T>();

            try
            {
                (await tran.HashGetAllAsync(hashKey)).ToList().ForEach(o =>
                    result.Add(o.Name, Jil.JSON.Deserialize<T>(o.Value))
                );
            }
            catch (Exception ex)
            {
                throw new GetHashTableException(ex);
            }

            return result;
        }
        #endregion

        #region Increment hash table value
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

        public async Task SetHashTable_PlusAsyncWithTran(string hashKey, string key, int value = 1, ITransaction tran = null)
        {
            if (tran == null)
                throw new ArgumentException("need ITransaction instance");

            try
            {
                await tran.HashIncrementAsync(hashKey, key, value, CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new SetHashTablePlusException(ex);
            }
        }
        #endregion

        #region Set hash table value
        public void SetHashTable<T>(string hashKey, string key, T value)
        {
            try
            {
                _client.HashSet<T>(hashKey, key, value, commandFlags: CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new SetHashTableException(ex);
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
                throw new SetHashTableException(ex);
            }
        }

        public async Task SetHashTableAsyncWithTran<T>(string hashKey, string key, T value, ITransaction tran)
        {
            if (tran == null)
                throw new ArgumentException("need ITransaction instance");

            string strVal = Jil.JSON.Serialize<T>(value);

            try
            {
                await tran.HashSetAsync(hashKey, key, strVal, flags: CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new SetHashTableException(ex);
            }
        }
        #endregion

        #region Push key data to buffer
        public void BufferingKey(string bufferName, string key)
        {
            try
            {
                this._client.ListAddToLeft(bufferName, key);
            }
            catch (Exception ex)
            {
                throw new BufferingKeyException(ex);
            }
        }

        public async Task BufferingKeyAsync(string bufferName, string key)
        {
            try
            {
                await this._client.ListAddToLeftAsync(bufferName, key);
            }
            catch (Exception ex)
            {
                throw new BufferingKeyException(ex);
            }
        }

        public async Task BufferingKeyAsyncWithTran(string bufferName, string key, ITransaction tran)
        {
            if (tran == null)
                throw new ArgumentException("need ITransaction instance");

            try
            {
                await tran.ListLeftPushAsync(bufferName, key, flags: CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                throw new BufferingKeyException(ex);
            }
        }
        #endregion

        #region Get key data from buffer
        public IEnumerable<string> GetBufferingKeyByRange(string bufferName, int start, int end)
        {
            IEnumerable<string> ieKeys = null;

            try
            {
                ieKeys = this._client.Database.ListRange(bufferName, start, end, flags: CommandFlags.FireAndForget).OfType<string>();
            }
            catch (Exception ex)
            {
                throw new GetBufferingKeyByRangeException(ex);
            }

            return ieKeys;
        }

        public async Task<IEnumerable<string>> GetBufferingKeyByRangeAsync(string bufferName, int start, int end)
        {
            IEnumerable<string> ieKeys = null;

            try
            {
                ieKeys = (await this._client.Database.ListRangeAsync(bufferName, start, end)).OfType<string>();
            }
            catch (Exception ex)
            {
                throw new GetBufferingKeyByRangeException(ex);
            }

            return ieKeys;
        }

        public async Task<IEnumerable<string>> GetBufferingKeyByRangeAsyncWithTran(string bufferName, int start, int end, ITransaction tran)
        {
            if (tran == null)
                throw new ArgumentException("need ITransaction instance");

            IEnumerable<string> ieKeys = null;

            try
            {
                ieKeys = (await tran.ListRangeAsync(bufferName, start, end, flags: CommandFlags.FireAndForget)).OfType<string>();
            }
            catch (Exception ex)
            {
                throw new GetBufferingKeyByRangeException(ex);
            }

            return ieKeys;
        }
        #endregion

        #endregion
    }
}
