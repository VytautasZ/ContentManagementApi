
using Microsoft.EntityFrameworkCore;

namespace ContentManagementApi;

public class BlogsContext : DbContext
{
    public BlogsContext(DbContextOptions<BlogsContext> options)
       : base(options)
    {
    }
    
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Blog>()
            .HasIndex(e => e.Name);

        modelBuilder
            .Entity<Account>()
            .Ignore(e => e.Details);
        
        modelBuilder
            .Entity<Account>()
            .Property(e => e.DetailsJson)
            .HasColumnName("Details");
        
    }
}
