using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser: IdentityUser<int>
    {
        // Defined in IdentityUser
        // public int Id { get; set; }
        // public string UserName { get; set; }
        // public byte[] PasswordHash { get; set; }
        // public byte[] PasswordSalt { get; set; }

        public DateTime DateOfBirth { get; set; }
        
        public string KnownAs { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public string Gender { get; set; }

        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string  Interests { get; set; }
        public string  City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }

        // Users who like you
        public ICollection<UserLike> LikedByUsers { get; set; }
        
        // And users you like
        public ICollection<UserLike> LikedUsers { get; set; }

        public ICollection<Message> MessagesSend { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
