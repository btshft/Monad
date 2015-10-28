using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Monad.Prelude;

namespace Monad.MaybeMonad
{
    /// <summary>
    /// Represent a Some value. It cant be null
    /// </summary>
    public struct Some<T> : IMaybe
    {
        private readonly T    _value;

        public Some(T value)
        {
            _value = ReturnIfInitialised(value);
        } 

        public T Value =>
            ReturnIfInitialised(_value);

        public bool IsSome
            => (_value != null);

        public bool IsNone 
            => !IsSome;

        /// <summary>
        /// Helper function. Returns input if it's not null. otherwise throws exception
        /// </summary>
        private static R ReturnIfInitialised<R>(R value) =>
            value == null
                ? Raise<R>(new ArgumentNullException($"{typeof(R)}"))
                : value;

        #region Operators

        public static implicit operator Maybe<T>(Some<T> value) =>
            Maybe<T>.Some(value.Value);

        public static implicit operator Some<T>(T value) =>
            new Some<T>(value);

        public static implicit operator T(Some<T> value) =>
            value.Value;

        public static bool operator ==(Some<T> lhs, Some<T> rhs) => lhs.Equals(rhs);

        public static bool operator !=(Some<T> lhs, Some<T> rhs) => !lhs.Equals(rhs);

        public static bool operator ==(Some<T> lhs, T rhs) => lhs.Equals(rhs);

        public static bool operator !=(Some<T> lhs, T rhs) => !lhs.Equals(rhs);

        #endregion

        #region Overrides

        public override string ToString() =>
            Value.ToString();

        public override int GetHashCode() =>
            EqualityComparer<T>.Default.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj == null) {
                return false;
            }

            // Compare to another Some
            if (obj is Some<T>)
            {
                var rhs = (Some<T>)obj;
                return Value.Equals(rhs.Value);
            }

            // Compare to Value
            if (obj is T)
            {
                var rhs = (T)obj;
                return Value.Equals(rhs);
            }
            return false;
        }
    
        #endregion

    }

    /// <summary>
    /// Helper for creating generic Some instances
    /// </summary>
    public static class Some
    {
        public static Some<T> Of<T>(T x) =>
            new Some<T>(x);

        public static Some<T> Of<T>(T? x) where T : struct =>
            new Some<T>(x.Value);
    }
}
