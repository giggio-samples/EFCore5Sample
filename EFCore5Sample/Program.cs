using EFCore5Sample;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

var podcastTag = new Tag { Text = "Podcast" };
var csharpTag = new Tag { Text = "C#" };
var functionalProgrammingTag = new Tag { Text = "Programação funcional" };
var dotnetTag = new Tag { Text = ".NET" };

var context = new BlogContext(
    new DbContextOptionsBuilder<BlogContext>()
    .UseSqlite(CreateInMemoryDatabase())
    .LogTo(WriteLine, LogLevel.Information).Options);
context.Database.EnsureCreated();

context.AddRange(
    new Post { Name = "Lambda3 Podcast 220 – Programação Funcional Parte 2 – Functors e Monads", Tags = new List<Tag> { podcastTag, functionalProgrammingTag, dotnetTag } },
    new Post { Name = "Onde C# dá overflow?", Tags = new List<Tag> { csharpTag, dotnetTag } });

context.SaveChanges();

var originalForegroundColor = ForegroundColor;
ForegroundColor = ConsoleColor.Blue;
var posts = context.Posts.Include(e => e.Tags).ToList();
ForegroundColor = ConsoleColor.DarkGreen;
WriteLine("\nPosts:");
foreach (var post in posts)
    WriteLine($"Post \"{post.Name}\" has tags {post.Tags.Aggregate("", (acc, tag) => acc == "" ? tag.Text : $"{acc}, {tag.Text}")}");
ForegroundColor = originalForegroundColor;

static SqliteConnection CreateInMemoryDatabase()
{
    var connection = new SqliteConnection("Filename=:memory:");
    connection.Open();
    return connection;
}
