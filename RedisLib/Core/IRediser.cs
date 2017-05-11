using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisLib.Core
{
    public interface IRediser
    {
        ICacheClient Client { get; }

        void BufferingKey(string bufferName, string key);
        Task BufferingKeyAsync(string bufferName, string key);
        Task BufferingKeyAsyncWithTran(string bufferName, string key, ITransaction tran);
        ITransaction CreateTransaction();
        Task ExecuteTransaction(ITransaction tran);
        IEnumerable<T> Fetch<T>(string keyPattern);
        Task<IEnumerable<T>> FetchAsync<T>(string keyPattern);
        Task<IEnumerable<T>> FetchAsyncWithTran<T>(string keyPattern, ITransaction tran);
        IEnumerable<string> GetBufferingKeyByRange(string bufferName, int start, int end);
        Task<IEnumerable<string>> GetBufferingKeyByRangeAsync(string bufferName, int start, int end);
        Task<IEnumerable<string>> GetBufferingKeyByRangeAsyncWithTran(string bufferName, int start, int end, ITransaction tran);
        IDictionary<string, T> GetHashTable<T>(string hashKey);
        Task<IDictionary<string, T>> GetHashTableAsync<T>(string hashKey);
        Task<IDictionary<string, T>> GetHashTableAsyncWithTran<T>(string hashKey, ITransaction tran);
        bool KeyExist(string key);
        Task<bool> KeyExistAsync(string key);
        Task<bool> KeyExistAsyncWithTran(string key, ITransaction tran);
        void PublishMessage<T>(string channelName, T message);
        Task PublishMessageAsync<T>(string channelName, T message);
        void Save<T>(string key, T value, TimeSpan? expiredTime = default(TimeSpan?));
        Task SaveAsync<T>(string key, T value, TimeSpan? expiredTime = default(TimeSpan?));
        Task SaveAsyncWithTran<T>(string key, T value, ITransaction tran, TimeSpan? expiredTime = default(TimeSpan?));
        void SetHashTable<T>(string hashKey, string key, T value);
        Task SetHashTableAsync<T>(string hashKey, string key, T value);
        Task SetHashTableAsyncWithTran<T>(string hashKey, string key, T value, ITransaction tran);
        void SetHashTable_Plus(string hashKey, string key, int value = 1);
        Task SetHashTable_PlusAsync(string hashKey, string key, int value = 1);
        Task SetHashTable_PlusAsyncWithTran(string hashKey, string key, int value = 1, ITransaction tran = null);
        void SubscribeMessage<T>(string channelName, Action<T> action);
        Task SubscribeMessageAsync<T>(string channelName, Func<T, Task> action);
        void UnSubscribeMessage<T>(string channelName, Action<T> action);
        Task UnSubscribeMessageAsync<T>(string channelName, Func<T, Task> action);
    }
}