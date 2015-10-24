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
    }
}
