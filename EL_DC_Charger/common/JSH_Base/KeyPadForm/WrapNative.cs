using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EL_DC_Charger.JSH_Base.KeyPadForm
{
    [Flags]
    public enum KeyFlag { KE_DOWN = 0, KE__EXTENDEDKEY = 1, KE_UP = 2 }
    static public class WrapNative
    {
        static Mutex mu = new Mutex();
        const int IME_CMODE_NATIVE = 0x1;
        [DllImport("imm32.dll")]
        static extern int ImmGetContext(int hWnd);
        [DllImport("imm32.dll")]
        static extern int ImmReleaseContext(int hWnd, int hImc);
        [DllImport("imm32.dll")]
        static extern int ImmGetConversionStatus(int hImc, out int fdwConversion,
                                              out int fdwSentence);
        [DllImport("imm32.dll")]
        static extern bool ImmSetConversionStatus(IntPtr hIMC, int fdwConversion,
                                                int fdwSentence);
        public static bool IsNativeMode(int handle)
        {
            int hImc, dwConversion = 0, dwSentense = 0;
            hImc = ImmGetContext(handle);
            ImmGetConversionStatus(hImc, out dwConversion, out dwSentense);
            return (dwConversion & IME_CMODE_NATIVE) == 1;
        }
        [DllImport("user32.dll")]
        private static extern int GetKeyboardState(byte[] pbKeyState);
        [DllImport("User32.dll")]
        static extern void keybd_event(byte vk, byte scan, int flags, int extra);
        public static void KeyDown(Keys keycode)
        {
            mu.WaitOne();
            keybd_event((byte)keycode, 0, (int)(KeyFlag.KE_DOWN | KeyFlag.KE__EXTENDEDKEY), 0);
            mu.ReleaseMutex();
        }
        public static void KeyUp(Keys keycode)
        {
            mu.WaitOne();
            keybd_event((byte)keycode, 0, (int)(KeyFlag.KE_UP | KeyFlag.KE__EXTENDEDKEY), 0);
            mu.ReleaseMutex();
        }
        public static void KeyClick(Keys keycode, bool shift)
        {
            mu.WaitOne();
            if (shift)
            {
                keybd_event((byte)Keys.ShiftKey, 0,
                            (int)(KeyFlag.KE_DOWN | KeyFlag.KE__EXTENDEDKEY), 0);
                keybd_event((byte)keycode, 0,
                            (int)(KeyFlag.KE_DOWN | KeyFlag.KE__EXTENDEDKEY), 0);
                keybd_event((byte)keycode, 0,
                            (int)(KeyFlag.KE_UP | KeyFlag.KE__EXTENDEDKEY), 0);
                keybd_event((byte)Keys.ShiftKey, 0,
                            (int)(KeyFlag.KE_UP | KeyFlag.KE__EXTENDEDKEY), 0);
            }
            else
            {
                keybd_event((byte)keycode, 0,
                             (int)(KeyFlag.KE_DOWN | KeyFlag.KE__EXTENDEDKEY), 0);
                keybd_event((byte)keycode, 0,
                             (int)(KeyFlag.KE_UP | KeyFlag.KE__EXTENDEDKEY), 0);
            }
            mu.ReleaseMutex();
        }
        public static bool IsPress(Keys keycode)
        {
            byte[] vks = new byte[256];
            GetKeyboardState(vks);
            return vks[(int)keycode] == 1;
        }
    }
}
