using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Cliente
{
    public static class NativeMethods
    {
        private const string Msvcrt = "msvcrt.dll";

        [DllImport(Msvcrt, CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern unsafe void* memcpy(void* dst, string src, int length);

        [DllImport(Msvcrt, CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern unsafe void* memcpy(void* dst, byte[] src, int length);

        [DllImport(Msvcrt, CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern unsafe void* memcpy(byte[] dst, void* src, int length);

        [DllImport(Msvcrt, CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern unsafe void* memcpy(void* dst, void* src, int length);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* memset(void* ptr, Byte value, Int32 num)
        {
            for (Int32 i = 0; i < num; i++)
                *(((Byte*)ptr) + i) = value;
            return ptr;
        }

        public static unsafe void* malloc(Int32 size)
        {
            void* ptr = Marshal.AllocHGlobal(size).ToPointer();
            return ptr;
        }

        public static unsafe void free(void* ptr)
        {
            if (ptr != null)
                Marshal.FreeHGlobal((IntPtr)ptr);
        }

        public static unsafe Int32 strlen(Byte* ptr)
        {
            for (int i = 0; i < Int32.MaxValue; i++)
                if (ptr[i] == '\0')
                    return i;
            return Int32.MaxValue;
        }

        public static unsafe Byte* ToPointer(this String Str)
        {
            byte[] Buffer = Encoding.GetEncoding(1252).GetBytes(Str + "\0");
            var ptr = (Byte*)malloc(Buffer.Length);

            fixed (Byte* pBuffer = Buffer)
                memcpy(ptr, pBuffer, Buffer.Length);
            return ptr;
        }

        public static unsafe Byte* ToPointer(this String Str, Byte* ptr)
        {
            byte[] Buffer = Encoding.GetEncoding(1252).GetBytes(Str + "\0");
            fixed (Byte* pBuffer = Buffer)
                memcpy(ptr, pBuffer, Buffer.Length);
            return ptr;
        }
    }
}