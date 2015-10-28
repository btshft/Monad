using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monad.MaybeMonad
{
    public static class SomeExtensions
    {
        /// <summary>
        /// Converts value to an Some<T>.
        /// </summary>
        public static Some<T> ToSome<T>(this T self) =>
            Some.Of(self);
    }
}
