using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monades.Exceptional
{
    /// <summary>
    /// Exceptional (Try) monad. Contains value or Exception
    /// </summary>
    public struct Exceptional<T> : IExceptional<T>
    {
        /// <summary>
        /// Represent monad has exception or not
        /// </summary>
        public bool IsFaulted => 
            Exception != null;

        /// <summary>
        /// Represent exception itself
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Represent value
        /// </summary>
        public T Value { get; }

        public Exceptional(T value)
        {
            Value = value;
            Exception = null;
        }

        public Exceptional(Exception e)
        {
            Exception = e;
            Value = default(T);
        }

    }
}
