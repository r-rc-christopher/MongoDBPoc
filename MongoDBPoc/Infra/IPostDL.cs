using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBPoc.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBPoc.Infra
{
    public interface IPostDL
    {
        Task AddPost(Post post);
        Task<IEnumerable<Post>> GetPosts();
    }

    public class MongoPostDL : IPostDL
    {
        public IUnitOfWork Context { get; }
        private readonly IMongoCollection<BsonDocument> _collection;

        public MongoPostDL(MongoDBContext context)
        {
            Context = context;
            _collection = context.Database.GetCollection<BsonDocument>("Posts");
        }

        public async Task AddPost(Post post)
        {
            var doc = post.ToBsonDocument();
            // Replace plain object comments by references
            doc.Set(nameof(post.Comments), new BsonArray(post.Comments.Select(x => x.Id)));
            await _collection.InsertOneAsync(doc);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _collection
                 .Aggregate()
                 .Lookup<BsonDocument, Post>("Comments", "Comments", "_id", "Comments")
                 .ToListAsync();
        }
    }
}
