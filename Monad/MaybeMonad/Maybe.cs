using System;
using System.Collections;
using System.Collections.Generic;
using static Monad.Prelude;

namespace Monad.MaybeMonad
{
    [Serializable]
    public struct Maybe<T> :
        IMaybe, IEnumerable<T>,
        IEquatable<Maybe<T>>, IEquatable<T>
    {
        /// <summary>
        /// Value stored in maybe
        /// </summary>
        private readonly T _value;

        /// <summary>
        /// Closed c-tor
        /// </summary>
        /// <param name="value">Value to convert</param>
        private Maybe(T value)
        {
            IsSome = (value != null);
            _value = value;
        }

        /// <summary>
        /// Maybe(x) open ctor
        /// </summary>
        /// <param name="value">Value to convert</param>
        public static Maybe<T> Some(T value) =>
            new Maybe<T>(value);

        /// <summary>
        /// Represent a None state
        /// </summary>
        public static readonly Maybe<T> None
            = new Maybe<T>();

        /// <summary>
        /// True if Maybe has some value
        /// </summary>
        public bool IsSome { get; }

        /// <summary>
        /// True if Maybe has no value
        /// </summary>
        public bool IsNone => !IsSome;

        /// <summary>
        /// Returns a Maybe value if it's not null otherwise throws InvalidOperationException
        /// </summary>
        /// <exception cref="InvalidOperationException">Throws if value is null</exception>
        public T Value =>
            IsSome
                ? _value
                : Raise<T>(new InvalidOperationException("Value is null"));

        /// <summary>
        /// Returns monad value or default value of parametric type
        /// </summary>
        /// <returns>If monad has value returns it otherwise returns default</returns>
        public T GetValueOrDefault() =>
            IsSome ? Value : default(T);

        /// <summary>
        /// Helper function. Returns input if it's not null. otherwise throws exception
        /// </summary>
        public static R ValueOrException<R>(R value, string where) =>
            value == null
                ? Raise<R>(new ArgumentNullException($"'{where}' result is null.  Not allowed."))
                : value;

        #region Operators

        /// <summary>
        /// Conversion from any value to Maybe<T>
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>If value is null returns Nothing othervise returns new MaybeLazy</returns>
        public static implicit operator Maybe<T>(T value) =>
            value == null
                ? None
                : Some(value);

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

        /// <summary>
        /// Compares monad with value
        /// </summary>
        /// <param name="lhs">Left hand side monad</param>
        /// <param name="rhs">Right hand side value</param>
        /// <returns>True if the monad's value are equal to rhs otherwise returns false</returns>
        public static bool operator ==(Maybe<T> lhs, T rhs) => lhs.Equals(rhs);

        /// <summary>
        /// Compares monad with value
        /// </summary>
        /// <param name="lhs">Left hand side monad</param>
        /// <param name="rhs">Right hand side value </param>
        /// <returns>True if the monad's value are not equal to rhs otherwise returns false</returns>
        public static bool operator !=(Maybe<T> lhs, T rhs) => !lhs.Equals(rhs);

        /// <summary>
        /// Monad has value
        /// </summary>
        public static bool operator true(Maybe<T> value) =>
            value.IsSome;

        /// <summary>
        /// Monad has no value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool operator false(Maybe<T> value) =>
            value.IsNone;

        #endregion

        #region Match

        /// <summary>
        /// Match the two states of the Maybe
        /// </summary>
        public R Match<R>(Func<T, R> some, Func<R> none) =>
            IsSome
                ? ValueOrException(some(Value), "MaybeOf")
                : ValueOrException(none(), "None");

        /// <summary>
        /// Match the two states of the Maybe
        /// </summary>
        /// <param name="some">MaybeOf match</param>
        /// <param name="none">None match</param>>
        /// <returns>Unit</returns>
        public Unit Match(Action<T> some, Action none)
        {
            if (IsSome)
            {
                some(Value);
            }
            else
            {
                none();
            }
            return Unit.Default;
        }

        /// <summary>
        /// Invokes the someHandler if Maybe has value
        /// happens.
        /// </summary>
        public Unit IfSome(Action<T> someHandler)
        {
            if (IsSome)
            {
                someHandler(_value);
            }
            return Unit.Default;
        }

        /// <summary>
        /// Invokes the someHandler if Maybe has value
        /// </summary>
        public Unit IfSome(Func<T, Unit> someHandler)
        {
            if (IsSome)
            {
                someHandler(_value);
            }
            return Unit.Default;
        }

        /// <summary>
        /// Invokes the someHandler if Maybe has no value
        /// </summary>
        public T IfNone(Func<T> none) =>
            Match(Identity, none);

        /// <summary>
        /// Invokes the someHandler if Maybe has no value
        /// </summary>
        public T IfNone(T noneValue) =>
            Match(Identity, () => noneValue);

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// Returns enumerator thar iterates over the monad
        /// </summary>
        /// <returns>An iterator that will yield value</returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (IsSome)
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

        #region IEquatable Members

        /// <summary>
        /// Compares two monads for equality
        /// </summary>
        /// <param name="other">Monade to compare with</param>
        /// <returns>True if equal</returns>
        public bool Equals(Maybe<T> other)
        {
            if (IsSome != other.IsSome)
                return false;

            return !IsSome || EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        /// <summary>
        /// Compares monad with value
        /// </summary>
        /// <returns>True if equal</returns>
        public bool Equals(T other)
        {
            if (!IsSome && other == null)
                return true;
            return EqualityComparer<T>.Default.Equals(Value, other);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Compares to objects for equality
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            // Compare to another Maybe
            if (obj is Maybe<T>)
            {
                var rhs = (Maybe<T>) obj;
                return IsSome && rhs.IsSome
                    ? Value.Equals(rhs.Value)
                    : !IsSome && !rhs.IsSome;
            }

            // Compare to Value
            if (obj is T)
            {
                var rhs = (T) obj;
                return IsSome && Value.Equals(rhs);
            }
            return false;
        }

        /// <summary>
        /// Generates hash code of the value
        /// </summary>
        /// <returns>Hash code of the value</returns>
        public override int GetHashCode() =>
            IsSome ? EqualityComparer<T>.Default.GetHashCode() : 0;

        /// <summary>
        /// MaybeOf(null) or MaybeOf(x) or [Nothing]
        /// </summary>
        public override string ToString() =>
            IsSome
                ? Value == null
                    ? "MaybeOf(null)"
                    : $"Some({Value})"
                : "[Nothing]";

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
        public static Maybe<T> MaybeOf<T>(T value) =>
            value == null
                ? Maybe<T>.None
                : Maybe<T>.Some(value);

        /// <summary>
        /// Creates Maybe from nullable
        /// </summary>
        public static Maybe<T> MaybeOf<T>(T? nullable) where T : struct =>
            nullable.HasValue
                ? Maybe<T>.Some(nullable.Value)
                : Maybe<T>.None;
    }
}
