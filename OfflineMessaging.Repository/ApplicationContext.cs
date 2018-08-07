using Microsoft.EntityFrameworkCore;
using OfflineMessaging.Data;

namespace OfflineMessaging.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<BlockList> BlockList { get; set; }
    }
}
