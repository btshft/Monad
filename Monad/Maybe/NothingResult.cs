using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monad.Maybe
{
    /// <summary>
    /// Nothing  case of Maybe<T> monad
    /// </summary>
    public sealed class NothingResult<T> : MaybeResult<T>
    {
        internal static MaybeResult<T> Default = new NothingResult<T>();

        /// <summary>
        /// Monad value 
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Throws
        /// </exception>
        public override T Value
        {
            get
            {
                throw new InvalidOperationException("Maybe<" + nameof(T) + ">.Nothing has no value.");
            }
        }

        /// <summary>
        /// Nothing case has no value
        /// </summary>
        public override bool HasValue { get { return false; } }

        /// <summary>
        /// Simple toString implementation
        /// </summary>
        /// <returns>"[Nothing]"</returns>
        public override string ToString()
        {
            return "[Nothing]";
        }
    }
}
