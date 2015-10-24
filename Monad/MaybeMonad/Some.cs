using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monad.MaybeMonad
{
    public sealed class Some<T> : Maybe<T>
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
        /// Constructs MaybeLazy from value
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static Some<T> Of(T value)
        {
            return new Some<T>(value);
        }

        /// <summary>
        /// 
        /// </summary>
        private Some(T value)
        {
            Value = value;
        } 
    }


}
