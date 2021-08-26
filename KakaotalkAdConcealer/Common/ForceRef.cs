﻿using System;
using System.Runtime.CompilerServices;

namespace KakaotalkAdConcealer.Common
{
    public class ForceRef<T> : IEquatable<ForceRef<T>> where T : struct
    {
        public T Value { get; set; }

        public static bool operator ==(ForceRef<T> one, ForceRef<T> other) => Equals(one, other);
        public static bool operator !=(ForceRef<T> one, ForceRef<T> other) => !Equals(one, other);

        [method: MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static bool Equals(ForceRef<T> one, ForceRef<T> other) => 
            ReferenceEquals(one, other) || one is not null && other is not null && one.Value.Equals(other.Value);

        public bool Equals(ForceRef<T> other)
            => Equals(this, other);

        public override bool Equals(object obj)
            => Equals(this, obj);

        public override int GetHashCode() 
            => Value.GetHashCode();
    }
}
