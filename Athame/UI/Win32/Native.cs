using System;
using System.Runtime.InteropServices;

namespace Athame.UI.Win32
{
    internal static class Native
    {
        internal const int WM_USER = 0x400;

        internal const string User32 = "user32.dll";

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
    }
}
