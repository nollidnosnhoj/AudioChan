﻿using System;

namespace Audiochan.Core.Common.Attributes
{
    /// <summary>
    /// Specifies the class this attribute is applied to requires authorization
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AuthorizeAttribute : Attribute
    {
        public AuthorizeAttribute()
        {
            
        }
    }
}