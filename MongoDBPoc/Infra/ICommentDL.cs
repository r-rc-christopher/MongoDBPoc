using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBPoc.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDBPoc.Infra
{
    public interface ICommentDL
    {
        Task AddComment(Comment comment);
        Task<IEnumerable<Comment>> GetComments();
    }

    public class MongoCommentDL : ICommentDL
    {
        public IUnitOfWork Context { get; }
        private readonly IMongoCollection<BsonDocument> _collection;

        public MongoCommentDL(MongoDBContext context)
        {
            Context = context;
            _collection = context.Database.GetCollection<BsonDocument>("Comments");
        }

        public async Task AddComment(Comment comment)
        {
            var doc = comment.ToBsonDocument();
            await _collection.InsertOneAsync(doc);
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            var comments = 
                await _collection
                    .Aggregate()
                    .Lookup("Posts", "_id", "Comments", "Post")
                    .Unwind("Post")
                    .Project<Comment>(new BsonDocument{{ "Post.Comments", 0 }})
                    .ToListAsync();

            return comments;
        }
    }
}
