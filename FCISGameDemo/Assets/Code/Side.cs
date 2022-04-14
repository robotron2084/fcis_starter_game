using System;

namespace Code
{
    [Flags]
    public enum Side
    {
        None = 0,
        Left = 1,
        Right = 1 << 2,
        Top = 1 << 3,
        Bottom = 1 << 4
    }
}