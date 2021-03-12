using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {            
        }

        public DbSet<AppUser> Users {get;set;}
        public DbSet<UserLike> Likes {get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Unique key combination for UserLike
            builder.Entity<UserLike>()
                .HasKey(k => new { k.SourceUserId, k.LikedUserId});

            // Specify: the relations
            // One SourceUser can have many LikeUsers
            builder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // A likeUser can have many likeByUsers
            builder.Entity<UserLike>()
                .HasOne(s => s.LikedUser)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);                
        }
    }
}
