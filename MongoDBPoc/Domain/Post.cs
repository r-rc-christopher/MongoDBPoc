using System;
using System.Collections.Generic;

namespace MongoDBPoc.Domain
{
    public class Post : Entity
    {
        public Post()
        {
            Comments = new List<Comment>();
        }

        public string Title { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
