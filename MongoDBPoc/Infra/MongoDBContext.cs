using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDBPoc.Domain;
using System.Collections.Generic;
using System.Linq;

namespace MongoDBPoc.Infra
{
    public interface IUnitOfWork { }
    public class MongoDBContext : IUnitOfWork
    {
        public IMongoDatabase Database { get; }

        public MongoDBContext(IConfiguration configuration)
        {
            RegisterMappings();
            var client = new MongoClient(configuration["AppSettings:MongoURL"]);
            Database = client.GetDatabase(configuration["AppSettings:MongoDB"]);
        }

        private void RegisterMappings()
        {
            BsonClassMap.RegisterClassMap<Comment>(x =>
            {
                x.AutoMap();
                x.GetMemberMap(e => e.Post)
                    .SetIgnoreIfDefault(true);
            });
        }
    }
}
