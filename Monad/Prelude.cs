using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monad
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
    }
}
