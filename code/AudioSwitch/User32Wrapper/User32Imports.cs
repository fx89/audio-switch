/*
 * SOURCE: https://stackoverflow.com/questions/115868/how-do-i-get-the-title-of-the-current-active-window-using-c
 */

using System;
using System.Runtime.InteropServices;
using System.Text;


namespace User32Wrapper
{
    class User32Imports
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
    }
}
