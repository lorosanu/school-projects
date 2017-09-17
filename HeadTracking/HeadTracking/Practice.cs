using System;
using System.Collections.Generic;
using System.IO;

using WiimoteLib;

namespace HeadTracking
{
    #region Class_SetSensorsP

    public class SetSensorsP
    {
        private List<Sensors> listS = new List<Sensors>();
        public List<string> cStart = new List<string>();
        public List<string> cPrevious = new List<string>();
        private Sensors startPoint = null;

        #region Properties

        public int N
        {
            get
            {
                return listS.Count;
            }
        }

        public Sensors StartPoint
        {
            set
            {
                startPoint = value;
            }
            get
            {
                return startPoint;
            }
        }

        public Sensors Last
        {
            get
            {
                if (N > 0)
                    return listS[N - 1];
                else
                    return startPoint;
            }
        }

        public Sensors Previous
        {
            get
            {
                if (N > 1)
                    return listS[N - 2];
                else
                    return startPoint;
            }
        }

        public string CStart
        {
            get
            {
                if (N > 0)
                    return cStart[N - 1];
                else
                    return "None";
            }
        }

        public string CPrevious
        {
            get
            {
                if (N > 0)
                    return cPrevious[N - 1];
                else
                    return "None";
            }
        }

        #endregion

        #region ComputeDistances

        private double xDistance(Sensors s)
        {
            IRSensor m1 = Last.MiddlePoint;
            IRSensor m2 = s.MiddlePoint;
            Sensors m = new Sensors(m1, m2);

            double d = Math.Abs(m.XDistance);
            return d;
        }

        private double yDistance(Sensors s)
        {
            IRSensor m1 = Last.MiddlePoint;
            IRSensor m2 = s.MiddlePoint;
            Sensors m = new Sensors(m1, m2);

            double d = Math.Abs(m.YDistance);
            return d;
        }

        private double zDistance(Sensors s)
        {
            double d = Math.Abs(Last.Z - s.Z);
            return d;
        }

        #endregion

        #region CheckTheLastMovement

        public bool CheckThreshold()
        {
            int pragX = 0, pragY = 0;

            if (Last.Left(startPoint))
                pragX = WiimoteControl.pragL;
            else
                pragX = WiimoteControl.pragR;

            if (Last.Up(startPoint))
                pragY = WiimoteControl.pragU;
            else
                pragY = WiimoteControl.pragD;

            if (xDistance(startPoint) > pragX || yDistance(startPoint) > pragY)
                return false;
            return true;
        }

        private string CheckMost(Sensors s)
        {
            double d1 = 0, d2 = 0;

             if ( Last.Left(s)|| Last.Right(s) )
                d1 = xDistance(s) / 1024 * 100;

            if ( Last.Up(s) || Last.Down(s) )
                d2 = yDistance(s) / 768 * 100;
           
            if (d1 >= d2)
                return "OX";
            else
                return "OY";
        }

        private string CheckMovement(Sensors s)
        {
            string directie = CheckMost(s);

            switch (directie)
            {
                case "OX":
                    if (Last.Left(s))
                        return "Left";
                    else
                        return "Right";

                case "OY":
                    if (Last.Up(s))
                        return "Up";
                    else
                        return "Down";
            }

            return "";
        }

        #endregion


        public void AddSensor(Sensors s)
        {
            if (s.Equals(Last) || s.Equals(startPoint))
                return;

            listS.Add(s);

            if (!CheckThreshold())
            {
                string sMove = CheckMovement(startPoint);
                cStart.Add(sMove);
            }
            else
                cStart.Add("None");

            string pMove = CheckMovement(Previous);
            cPrevious.Add(pMove);
        }
    }

    #endregion

    #region Class_Practice

    public class Practice
    {
        private static SetSensorsP p = new SetSensorsP();

        #region Properties

        public SetSensorsP List
        {
            get
            {
                return p;
            }
        }

        #endregion

        public Practice()
        {
            p = new SetSensorsP();
        }
        
        public void AddMove(IRSensor s1, IRSensor s2)
        {
            Sensors s = new Sensors(s1, s2);

            if (p.StartPoint == null)
                p.StartPoint = s;
            else
                p.AddSensor(s);
        }

        public void Print()
        {
            Console.WriteLine(p.Last + " " + p.cStart + " " + p.CPrevious);
        }

        #region Read_Data_From_Log

        public static void ReadLog()
        {
            try
            {
                WiimoteControl.command = new SetCommand();

                string filename = HeadTracking.FilePath + "\\log_" + HeadTracking.Game + ".txt";

                TextReader tr = File.OpenText(filename);
                string input = null;
                int q = Convert.ToInt16(tr.ReadLine());
                tr.ReadLine();
                tr.ReadLine();

                for (int i = 0; i < q; i++)
                {
                    input = tr.ReadLine();
                    string[] s = input.Split(' ');
                    int k = Convert.ToInt16(s[0]);
                    string h = s[1];

                    Command c = new Command(k, h);
                    WiimoteControl.command.Add(c);
                }

                tr.ReadLine();
                tr.ReadLine();
                for (int i = 0; i < q; i++)
                {
                    input = tr.ReadLine();
                    string[] s = input.Split(' ');
                    string move = s[0];
                    int p = Convert.ToInt16(s[1]);
                    switch (move)
                    {
                        case "Left":  WiimoteControl.pragL = p; break;
                        case "Right": WiimoteControl.pragR = p; break;
                        case "Up":    WiimoteControl.pragU = p; break;
                        case "Down":  WiimoteControl.pragD = p; break;
                        default: break;
                    }
                }

                if (WiimoteControl.pragL == 0) WiimoteControl.pragL = 100;
                if (WiimoteControl.pragR == 0) WiimoteControl.pragR = 100;
                if (WiimoteControl.pragU == 0) WiimoteControl.pragU = 100;
                if (WiimoteControl.pragD == 0) WiimoteControl.pragD = 100;

                tr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion
    }

    #endregion
}
