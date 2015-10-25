using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monad.MaybeMonad
{
    public interface IMaybe
    {
        bool IsSome { get; }
        bool IsNone { get; }
    }
}
