using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monad.Maybe
{
    /// <summary>
    /// Maybe delegate for lazy computation
    /// </summary>
    public delegate MaybeResult<T> Maybe<T>();

    /// <summary>
    /// Helper for creating generic Maybe instances
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        /// Represent an Maybe without value
        /// </summary>
        public static Maybe<T> Nothing<T>()
        {
            return () => NothingResult<T>.Default;
        }

        /// <summary>
        /// Wraps function to Option. If functions result is null
        /// automatically converts to NothingResult
        /// </summary>
        public static Maybe<T> ResultOf<T>(Func<T> function)
        {
            return () => {
                var value = function();
                return value == null
                    ? NothingResult<T>.Default
                    : new SomeResult<T>(value);
            };
        }
    }
}
