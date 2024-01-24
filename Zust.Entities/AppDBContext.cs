using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Zust.Entities
{
    public class AppDBContext:IdentityDbContext<AppUser,AppRole,string>
    {
        PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
        public AppDBContext(DbContextOptions<AppDBContext> options)
            :base(options)
        {
            
        }
        public DbSet<Friend>? Friends { get; set; }
        public DbSet<FriendRequest>? FriendRequests { get; set; }
        public DbSet<Notification>? Notifications { get; set; }
        public DbSet<Message>? Messages { get; set; }
        public DbSet<Chat>? Chats { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<Comment>? Comments { get; set; }
    }
}
