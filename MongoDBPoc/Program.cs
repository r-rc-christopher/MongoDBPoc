using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDBPoc.Domain;
using MongoDBPoc.Infra;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBPoc
{
    public class Program
    {
        private const string CONFIG_FILE = "appsettings.json";
        private static IConfiguration _configuration;
        private static IServiceProvider _services;

        public static void Main(string[] args)
        {
            _configuration =
                new ConfigurationBuilder()
                    .AddJsonFile(CONFIG_FILE)
                    .Build();


            _services =
                new ServiceCollection()
                    .AddScoped<MongoDBContext>()
                    .AddScoped<IPostDL, MongoPostDL>()
                    .AddScoped<ICommentDL, MongoCommentDL>()
                    .AddSingleton(typeof(IConfiguration), _configuration)
                    .BuildServiceProvider();

            RunAsync().Wait();
        }


        private static async Task RunAsync()
        {
            var post = DataMockup.GetPosts(1).First();
            var comments = DataMockup.GetComments(5).ToList();
            post.Comments.AddRange(comments);

            var postDL = _services.GetService<IPostDL>();
            var commentDL = _services.GetService<ICommentDL>();

            //await postDL.AddPost(post);
            //foreach (var comment in comments)
            //{
            //    await commentDL.AddComment(comment);
            //}

            var posts = await postDL.GetPosts();
            //var c = await commentDL.GetComments();
        }
    }
}
