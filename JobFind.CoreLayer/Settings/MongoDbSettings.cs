using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.CoreLayer.Settings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string Database { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConnectionString => $@"mongodb+srv://{UserName}:{Password}@cluster0.zrvvz.mongodb.net/{Database}?retryWrites=true&w=majority";


    }
}
