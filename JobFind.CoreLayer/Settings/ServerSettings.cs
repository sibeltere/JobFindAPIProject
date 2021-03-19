using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.CoreLayer.Settings
{
    public class ServerSettings
    {
        public MongoDbSettings MongoDB { get; set; } = new MongoDbSettings();
    }
}
