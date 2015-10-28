using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Monad.PartialApplication;
using static Monad.Curry;

namespace Monad.MaybeMonad
{
    public static class MaybeExtensions
    {
        /// <summary>
        /// Converts value to an Maybe<T>.
        /// </summary>
        public static Maybe<T> ToMaybe<T>(this T self) =>
            self == null
                ? Maybe<T>.None
                : Maybe<T>.Some(self);

        /// <summary>
        /// Flattens a sequence of Maybe into one sequence of T
        /// elements where the value of the monad was Of
        /// </summary>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<Maybe<T>> self) =>
            self.Where(maybe => maybe.IsSome).Select(maybe => maybe.Value);

        /// <summary>
        /// Converts maybe to nullable if it has struct type
        /// </summary>
        public static T? ToNullable<T>(this Maybe<T> self) where T : struct =>
            self.IsNone
                ? (T?) null
                : self.Value;

        /// <summary>
        /// Apply maybe value to maybe function of arity 1
        /// </summary>
        public static Maybe<R> Apply<T, R>(this Maybe<Func<T, R>> self, Maybe<T> arg) =>
            self.IsSome && arg.IsSome
                ? Maybe<R>.Some(self.Value(arg.Value))
                : Maybe<R>.None;

        /// <summary>
        /// Apply maybe value to maybe function of arity 2
        /// </summary>
        public static Maybe<R> Apply<T1, T2, R>(this Maybe<Func<T1, T2, R>> self, Maybe<T1> arg1, Maybe<T2> arg2) =>
            self.IsSome && arg1.IsSome && arg2.IsSome
                ? Maybe<R>.Some(self.Value(arg1.Value, arg2.Value))
                : Maybe<R>.None;

        /// <summary>
        /// Partial apply maybe value to maybe function of arity 2 by using partial apply
        /// </summary>
        public static Maybe<Func<T2, R>> Apply<T1, T2, R>(this Maybe<Func<T1, T2, R>> self, Maybe<T1> arg) =>
            self.IsSome && arg.IsSome
                ? Maybe<Func<T2, R>>.Some(papply(self.Value, arg.Value))
                : Maybe<Func<T2, R>>.None;


        /// <summary>
        /// Map Maybe T to Maybe R with mapper of arity 1
        /// </summary>
        /// <returns></returns>
        public static Maybe<R> Map<T, R>(this Maybe<T> self, Func<T, R> mapper) =>
            self.IsSome
                ? Maybe<R>.Some(mapper(self.Value))
                : Maybe<R>.None;

        /// <summary>
        /// Map Maybe T to Maybe R with mapper of arity 2
        /// by currying
        /// </summary>
        public static Maybe<Func<T2, R>> Map<T1, T2, R>(this Maybe<T1> self, Func<T1, T2, R> mapper) =>
            self.Map(curry(mapper));

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
        public static Maybe<R> Map<T, R>(this Maybe<T> self, Func<T, R> onSome, Func<R> onNone) =>
            self.IsSome
                ? Maybe<R>.Some(onSome(self.Value))
                : onNone();

        /// <summary>
        /// Filter maybe values
        /// </summary>
        public static Maybe<T> Filter<T>(this Maybe<T> self, Func<T, bool> predicate) =>
            self.IsSome
                ? predicate(self.Value)
                    ? self
                    : Maybe<T>.None
                : self;

        /// <summary>
        /// Returns true if value is not null and matches predicate
        /// </summary>
        public static bool Exists<T>(this Maybe<T> self, Func<T, bool> predicate) =>
            self.IsSome && predicate(self.Value);

        /// <summary>
        /// If value in not null calls onSome, else onNone
        /// </summary>
        public static bool Exists<T>(this Maybe<T> self, Func<T, bool> onSome, Func<bool> onNone) =>
            self.IsSome
                ? onSome(self.Value)
                : onNone();

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
        public static Maybe<R> Select<T, R>(this Maybe<T> self, Func<T, R> mapper) =>
            self.Map(mapper);

        /// <summary>
        /// Use filter instead
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Maybe<T> Where<T>(this Maybe<T> self, Func<T, bool> predicate) =>
            self.Filter(predicate)
                ? self
                : Maybe<T>.None;
    }
}