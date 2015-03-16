using System.Runtime.InteropServices;

namespace Cliente.Hooks
{
    public sealed class Mouse
    {
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
    }
}