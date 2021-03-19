using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.CoreLayer.Settings
{
    public interface IMongoDbSettings
    {
        string Database { get; set; }
        string Host { get; set; }
        int Port { get; set; }
        string ConnectionString { get; set; }
    }
}
