using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EFCore5Sample
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions options) : base(options) { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
