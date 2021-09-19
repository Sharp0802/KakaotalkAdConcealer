using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace KakaotalkAdConcealer.Core.Common
{
    /// <summary>
    /// Force T to be reference type
    /// </summary>
    /// <typeparam name="T">Type of value that to be reference type</typeparam>
    internal class ForceRef<T> : IEquatable<ForceRef<T>> where T : struct
    {
        /// <summary>
        /// Value of reference
        /// </summary>
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

        [method: SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() 
            => Value.GetHashCode();
    }
}
