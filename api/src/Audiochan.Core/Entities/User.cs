﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Audiochan.Core.Entities
{
    public class User : IdentityUser<long>
    {
        public User()
        {
            Audios = new HashSet<Audio>();
            FavoriteAudios = new HashSet<FavoriteAudio>();
            Followings = new HashSet<FollowedUser>();
            Followers = new HashSet<FollowedUser>();
            Roles = new HashSet<UserRole>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public string DisplayName { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
        
        public Profile Profile { get; set; } = null!;

        public ICollection<Audio> Audios { get; set; }
        public ICollection<FavoriteAudio> FavoriteAudios { get; set; }
        public ICollection<FollowedUser> Followings { get; set; }
        public ICollection<FollowedUser> Followers { get; set; }
        public ICollection<UserRole> Roles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
