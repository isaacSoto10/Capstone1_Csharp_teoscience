using Microsoft.EntityFrameworkCore;

namespace HobbyCenter.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get;set;}
        public DbSet<Hobby> Hobbys { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostHasTags> PostHasTags { get; set; }
    }
}