using System;
using System.Collections;
using System.Collections.Generic;

namespace Monad.Maybe
{
    /// <summary>
    /// Represents base case of Maybe monad
    /// </summary>
    public abstract class MaybeResult<T> : IEnumerable<T>, IEquatable<MaybeResult<T>>
    {
        /// <summary>
        /// Monad value
        /// </summary>
        public abstract T    Value { get; }

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
        /// <param name="another">The value to return in case of empty Maybe</param>
        /// <returns>If monad has value returns it otherwise returns another</returns>
        public T GetValueOr(T another)
        {
            return HasValue ? Value : another;
        }

        /// <summary>
        /// Returns monad value or result of function
        /// </summary>
        /// <param name="another">The function to get result in case of empty Maybe</param>
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
        /// Conversion from any value to MaybeResult<T>
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>If value is null returns Nothing othervise returns new Maybe</returns>
        public static implicit operator MaybeResult<T>(T value)
        {
            return value == null
                ? NothingResult<T>.Default
                : new SomeResult<T>(value);
        }

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
        /// <param name="other"></param>
        /// <returns>True if equal</returns>
        public bool Equals(MaybeResult<T> other)
        {
            return HasValue == other.HasValue && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        #endregion
    }
}
