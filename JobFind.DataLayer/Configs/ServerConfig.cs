using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.Configs
{
    public class ServerConfig
    {
        public MongoDBConfig MongoDB { get; set; } = new MongoDBConfig();
    }
}
