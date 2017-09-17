using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HeadTracking
{
    class KeyListener
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static int vkCode = 0;
        public static bool b_down = false;
        public static string priority = "";

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);


        public static void Start()
        {
            _hookID = SetHook(_proc);
        }

        public static void Stop()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (wParam == (IntPtr)WM_KEYDOWN)
            {
                vkCode = Marshal.ReadInt32(lParam);
                b_down = true;
                return CallNextHookEx(_hookID, nCode, wParam, lParam);
            }
                        
            b_down = false;
            Console.WriteLine(((Keys)vkCode).ToString());

            if ( !HeadTracking.Start )
            {
                if ((Keys)vkCode == Keys.F2)
                {
                    HeadTracking.Start = true;
                    HeadTracking.StartHeadTracking();
                }
                return CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            if ( vkCode !=0 )
                Learning.SaveEvent(vkCode);            

            vkCode = 0;
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
    }
}
