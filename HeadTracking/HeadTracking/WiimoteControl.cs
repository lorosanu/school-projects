using System;
using System.Windows.Forms;
using System.IO;

using WiimoteLib;

namespace HeadTracking
{
    public partial class WiimoteControl : UserControl
    {
        public Wiimote mWiimote;
        public delegate void UpdateWiimoteStateDelegate(WiimoteChangedEventArgs args);

        #region Initialization

        public static string stage = "Learning";
        public static SetCommand command = new SetCommand();

        public static Learning L = new Learning();
        public static Practice P = new Practice();
        
        public static int pragL = 0, pragR = 0; //OX
        public static int pragU = 0, pragD = 0; //OY

        public static IRSensor s1, s2;
        private static int Nr = 0, NrK = 0;
        private static int NrIRs = 0;                

        private bool post = false;

        public WiimoteControl()
        {
            InitializeComponent();
        }

        public WiimoteControl(Wiimote wm): this()
        {
            mWiimote = wm;
        }

        public Wiimote Wiimote
        {
            set { mWiimote = value; }
        }

        public static void Restart()
        {
            L = new Learning();
            P = new Practice();
        
            pragL = 0; pragR = 0; //OX
            pragU = 0; pragD = 0; //OY

            NrIRs = 0;
            NrK = 0;
            Nr = 0;
        }

        #endregion

        #region UpdateWiimote

        public void UpdateState(WiimoteChangedEventArgs args)
        {
            BeginInvoke(new UpdateWiimoteStateDelegate(UpdateWiimoteChanged), args);
        }

        public void UpdateWiimoteChanged(WiimoteChangedEventArgs args)
        {
            Nr++;

            try
            {
                if (stage == "Learning")
                    LearningStage(args);
                else
                    PracticeStage(args);                 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exceptie WiimoteUpdate" + ex.ToString());
                return;
            }
        }

        private int NumberOfIRs()
        {
            int nr = 0;
            if (mWiimote.WiimoteState.IRState.IRSensors[0].Found)
                nr++;
            if (mWiimote.WiimoteState.IRState.IRSensors[1].Found)
                nr++;
            return nr;
        }

        private void Swap(ref IRSensor s1, ref IRSensor s2)
        {
            int x = s1.RawPosition.X;
            int y = s1.RawPosition.Y;
            s1.RawPosition.X = s2.RawPosition.X;
            s1.RawPosition.Y = s2.RawPosition.Y;
            s2.RawPosition.X = x;
            s2.RawPosition.Y = y;
        }

        #region Learning

        private void LearningStage(WiimoteChangedEventArgs args)
        {
            WiimoteState ws = args.WiimoteState;
            s1 = ws.IRState.IRSensors[0];
            s2 = ws.IRState.IRSensors[1];

            NrIRs = NumberOfIRs();

            if (NrIRs != 2)
                return;

            if (s1.RawPosition.X > s2.RawPosition.X)
                Swap(ref s1, ref s2);

            L.AddMove(s1, s2);

            if (!post)
            {
                Console.WriteLine("Initial position: " + new Sensors(s1, s2).ToString());
                post = true;
            }

            if (HeadTracking.Start)
            {
                if (HeadTracking.Game == "Shooter")
                     MouseEvent();
                else
                     KeyboardEvent();
            }
        }

        private void KeyboardEvent()
        {
            if (KeyListener.b_down == true)
                Learning.SaveMove(s1, s2);
        }

        private void MouseEvent()
        {
            if (MouseListener.m_move == true)
                Learning.SaveMove(s1, s2);
        }

        #endregion

        #region Practice

        #region CarGame

        private void CarGame(WiimoteChangedEventArgs args)
        {
            KeySender.AccDown();

            if (!P.List.CheckThreshold())
            {
                string ss = P.List.CStart;
                string sp = P.List.CPrevious;

                if (ss != "None" && ss == sp)
                {
                    Console.WriteLine("Movement -> " + ss);
                    int key = command.FindKey(ss);

                    if (key != 0)
                    {
                        if ((Keys)key != Keys.Down)
                        {
                            NrK++;
                            if (NrK % 6 < 2)
                                command.Send((Keys)key);
                            else
                                KeySender.Pause();
                        }
                    }
                }
                else
                {
                    KeySender.KeyUp();
                    KeySender.AccDown();
                    NrK = 0;
                }
            }
            else
                NrK = 0;
       }

        #endregion

        #region Pacman&Tetris Game

        private void PTGame(WiimoteChangedEventArgs args)
        {
            //P.Print();

            if (!P.List.CheckThreshold())
            {
                string ss = P.List.CStart;
                string sp = P.List.CPrevious;

                if (ss != "None" && ss == sp)
                {
                    if (NrK < 4)
                    {
                        Console.WriteLine("Movement -> " + ss);
                        NrK++;

                        int key = command.FindKey(ss);
                        if (key != 0)
                            command.Send((Keys)key);
                    }
                }
                else
                    NrK = 0;
            }
            else
                NrK = 0;
        }

        #endregion

        #region ShooterGame

        private void ShooterGame(WiimoteChangedEventArgs args)
        {
            KeySender.AccDown();

            if (!P.List.CheckThreshold())
            {
                string ss = P.List.CStart;
                string sp = P.List.CPrevious;

                if (ss != "None" && ss == sp)
                {
                    Console.WriteLine("Movement -> " + ss);
                    int key = command.FindKey(ss);

                    if (key != 0)
                    {
                        NrK++;
                        if (NrK % 15 < 13)
                            command.Send((Keys)key);
                        else
                            KeySender.KeyUp();
                    }
                }
                else
                {
                    KeySender.KeyUp();
                    NrK = 0;
                }
            }
            else
                NrK = 0;
        }

        #endregion
       
        private void EstimatePosition()
        {
            s1 = P.List.Last.P1;
            s2 = P.List.Last.P2;

            if (P.List.CStart == "None")
            {
                switch (P.List.CPrevious)
                {
                    case "Left":
                        s1.RawPosition.X += 100;
                        s2.RawPosition.X += 100;
                        break;

                    case "Right":
                        s1.RawPosition.X -= 100;
                        s2.RawPosition.X -= 100;
                        break;

                    case "Up":
                        s1.RawPosition.Y += 100;
                        s2.RawPosition.Y += 100;
                        break;

                    case "Down":
                        s1.RawPosition.Y -= 100;
                        s2.RawPosition.Y -= 100;
                        break;
                    default: break;
                }
            }
        }

        private void PracticeStage(WiimoteChangedEventArgs args)
        {
            WiimoteState ws = args.WiimoteState;
            s1 = ws.IRState.IRSensors[0];
            s2 = ws.IRState.IRSensors[1];

            NrIRs = NumberOfIRs();

            if (NrIRs != 2)
            {
                if (P.List.N == 0)
                    return;
                EstimatePosition();                
            }

            if (s1.RawPosition.X > s2.RawPosition.X)
                Swap(ref s1, ref s2);

            if (!post)
            {
                Console.WriteLine("Initial position: " + new Sensors(s1, s2).ToString());
                post = true;
            }

            P.AddMove(s1, s2);

            if (HeadTracking.Start)
                switch (HeadTracking.Game)
                {                    
                    case "Tetris":  PTGame(args); break;
                    case "Pacman":  PTGame(args); break;
                    case "Cars":    CarGame(args); break;
                    case "Shooter": ShooterGame(args); break;
                    default: CarGame(args); break;
                }              
        }

        #endregion

        #endregion

    }
}
