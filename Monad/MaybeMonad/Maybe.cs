using System;
using System.Collections;
using System.Collections.Generic;

namespace Monad.MaybeMonad
{
    /// <summary>
    /// Maybe monade main class
    /// </summary>
    public abstract class Maybe<T> : IEnumerable<T>, IEquatable<Maybe<T>>, IEquatable<T>
    {
        /// <summary>
        /// Represents a Maybe monad without a value
        /// </summary>
        public static readonly Maybe<T> Nothing = new Nothing<T>();

        /// <summary>
        /// Monad value
        /// </summary>
        public abstract T Value { get; }

        protected Maybe()
        {
        }

        /// <summary>
        /// Does monad have value
        /// </summary>
        public abstract bool HasValue { get; }

        /// <summary>
        /// Returns monad value or default value of parametric type
        /// </summary>
        /// <returns>If monad has value returns it otherwise returns default</returns>
        public T GetValueOrDefault()
        {
            return HasValue ? Value : default(T);
        }

        /// <summary>
        /// Returns monad value or another specified by user
        /// </summary>
        /// <param name="another">The value to return in case of empty MaybeLazy</param>
        /// <returns>If monad has value returns it otherwise returns another</returns>
        public T GetValueOr(T another)
        {
            return HasValue ? Value : another;
        }

        /// <summary>
        /// Returns monad value or result of function
        /// </summary>
        /// <param name="another">The function to get result in case of empty MaybeLazy</param>
        /// <returns>If monad has value returns it otherwise returns another result</returns>
        public T GetValueOr(Func<T> another)
        {
            return HasValue ? Value : another();
        }

        /// <summary>
        /// Tries to get value from monad
        /// </summary>
        /// <param name="output">The varibale to store result. If monad has no value result will be default</param>
        /// <returns>Returns true if monad has value otherwise returns false</returns>
        public bool TryGetValue(out T output)
        {
            output = HasValue ? Value : default(T);
            return HasValue;
        }

        /// <summary>
        /// Conversion from any value to Maybe<T>
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>If value is null returns Nothing othervise returns new MaybeLazy</returns>
        public static implicit operator Maybe<T>(T value)
        {
            return value == null
                ? Nothing<T>.Default
                : Some<T>.Of(value);
        }

        /// <summary>
        /// Compares two monads for inequality.
        /// </summary>
        /// <param name="lhs">Left hand side monad</param>
        /// <param name="rhs">Right hand side monad</param>
        /// <returns>True if the monad's values are equal otherwise returns false</returns>
        public static bool operator ==(Maybe<T> lhs, Maybe<T> rhs) => lhs.Equals(rhs);

        /// <summary>
        /// Compares two monads for inequality.
        /// </summary>
        /// <param name="lhs">Left hand side monad</param>
        /// <param name="rhs">Right hand side monad</param>
        /// <returns>True if the monad's values are not equal otherwise returns false</returns>
        public static bool operator !=(Maybe<T> lhs, Maybe<T> rhs) => !lhs.Equals(rhs);

        #region IEnumerable<T> Members

        /// <summary>
        /// Returns enumerator thar iterates over the monad
        /// </summary>
        /// <returns>An iterator that will yield value</returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (HasValue)
                yield return Value;
        }

        /// <summary>
        /// Returns enumerator thar iterates over the monad
        /// </summary>
        /// <returns>An iterator that will yield value</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IEquatalbe<T> Members

        /// <summary>
        /// Compares two monads for equality
        /// </summary>
        /// <param name="other">Monade to compare with</param>
        /// <returns>True if equal</returns>
        public bool Equals(Maybe<T> other)
        {
            if (this.HasValue != other.HasValue)
                return false;

            if (!this.HasValue)
                return true;

            return EqualityComparer<T>.Default.Equals(this.Value, other.Value);
        }

        /// <summary>
        /// Compares monad with value
        /// </summary>
        /// <returns>True if equal</returns>
        public bool Equals(T other)
        {
            if (!HasValue && other == null)
                return true;
            return EqualityComparer<T>.Default.Equals(Value, other);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Compares to objects for equality
        /// </summary>
        /// <param name="other">The object to compare to.</param>
        /// <returns>True if other object is MaybeLazy and both values are equal</returns>
        public override bool Equals(object other)
        {
            var casted = other as Maybe<T>;
            return (casted != null && Equals(casted));
        }

        /// <summary>
        /// Generates hash code of the value
        /// </summary>
        /// <returns>Hash code of the value</returns>
        public override int GetHashCode()
        {
            return HasValue ? EqualityComparer<T>.Default.GetHashCode() : 0;
        }

        #endregion
    }

    /// <summary>
    /// Helper for creating generic Maybe instances
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        /// Creates MaybeLazy from value
        /// </summary>
        public static Maybe<T> Create<T>(T value)
        {
            return value == null
                ? MaybeMonad.Nothing<T>.Default
                : Some<T>.Of(value);
        }

        /// <summary>
        /// Creates Maybe from nullable
        /// </summary>
        public static Maybe<T> Create<T>(T? value) where T : struct
        {
            return !value.HasValue
                ? MaybeMonad.Nothing<T>.Default
                : Some<T>.Of(value.Value);
        }
    }
}