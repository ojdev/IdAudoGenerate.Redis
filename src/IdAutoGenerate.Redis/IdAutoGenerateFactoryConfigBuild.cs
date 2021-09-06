using System;

namespace IdAutoGenerate.Redis
{
    public class IdAutoGenerateFactoryConfigBuild
    {
        public int? DB { get; private set; }
        public string DefaultKey { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public IdAutoGenerateFactoryConfigBuild()
        {
        }
        public void SetDB(int db)
        {
            DB = db;
        }
        public void SetDefaultKey(string key)
        {
            DefaultKey = key;
        }
    }
}
