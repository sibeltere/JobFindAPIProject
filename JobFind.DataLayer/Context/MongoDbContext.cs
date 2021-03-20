using JobFind.CoreLayer.Settings;
using JobFind.DataLayer.Entities;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace JobFind.DataLayer.Context
{
    public class MongoDbContext:IMongoDbContext
    {
        #region Fields
        private MongoClient _mongodbClient;
        private readonly IMongoDatabase _db;
        #endregion

        #region CTOR
        public MongoDbContext(MongoDbSettings config)
        {
            _mongodbClient = new MongoClient(config.ConnectionString);
            _db = _mongodbClient.GetDatabase(config.Database);
        }
        #endregion

        #region Methods
        public IMongoCollection<T> GetCollection<T>() where T : BaseEntity
        {
            var table = typeof(T).GetCustomAttribute<TableAttribute>(false).Name;
            return _db.GetCollection<T>(table);

        }
        #endregion
    }
}
