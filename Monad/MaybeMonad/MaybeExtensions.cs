using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monad.MaybeMonad
{
    public static class MaybeExtensions
    {
        /// <summary>
        /// Converts value to an Maybe<T>.
        /// </summary>
        public static Maybe<T> ToMaybe<T>(this T self)
        {
            return self == null
                ? Maybe<T>.None
                : Maybe<T>.Some(self);
        }

        /// <summary>
        /// Flattens a sequence of Maybe into one sequence of T
        /// elements where the value of the monad was Some
        /// </summary>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<Maybe<T>> source)
        {
            return source
                .Where(maybe => maybe.IsSome)
                .Select(maybe => maybe.Value);
        }

        /// <summary>
        /// Converts maybe to nullable if it has struct type
        /// </summary>
        public static T? ToNullable<T>(this Maybe<T> self) where T : struct =>
            self.IsNone
                ? (T?) null
                : self.Value;


        /// <summary>
        /// Map Maybe T to Maybe R
        /// </summary>
        /// <returns></returns>
        public static Maybe<R> Map<T, R>(this Maybe<T> self, Func<T, R> mapper) =>
            self.IsSome
                ? Maybe<R>.Some(mapper(self.Value))
                : Maybe<R>.None;

        /// <summary>
        /// FlatMap Maybe T to Maybe R
        /// </summary>
        /// <returns></returns>
        public static R FlatMap<T, R>(this Maybe<T> self, Func<T, R> mapper) =>
            self.IsSome
                ? Maybe<R>.Some(mapper(self.Value)).Value
                : default(R);

        /// <summary>
        /// Map Maybe T to Maybe R
        /// </summary>
        public static Maybe<R> Map<T, R>(this Maybe<T> self, Func<T, R> some, Func<R> none) =>
            self.IsSome
                ? Maybe<R>.Some(some(self.Value))
                : none();

        /// <summary>
        /// Filter maybe values
        /// </summary>
        public static Maybe<T> Filter<T>(this Maybe<T> self, Func<T, bool> pred) =>
            self.IsSome
                ? pred(self.Value)
                    ? self
                    : Maybe<T>.None
                : self;

        /// <summary>
        /// Bind function to Maybe
        /// </summary>
        public static Maybe<R> Bind<T, R>(this Maybe<T> self, Func<T, Maybe<R>> binder) =>
            self.IsSome
                ? binder(self.Value)
                : Maybe<R>.None;

        /// <summary>
        /// Use map instead
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Maybe<U> Select<T, U>(this Maybe<T> self, Func<T, U> map) =>
            self.Map(map);

        /// <summary>
        /// Use filter instead
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Maybe<T> Where<T>(this Maybe<T> self, Func<T, bool> pred) =>
            self.Filter(pred)
                ? self
                : Maybe<T>.None;

    }
}
