using System;
using System.Runtime.InteropServices;

namespace Server.Structures
{
    [StructLayout(LayoutKind.Sequential,Pack = 1)]
    public struct POINT
    {
        public Int32 x;
        public Int32 y;
    }
}