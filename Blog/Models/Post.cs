using Dapper.Contrib.Extensions;

namespace Blog.Models
{
    [Table("[Post]")]
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
