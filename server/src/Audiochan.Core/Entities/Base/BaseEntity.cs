﻿using System;

namespace Audiochan.Core.Entities.Base
{
    public abstract class BaseEntity : IEntity, IAudited
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}