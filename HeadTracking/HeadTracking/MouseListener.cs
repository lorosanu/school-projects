using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace HeadTracking
{
    class MouseListener
    {
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private const int WH_MOUSE_LL = 14;

        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        public static bool m_move = false;
        private static int ix = 0, iy = 0;
        private static int nx = 0, ny = 0;
        private static int tx = 0, ty = 0;

        private static int w = Screen.PrimaryScreen.Bounds.Width;
        private static int h = Screen.PrimaryScreen.Bounds.Height;

        private static int nr = 0;
        private static System.Windows.Forms.Timer tr = null;


        private static int Key(string m)
        {
            switch (m)
            {
                case "Down": return (int)Keys.Down;
                case "Left": return (int)Keys.Left;
                case "Right": return (int)Keys.Right;
                case "Up": return (int)Keys.Up;
                default: return 0;                    
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            if (m_move)
            {
                if (tx == nx && ty == ny)
                {
                    nr++;
                    if (nr > 10)
                    {
                        string m = DetermineDirection();
                        Console.WriteLine("MouseMovement: " + m);
                        Learning.SaveEvent(Key(m));
                        ix = 0; iy = 0;
                        m_move = false;

                        nr = 0;
                    }                   
                }
                else
                    nr = 0;
                tx = nx; ty = ny;
            }
        }

        private static string DetermineDirection()
        {
            double dx = ((nx - ix) * 1.0) / w;
            double dy = ((ny - iy) * 1.0) / h;

            if (Math.Abs(dx) >= Math.Abs(dy))
            {
                if (nx <= ix)
                    return "Left";
                else
                    return "Right";
            }
            else
            {
                if (ny <= iy)
                    return "Up";
                else
                    return "Down";
            }
        }

        public static void Start()
        {
            tr = new System.Windows.Forms.Timer();
            tr.Interval = 10;
            tr.Tick += new EventHandler(Timer_Tick);
            tr.Start();

            _hookID = SetHook(_proc);
        }

        public static void Stop()
        {
            tr.Stop();
             UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback( int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && MouseMessages.WM_MOUSEMOVE == (MouseMessages)wParam)
            {
                m_move = true;
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                if (ix + iy == 0)
                {
                    ix = hookStruct.pt.x;
                    iy = hookStruct.pt.y;
                }

                nx = hookStruct.pt.x;
                ny = hookStruct.pt.y;

                //Console.WriteLine(hookStruct.pt.x + ", " + hookStruct.pt.y);
                return CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
     
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
