using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HeadTracking
{
    static class Program
    {
        #region handlerConsole

        delegate bool ConsoleEventHandlerDelegate( ConsoleHandlerEventCode eventCode );

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler( ConsoleEventHandlerDelegate handlerProc, bool add );

        enum ConsoleHandlerEventCode : uint
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        static ConsoleEventHandlerDelegate consoleHandler;

        static void ConsoleApp()
        {
            consoleHandler = new ConsoleEventHandlerDelegate(ConsoleEventHandler);
            SetConsoleCtrlHandler(consoleHandler, true);
        }

        static bool ConsoleEventHandler( ConsoleHandlerEventCode eventCode )
        {
            switch( eventCode )
            {
                case ConsoleHandlerEventCode.CTRL_CLOSE_EVENT:
                    HeadTracking.DC();
                    break;
            }
            return false;
        }

        #endregion

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConsoleApp();
            Application.Run(new HeadTracking());
        }
    }
}