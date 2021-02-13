using System;

namespace MongoDBPoc.Domain
{
    public class Comment : Entity
    {
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public Post Post { get; set; }
    }
}
