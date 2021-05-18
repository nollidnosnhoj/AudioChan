﻿using Audiochan.Core.Entities.Base;

namespace Audiochan.Core.Entities
{
    public class FollowedUser
    {
        public string ObserverId { get; set; } = null!;
        public User Observer { get; set; } = null!;
        public string TargetId { get; set; } = null!;
        public User Target { get; set; } = null!;
        public long Id { get; set; }
    }
}