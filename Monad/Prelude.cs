using System;
using Monades.Maybe;

namespace Monades
{
    public static class Prelude
    {
        /// <summary>
        /// Identity function
        /// </summary>
        public static T Identity<T>(T x) => x;

        /// <summary>
        /// Raise an exception
        /// </summary>
        public static T Raise<T>(Exception ex)
        {
            throw ex;
        }

        /// <summary>
        /// Ignore any value, return Unit
        /// </summary>
        public static Unit Ignore<T>(T any) => Unit.Default;

    }
}