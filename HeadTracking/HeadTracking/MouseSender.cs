using System;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HeadTracking
{
    class MouseSender
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const int MOUSEEVENTF_MOVE = 0x0001;

        private static int h = Screen.PrimaryScreen.Bounds.Height;
        private static int w = Screen.PrimaryScreen.Bounds.Width;

        private static double SCREEN_X_CONV;
        private static double SCREEN_Y_CONV;

        private static int x = 0, y = 0;
        private static int nx = 0, ny = 0;

        public static void Start()
        {
            SCREEN_X_CONV = 65535 / w;
            SCREEN_Y_CONV = 65535 / h;

            x = w / 2; y = h / 2;

            nx = (int)(x * SCREEN_X_CONV);
            ny = (int)(y * SCREEN_Y_CONV);

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, nx, ny, 0, 0);
        }

        public static void MouseMove(string movement)
        {
            switch (movement)
            {
                case "Down":   y += 10; break;
                case "Left":   x -= 10; break;
                case "Right":  x += 10; break;
                case "Up":     y -= 10; break;
                default: break;
            }

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            nx = (int)(x * SCREEN_X_CONV);
            ny = (int)(y * SCREEN_Y_CONV);
            
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, nx, ny, 0, 0);       
        }
    }
}


