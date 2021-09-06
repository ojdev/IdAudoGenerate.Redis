using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace IdAutoGenerate.Redis
{
    public class IdAutoGenerateFactory
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IdAutoGenerateFactoryConfigBuild _build;
        private readonly IDatabase db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionMultiplexer"></param>
        /// <param name="build"></param>
        public IdAutoGenerateFactory(IConnectionMultiplexer connectionMultiplexer, IdAutoGenerateFactoryConfigBuild build)
        {
            _connectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
            _build = build ?? throw new ArgumentNullException(nameof(build));
            db = _connectionMultiplexer.GetDatabase(_build.DB ?? -1);
        }
        /// <summary>
        /// 设置初始值
        /// </summary>
        /// <param name="key">指定的key</param>
        /// <param name="value"></param>
        /// <param name="force">即使已经存在也强制设置</param>
        /// <returns></returns>
        public async Task SetInitialSeedAsync(string key, long value, bool force = false)
        {
            var keyExists = await db.KeyExistsAsync(key);
            if (!keyExists || force)
            {
                await db.StringSetAsync(key, value);
            }
        }
        /// <summary>
        /// 设置初始值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="force">即使已经存在也强制设置</param>
        /// <returns></returns>
        public async Task SetInitialSeedAsync(long value, bool force = false)
        {
            await SetInitialSeedAsync(_build.DefaultKey, value, force);
        }
        public async Task<long> GetIncrementAsync(string key)
        {
            if (!await db.KeyExistsAsync(key))
            {
                throw new ArgumentNullException($"{key}不存在");
            }
            return await db.StringIncrementAsync(key);
        }
        public async Task<long> GetIncrementAsync()
        {
            return await GetIncrementAsync(_build.DefaultKey);
        }
        /// <summary>
        /// 获取一种编号
        /// </summary>
        /// <param name="key">redis中存储编号的key</param>
        /// <param name="length">长度</param>
        /// <param name="padfix">补位的字符串</param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public async Task<string> GetCode(string key, int length, char paddingChar = '0', string prefix = null)
        {
            var increment = await GetIncrementAsync(key);
            var code = increment.ToString();
            return $"{prefix}{code.PadLeft(length, paddingChar)}";
        }
        /// <summary>
        /// 获取一种编号
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="paddingChar">补位的字符</param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public async Task<string> GetCode(int length, char paddingChar = '0', string prefix = null)
        {
            return await GetCode(_build.DefaultKey, length, paddingChar, prefix);
        }
    }
}
