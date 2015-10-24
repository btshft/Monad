using System;
using System.Collections.Generic;
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
                ? Nothing<T>.Default
                : Some<T>.Of(self);
        }

        /// <summary>
        /// Flattens a sequence of Maybe into one sequence of T
        /// elements where the value of the monad was Some
        /// </summary>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<Maybe<T>> source)
        {
            return from maybe in source
                where maybe.HasValue
                select maybe.Value;
        }

        /// <summary>
        /// Allows to do conversion of source if it's not null
        /// </summary>
        /// <returns>Converted object which returns action</returns>
        public static Maybe<TR> Select<TS, TR>(this Maybe<TS> source, Func<TS, TR> function)
        {
            return source.HasValue ? function(source.Value) : Maybe<TR>.Nothing;
        }

        /// <summary>
        /// Allows to do conversion of source if its not null and catch any exceptions
        /// </summary>
        /// <returns>Tuple which contains Converted object and info about exception (if it throws)</returns>
        public static Tuple<Maybe<TR>, Exception> TrySelect<TS, TR>(this Maybe<TS> source, Func<TS, TR> function)
        {
            var result = Maybe<TR>.Nothing;

            if (!source.HasValue)
                return new Tuple<Maybe<TR>, Exception>(result, null);

            try
            {
                result = function(source.Value);
                return new Tuple<Maybe<TR>, Exception>(result, null);
            }
            catch (Exception ex)
            {
                return new Tuple<Maybe<TR>, Exception>(result, ex);
            }
        }

        /// <summary>
        /// Handle exception with handler
        /// </summary>
        public static Maybe<TS> Catch<TS>(this Tuple<Maybe<TS>, Exception> source, Action<Exception> handler = null)
        {
            if (source.Item2 != null)
                handler?.Invoke(source.Item2);

            return source.Item1;
        }

        /// <summary>
        /// Retruns the source if both condition is true and monade has value, or null otherwise
        /// </summary>
        public static Maybe<TS> Where<TS>(this Maybe<TS> source, Func<TS, bool> condition)
        {
            if (source.HasValue && condition(source.Value))
                return source;

            return Maybe<TS>.Nothing;
        }
    }
}