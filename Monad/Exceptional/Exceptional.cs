using Monades.Maybe;
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

        public Unit IfSucc(Action<T> handler)
        {
            if (!IsFaulted)
                handler(Value);
            return Unit.Default;
        }

        public T IfSucc(Func<T> handler)
        {
            if (!IsFaulted)
                return handler();
            return default(T);
        }

        public T IfFail(T defaultValue)
        {
            if (IsFaulted)
                return defaultValue;
            return Value;
        }

        public T IfFail(Func<T> defaultAction)
        {
            if (IsFaulted)
                defaultAction();
            return Value;
        }

    }
}
