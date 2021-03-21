using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.CoreLayer.Settings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string Database { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string ConnectionString => $@"mongodb://root:example@{Host}:{Port}/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false";


    }
}
