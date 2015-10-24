using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monad.Maybe
{
    public sealed class SomeResult<T> : MaybeResult<T>
    {
        /// <summary>
        /// Returns Some value
        /// </summary>
        public override T    Value    { get; }

        /// <summary>
        /// Some always have a value
        /// </summary>
        public override bool HasValue { get { return true; } }

        /// <summary>
        /// Constructs Maybe from value
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public SomeResult(T value)
        {
            Value = value;
        } 
    }
}
