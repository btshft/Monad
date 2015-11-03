using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monades.Exceptional
{
    public interface IExceptional<out T>
    {
        bool IsFaulted      { get; }
        Exception Exception { get; }
        T Value             { get; }
    }
}

