﻿using System;

namespace Audiochan.Core.Entities.Abstractions;

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}

public abstract class AuditableEntity<TKey> : Entity<TKey>, IAuditable
    where TKey : IComparable<TKey>, IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}