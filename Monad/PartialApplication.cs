using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monad
{
    /// <summary>
    /// Fixing a number of arguments to a function, producing another function of smaller arity.
    /// </summary>
    public static class PartialApplication
    {
        public static Func<T2, TR> papply<T1, T2, TR>(Func<T1, T2, TR> f, T1 a) =>
            (b) => f(a, b);

        public static Func<T3, TR> papply<T1, T2, T3, TR>(Func<T1, T2, T3, TR> f, T1 a, T2 b) =>
            (c) => f(a, b, c);

        public static Func<T2, T3, TR> papply<T1, T2, T3, TR>(Func<T1, T2, T3, TR> f, T1 a) =>
            (b, c) => f(a, b, c);

        public static Func<T4, TR> papply<T1, T2, T3, T4, TR>(Func<T1, T2, T3, T4, TR> f, T1 a, T2 b, T3 c) =>
            (d) => f(a, b, c, d);

        public static Func<T3, T4, TR> papply<T1, T2, T3, T4, TR>(Func<T1, T2, T3, T4, TR> f, T1 a, T2 b) =>
            (c, d) => f(a, b, c, d);

        public static Func<T2, T3, T4, TR> papply<T1, T2, T3, T4, TR>(Func<T1, T2, T3, T4, TR> f, T1 a) =>
            (b, c, d) => f(a, b, c, d);

        public static Func<T5, TR> papply<T1, T2, T3, T4, T5, TR>(Func<T1, T2, T3, T4, T5, TR> f, T1 a, T2 b, T3 c, T4 d) => 
            (e) => f(a, b, c, d, e);

        public static Func<T4, T5, TR> papply<T1, T2, T3, T4, T5, TR>(Func<T1, T2, T3, T4, T5, TR> f, T1 a, T2 b, T3 c) => 
            (d, e) => f(a, b, c, d, e);

        public static Func<T3, T4, T5, TR> papply<T1, T2, T3, T4, T5, TR>(Func<T1, T2, T3, T4, T5, TR> f, T1 a, T2 b) =>
            (c, d, e) => f(a, b, c, d, e);

        public static Func<T2, T3, T4, T5, TR> papply<T1, T2, T3, T4, T5, TR>(Func<T1, T2, T3, T4, T5, TR> f, T1 a) =>
            (b, c, d, e) => f(a, b, c, d, e);
    }
}