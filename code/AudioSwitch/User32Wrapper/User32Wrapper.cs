using System;
using System.Text;

namespace User32Wrapper
{
    public class User32ImportsWrapper
    {
        public static string GetActiveWindowTitle() {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = User32Imports.GetForegroundWindow();

            if (User32Imports.GetWindowText(handle, Buff, nChars) > 0)
                return Buff.ToString();

            return null;
        }
    }
}
