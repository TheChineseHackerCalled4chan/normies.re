using Microsoft.EntityFrameworkCore;

namespace NormiesRe.Models
{
    public class NormiesDbContext: DbContext
    {
        public NormiesDbContext(DbContextOptions<NormiesDbContext> options)
            : base(options)
        { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
