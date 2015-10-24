using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monad.MaybeMonad
{
    /// <summary>
    /// Nothing  case of MaybeLazy<T> monad
    /// </summary>
    public sealed class Nothing<T> : Maybe<T>
    {
        internal static Maybe<T> Default = new Nothing<T>();

        /// <summary>
        /// Monad value 
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Throws
        /// </exception>
        public override T Value
        {
            get { throw new InvalidOperationException("MaybeLazy<" + nameof(T) + ">.Nothing has no value."); }
        }

        /// <summary>
        /// Nothing case has no value
        /// </summary>
        public override bool HasValue
        {
            get { return false; }
        }

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