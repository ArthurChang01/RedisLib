using RedisLoggerLib.Enums;
using RedisLoggerLib.Exceptions;
using RedisLoggerLib.Factories.Client;
using RedisLoggerLib.Factories.Connction;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisLoggerLib
{
    public class RedisLogger
    {
        #region Members
        ICacheClient _client = null;
        string _channelName = string.Empty;
        #endregion

        #region Constructor
        public RedisLogger(string conString, string channelName) : this(conString, channelName, SerializerType.Json) { }

        public RedisLogger(string conString, string channelName, SerializerType serializerType) {
            IConnectionMultiplexer opt = ConnectionFactory.ConcretConnection(conString);
            this._client = RedisClientFactory.ConcretRedisClient(opt, serializerType);
        }
        #endregion

        #region Public Methods
        public void SubscribeMessage<T>(Action<T> action) {
            try
            {
                this._client.Subscribe<T>(this._channelName, action);
            }
            catch (Exception ex)
            {
                throw new SubscribeException(ex);
            }
        }

        public async Task SubscribeMessageAsync<T>(Func<T,Task> action)
        {
            try
            {
                await this._client.SubscribeAsync<T>(this._channelName, action);
            }
            catch (Exception ex)
            {
                throw new SubscribeException(ex);
            }
        }

        public void UnSubscribeMessage<T>(Action<T> action) {
            try
            {
                this._client.Unsubscribe<T>(this._channelName, action);
            }
            catch (Exception ex)
            {
                throw new SubscribeException(ex);
            }
        }

        public async Task UnSubscribeMessageAsync<T>(Func<T,Task> action)
        {
            try
            {
                await this._client.UnsubscribeAsync<T>(this._channelName, action);
            }
            catch (Exception ex)
            {
                throw new SubscribeException(ex);
            }
        }

        public void SaveLog<T>(RedisKey key, T value,TimeSpan? expiredTime= null,bool stopPublish=false) {
            try {
                if (expiredTime.HasValue)
                    this._client.Add<T>(key, value, expiredTime.Value);
                else
                    this._client.Add<T>(key, value);

                if(!stopPublish)
                    this._client.Publish<T>(this._channelName, value, CommandFlags.FireAndForget);
            }
            catch (Exception ex) {
                throw new SaveLogException(ex);
            }
        }

        public async Task SaveLogAsync<T>(RedisKey key, T value, TimeSpan? expiredTime = null, bool stopPublish = false)
        {
            try {
                if (expiredTime.HasValue)
                    await this._client.AddAsync<T>(key, value, expiredTime.Value);
                else
                    await this._client.AddAsync<T>(key, value);

                if (!stopPublish)
                    await this._client.PublishAsync<T>(this._channelName, value, CommandFlags.FireAndForget);
            }
            catch (Exception ex) {
                throw new SaveLogException(ex);
            }
        }

        public T ReceiveLog<T>(RedisKey key) {
            T result=default(T);

            try {
                result = this._client.Get<T>(key);
            }
            catch (Exception ex) {
                throw new ReceiveLogException(ex);
            }

            return result;
        }

        public async Task<T> ReceiveLogAsync<T>(RedisKey key){
            T result = default(T);

            try
            {
                result = await this._client.GetAsync<T>(key);
            }
            catch (Exception ex)
            {
                throw new ReceiveLogException(ex);
            }

            return result;
        }
        #endregion
    }
}
