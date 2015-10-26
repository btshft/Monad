using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monad
{
    /// <summary>
    /// Curry the function of mult args into
    /// sequence of fuctions with single args
    /// </summary>
    public static class Curry
    {
        public static Func<T1, Func<T2, TR>> curry<T1, T2, TR>(Func<T1, T2, TR> f) =>
           a => b => f(a, b);

        public static Func<T1, Func<T2, Func<T3, TR>>> curry<T1, T2, T3, TR>(Func<T1, T2, T3, TR> f) =>
            a => b => c => f(a, b, c);

        public static Func<T1, Func<T2, Func<T3, Func<T4, TR>>>> curry<T1, T2, T3, T4, TR>(Func<T1, T2, T3, T4, TR> f) =>
            a => b => c => d => f(a, b, c, d);

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, TR>>>>> curry<T1, T2, T3, T4, T5, TR>(Func<T1, T2, T3, T4, T5, TR> f) =>
            a => b => c => d => e => f(a, b, c, d, e);
    }
}
