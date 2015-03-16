using System;
using System.Runtime.InteropServices;

namespace Cliente.Structures
{
    [StructLayout(LayoutKind.Sequential,Pack=1)]
    public struct POINT
    {
        public Int32 x;
        public Int32 y;
    }
}