﻿namespace Monades.Maybe
{
    public interface IMaybe
    {
        bool IsSome { get; }
        bool IsNone { get; }
    }
}