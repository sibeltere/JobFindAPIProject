using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.CoreLayer.Settings
{
    public interface IMongoDbSettings
    {
        string Database { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string ConnectionString { get;}
    }
}
