using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.Configs
{
    public class MongoDBConfig
    {
        public string Database { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string ConnectionString
        {
            get { return $@"mongodb://{Host}:{Port}"; }
        }
    }
}
