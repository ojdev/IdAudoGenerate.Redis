using IdAutoGenerate.Redis;
using StackExchange.Redis;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        private static IdAutoGenerateFactoryConfigBuild build;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Redis链接字符串</param>
        /// <param name="db">默认的db</param>
        /// <param name="defaultKey">默认的key</param>
        /// <returns></returns>
        public static IServiceCollection AddIdAutoGenerateFactory(this IServiceCollection services, string configuration, int db = -1, string defaultKey = null)
        {
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration));
            build = new IdAutoGenerateFactoryConfigBuild();
            if (!string.IsNullOrWhiteSpace(defaultKey))
            {
                build.SetDefaultKey(defaultKey);
            }
            build.SetDB(db);
            services.AddSingleton(build);
            services.AddSingleton<IdAutoGenerateFactory>();
            return services;
        }
    }
}
