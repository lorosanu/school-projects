using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using WiimoteLib;

namespace HeadTracking
{
    #region Class_SetSensorsL

    public class SetSensorsL
    {
        private List<Sensors> listS = new List<Sensors>();        
        public List<string> cPrevious = new List<string>();

        private Sensors startPoint = null;
        public string cStart;

        #region Properties

        public int N
        {
            get
            {
                return listS.Count;
            }
        }

        public string CStart
        {
            get
            {
                return cStart;
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

        public Sensors this[int index]
        {
            get
            {
                if (index >= 0 && index < N)
                    return listS[index];
                else
                    throw new ArgumentException();
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

        #endregion

        public double Distance(int index)
        {
            Sensors Last = listS[index];
            Sensors s = new Sensors(startPoint.MiddlePoint, Last.MiddlePoint);
            double d = s.Z;
            return d;
        }

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

        private string CheckMost(Sensors Last, Sensors s)
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

        private string CheckMovement(Sensors Last, Sensors s)
        {
            string directie = CheckMost(Last, s);

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
            if (s.Equals(startPoint) || s.Equals(Last))
                return;

            listS.Add(s);

            //compare current position with StartPoint
            string sMove = CheckMovement(Last, startPoint);
            cStart = sMove;

            //compare current position with previous position
            string pMove = CheckMovement(Last, Previous);
            cPrevious.Add(pMove);
        }

        public void Filter()
        {          
            if ( N > 2 && cPrevious[0] != cPrevious[1] && cPrevious[0] != cPrevious[2])
            {
                listS.RemoveAt(0);
                cPrevious.RemoveAt(0);
            }

            int k = cPrevious.Count;
            if (k > 2 && cPrevious[k - 2] != cPrevious[k - 1] && cPrevious[k - 3] != cPrevious[k - 1])
            {
                listS.RemoveAt(k - 1);
                cPrevious.RemoveAt(k - 1);
            }

            int i = 1;
            while (i < listS.Count -1)
            {
                if (cPrevious[i - 1] != cPrevious[i] && cPrevious[i] != cPrevious[i + 1])
                {
                    listS.RemoveAt(i);
                    cPrevious.RemoveAt(i);
                }
                else
                    i++;
            }
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < N; i++)            
                s += listS[i] + " " + cPrevious[i] + "\n";                            
            return s;
        }
    }

    #endregion

    #region Class_Learning

    public class Learning
    {
        private static SetSensorsL p = new SetSensorsL();
        private static SetSensorsL temp = new SetSensorsL();        
        private static List<Histogram> Set = new List<Histogram>();
               
        private static int poz = 0;

        public Learning()
        {
            p = new SetSensorsL();
            temp = new SetSensorsL();        
            Set = new List<Histogram>();            
            poz = 0;            
        }

        public void AddMove(IRSensor s1, IRSensor s2)
        {
            Sensors s = new Sensors(s1, s2);

            if (p.StartPoint == null)
                p.StartPoint = s;
            else
                p.AddSensor(s);
        }

        #region Learning_Stage

        public static void SaveMove(IRSensor s1, IRSensor s2)
        {
            Sensors s = new Sensors(s1, s2);

            if (temp.StartPoint == null)
                temp.StartPoint = s;
            else
                temp.AddSensor(s);
        }

        private static void GetHistogram(int keyP)
        {
            poz = -1;
            for (int i = 0; i < Set.Count; i++)
                if (Set[i].KeyPressed == keyP)
                {
                    poz = i;
                    break;
                }
        }

        private static void SaveData(int KeyP, string movement, int amplitude)
        {
            int move = HeadTracking.moves[movement];

            if (poz == -1)
            {
                Histogram h = new Histogram(KeyP);
                h.Add(move, amplitude);
                Set.Add(h);
                poz = Set.Count - 1;
            }
            else
                Set[poz].Add(move, amplitude);
        }

        public static void SaveEvent(int keyP)
        {
            if (keyP == 0 || temp.N == 0)
            {
                temp = new SetSensorsL();
                return;
            }

            temp.Filter();
            Console.WriteLine(temp);

            try
            {
                GetHistogram(keyP);
                Console.WriteLine("KeyPressed: " + (Keys)keyP + ", Tracked: " + temp.N + " moves");                

                int k = 0, i = 1;
                while (i < temp.N - 1)
                {                   
                    if (temp.cPrevious[i] == temp.cPrevious[i - 1])
                        i++;
                    else
                        if (temp.cPrevious[i] == temp.cPrevious[i + 1])
                        {                            
                            string s = temp.cPrevious[i-1];
                            int amplitude = (int)temp.Distance(i - 1);

                            if (amplitude > 0)
                            {
                                SaveData(keyP, s, amplitude);
                                Console.WriteLine("Movement: " + s + ", Amplitude: " + amplitude);
                            }

                            temp.StartPoint = temp[i - 1];
                            k = i;
                            i++;
                        }
                        else
                            i += 2;
                }

                if (k < temp.N)
                {
                    string s = temp.cPrevious[temp.N - 1];
                    int amplitude = (int)temp.Distance(temp.N - 1);

                    if (amplitude > 0)
                    {
                        SaveData(keyP, s, amplitude);
                        Console.WriteLine("Movement: " + s + ", Amplitude: " + amplitude);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception SaveEvent: " + ex.ToString());
            }

            temp = new SetSensorsL();
        }

        
        #endregion

        #region Write_Data_To_Log

        private static int GetMaxKey(string movement)
        {
            int n = Set.Count;
            int max = 0, poz = -1;

            for (int i = 0; i < n; i++)
                if (Set[i].GetMovement() == movement)
                    if (Set[i].GetFrequency(movement) > max)
                    {
                        max = Set[i].GetFrequency(movement);
                        poz = i;
                    }
            return poz;
        }

        public static void CreateDataDictionary()
        {
            WiimoteControl.command = new SetCommand();

            for (int i = 0; i < 4; i++)
            {
                int poz = GetMaxKey(HeadTracking.moves[i]);
                if (poz != -1)
                {
                    string move = HeadTracking.moves[i];
                    int key = Set[poz].KeyPressed;
                    Command c = new Command(key, move);
                    WiimoteControl.command.Add(c);
                }
            }
        }

        public static void WriteLog()
        {
            CreateDataDictionary();

            try
            {
                int q = WiimoteControl.command.P;
                if (q == 0) return;

                string filename = HeadTracking.FilePath + "\\log_" + HeadTracking.Game + ".txt";

                using (TextWriter tw = File.CreateText(filename))
                {
                    tw.WriteLine(q);
                    tw.WriteLine("\nDictionary:");

                    for (int i = 0; i < q; i++)
                    {
                        string move = WiimoteControl.command[i].HeadMovement;
                        int key = WiimoteControl.command[i].KeyCode;
                        tw.WriteLine(key + " " + move);
                    }

                    tw.WriteLine("\nThreshold:");

                    for (int i = 0; i < 4; i++)
                    {
                        string move = HeadTracking.moves[i];
                        int poz = GetMaxKey(move);
                        if (poz != -1)
                        {
                            int prag = Set[poz].GetThreshold();
                            tw.WriteLine(move + " " + prag);
                        }
                    }                    

                    tw.WriteLine("\nAll data collected:\n");
                    for (int i = 0; i < Set.Count; i++)
                        tw.WriteLine(Set[i].ToString());               
                    tw.Close();
                }
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
