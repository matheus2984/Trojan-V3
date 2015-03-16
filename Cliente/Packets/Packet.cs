using System.Runtime.InteropServices;

namespace Cliente.Packets
{
    public abstract class Packet
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PacketHeader
        {
            public ushort Length;
            public ushort Type;
        }
    }
}
