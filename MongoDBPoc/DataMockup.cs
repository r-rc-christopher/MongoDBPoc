using MongoDBPoc.Domain;
using System;
using System.Collections.Generic;

namespace MongoDBPoc
{
    public static class DataMockup
    {
        public static IEnumerable<Post> GetPosts(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new Post
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = Faker.Lorem.Sentence(),
                    Content = Faker.Lorem.Paragraph(),
                    PublishedAt = DateTime.Now,
                };
            }
        }

        public static IEnumerable<Comment> GetComments(int count)
        {
            for(var i = 0; i < count; i++)
            {
                yield return new Comment
                {
                    Id = Guid.NewGuid().ToString(),
                    Author = Faker.Internet.UserName(),
                    Content = Faker.Lorem.Paragraph(),
                    PublishedAt = DateTime.Now.AddMinutes(Faker.RandomNumber.Next(5, 60))
                };
            }
        }
    }
}
