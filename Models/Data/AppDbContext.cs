using Microsoft.EntityFrameworkCore;
using Blogovic.Models.Entity;

namespace Blogovic.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext( DbContextOptions<AppDbContext>options) : base(options)
        {
        }

        public DbSet<User>  Users { get; set; }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User(1, "yasir@blogovic.com"));
            modelBuilder.Entity<Article>().Property(x => x.CreatedTime).HasDefaultValueSql("getutcdate()");
        }
    }
}
