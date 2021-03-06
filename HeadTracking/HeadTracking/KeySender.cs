using System;
using System.Text;
using System.Diagnostics;
using KernelHotkey;
using System.Windows.Forms;

namespace HeadTracking
{
    class KeySender
    {
        private static Keyboard kbd = null;
        public static Keyboard.Stroke keystroke = null;
        private static Keyboard.Stroke acc = null;

        #region ScanCodes
        public enum ScanCodes : ushort
        {
            NotAssigned = 0x00,
            Escape = 0x01,
            One = 0x02,
            Two = 0x03,
            Three = 0x04,
            Four = 0x05,
            Five = 0x06,
            Six = 0x07,
            Seven = 0x08,
            Eight = 0x09,
            Nine = 0x0A,
            Zero = 0x0B,
            Minus = 0x0C,
            Equals = 0x0D,
            Backspace = 0x0E,
            Tab = 0x0F,
            Q = 0x10,
            W = 0x11,
            E = 0x12,
            R = 0x13,
            T = 0x14,
            Y = 0x15,
            U = 0x16,
            I = 0x17,
            O = 0x18,
            P = 0x19,
            LBracket = 0x1A,
            RBracket = 0x1B,
            Return = 0x1C,
            LControl = 0x1D,
            A = 0x1E,
            S = 0x1F,
            D = 0x20,
            F = 0x21,
            G = 0x22,
            H = 0x23,
            J = 0x24,
            K = 0x25,
            L = 0x26,
            Semicolon = 0x27,
            Apostrophe = 0x28,
            Grave = 0x29,
            LShift = 0x2A,
            Backslash = 0x2B,
            Z = 0x2C,
            X = 0x2D,
            C = 0x2E,
            V = 0x2F,
            B = 0x30,
            N = 0x31,
            M = 0x32,
            Comma = 0x33,
            Period = 0x34,
            Slash = 0x35,
            RShift = 0x36,
            Multiply = 0x37,
            LMenu = 0x38,
            Space = 0x39,
            CapsLock = 0x3A,
            F1 = 0x3B,
            F2 = 0x3C,
            F3 = 0x3D,
            F4 = 0x3E,
            F5 = 0x3F,
            F6 = 0x40,
            F7 = 0x41,
            F8 = 0x42,
            F9 = 0x43,
            F10 = 0x44,
            NumLock = 0x45,
            ScrollLock = 0x46,
            NumPad7 = 0x47,
            NumPad8 = 0x48,
            NumPad9 = 0x49,
            Subtract = 0x4A,
            NumPad4 = 0x4B,
            NumPad5 = 0x4C,
            NumPad6 = 0x4D,
            Add = 0x4E,
            NumPad1 = 0x4F,
            NumPad2 = 0x50,
            NumPad3 = 0x51,
            NumPad0 = 0x52,
            Decimal = 0x53,
            F11 = 0x57,
            F12 = 0x58,
            F13 = 0x64,
            F14 = 0x65,
            F15 = 0x66,
            Kana = 0x70,
            Convert = 0x79,
            NoConvert = 0x7B,
            Yen = 0x7D,
            NumPadEquals = 0x8D,
            Circumflex = 0x90,
            At = 0x91,
            Colon = 0x92,
            Underline = 0x93,
            Kanji = 0x94,
            Stop = 0x95,
            Ax = 0x96,
            Unlabeled = 0x97,
            NumPadEnter = 0x9C,
            RControl = 0x9D,
            NumPadComma = 0xB3,
            Divide = 0xB5,
            SysRq = 0xB7,
            RMenu = 0xB8,
            Home = 0xC7,
            Up = 0xC8,
            Prior = 0xC9,
            Left = 0xCB,
            Right = 0xCD,
            End = 0xCF,
            Down = 0xD0,
            Next = 0xD1,
            Insert = 0xD2,
            Delete = 0xD3,
            LWin = 0xDB,
            RWin = 0xDC,
            Apps = 0xDD
        }
        #endregion

        public static void Start()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            kbd = new Keyboard(0);           

            if (HeadTracking.Game == "Cars")
            {                
                acc = new Keyboard.Stroke();
                acc.code = (ushort)ScanCodes.Up;
                acc.state = Keyboard.States.MAKE;
                kbd.Write(acc);
            }
        }

        public static void KeyDown(ScanCodes code)
        {
            if (kbd != null && ( (keystroke == null) || (keystroke.state == Keyboard.States.BREAK)) )
            {
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

                keystroke = new Keyboard.Stroke();
                keystroke.code = (ushort)code;
                keystroke.state = Keyboard.States.MAKE;
                kbd.Write(keystroke);
            }
        }

        public static void KeyUp()
        {
            if (keystroke != null && keystroke.state == Keyboard.States.MAKE)
            {
                keystroke.state = Keyboard.States.BREAK;
                kbd.Write(keystroke);
            }
        }

        public static void AccDown()
        {
            if (acc != null && acc.state == Keyboard.States.BREAK)
            {
                acc.state = Keyboard.States.MAKE;
                kbd.Write(acc);
            }
        }

        public static void AccUp()
        {
            if (keystroke != null && keystroke.state == Keyboard.States.MAKE)
                if (acc != null && acc.state == Keyboard.States.MAKE)
                {
                    acc.state = Keyboard.States.BREAK;
                    kbd.Write(acc);
                }
        }

        public static void Pause()
        {
            if (keystroke != null && keystroke.state == Keyboard.States.MAKE)
            {
                keystroke.state = Keyboard.States.BREAK;
                kbd.Write(keystroke);
            }

            if (acc != null && acc.state == Keyboard.States.BREAK)
            {
                acc.state = Keyboard.States.MAKE;
                kbd.Write(acc);
            }
        }
    }
}
