using System.Runtime.InteropServices;

namespace Server.Packets
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
