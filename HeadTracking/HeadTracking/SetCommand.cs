using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HeadTracking
{
    #region Class_Command

    public class Command
    {
        private int keyCode = 0;
        private string headMovement = "";

        public Command(int k, string h)
        {
            KeyCode = k;
            headMovement = h;
        }

        public int KeyCode
        {
            get
            {
                return keyCode;
            }
            set
            {
                keyCode = value;
            }
        }

        public string HeadMovement
        {
            get
            {
                return headMovement;
            }
            set
            {
                headMovement = value;
            }
        }
    }

    #endregion

    #region Class_SetCommand

    public class SetCommand
    {
        private List<Command> listA = new List<Command>();

        #region Properties

        public int P
        {
            get
            {
                return listA.Count;
            }
        }

        public Command this[int index]
        {
            get
            {
                if (index >= 0 && index < P)
                    return listA[index];
                else
                    throw new ArgumentException();
            }

            set
            {
                if (index >= 0 && index < listA.Count)
                    listA[index] = value;
                else
                    throw new ArgumentException();
            }
        }

        #endregion

        public void Add(Command a)
        {            
            listA.Add(a);
        }

        public int FindKey(string movement)
        {
            int code = 0;

            for (int i = 0; i < P; i++)
                if (listA[i].HeadMovement == movement)
                {
                    code = listA[i].KeyCode;
                    break;
                }

            return code;
        }

        public void SendMouse(string m)
        {
            MouseSender.MouseMove(m);
        }

        public void Send(Keys k)
        {
            if (HeadTracking.Game == "" || HeadTracking.Game == "Cars")
                switch (k)
                {
                    case Keys.Up:    KeySender.KeyDown(KeySender.ScanCodes.Up); break;
                    case Keys.Down:  KeySender.KeyDown(KeySender.ScanCodes.Down); break;
                    case Keys.Left:  KeySender.KeyDown(KeySender.ScanCodes.Left); break;
                    case Keys.Right: KeySender.KeyDown(KeySender.ScanCodes.Right); break;
                    default: break;
                }
            else
                if (HeadTracking.Game == "Pacman" || HeadTracking.Game == "Tetris")
                    switch (k)
                    {
                        case Keys.Up: SendKeys.Send("{Up}"); ; break;
                        case Keys.Down: SendKeys.Send("{Down}"); break;
                        case Keys.Left: SendKeys.Send("{Left}"); break;
                        case Keys.Right: SendKeys.Send("{Right}"); break;
                        default: break;
                    }
                else
                {
                    switch (k)
                    {
                        case Keys.Up:   KeySender.KeyDown(KeySender.ScanCodes.U); break;
                        case Keys.Down: KeySender.KeyDown(KeySender.ScanCodes.J); break;
                        case Keys.Left: KeySender.KeyDown(KeySender.ScanCodes.H); break;
                        case Keys.Right: KeySender.KeyDown(KeySender.ScanCodes.K); break;
                        default: break;
                    }
                }
        }
    }

    #endregion
}
