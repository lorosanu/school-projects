using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HeadTracking
{
    #region Class_Series

    public class Series
    {
        private List<int> Amplitude = new List<int>();
        private List<int> Frequency = new List<int>();
        private int movement;

        public Series(int m)
        {
            movement = m;
        }

        #region Properties

        public int S
        {
            get
            {
                return Amplitude.Count;
            }
        }

        public int Movement
        {
            get
            {
                return movement;
            }
        }

        public string sMovement
        {
            get
            {
                return HeadTracking.moves[movement];
            }
        }

        public int TotalFrequency
        {
            get
            {
                int f =0;
                for (int i = 0; i < S; i++)
                    f += Frequency[i];
                return f;
            }
        }

        public int TotalAmplitude
        {
            get
            {
                int a = 0;
                for (int i = 0; i < S; i++)
                    a += Amplitude[i] * Frequency[i];
                return a;
            }
        }

        public int Threshold
        {
            get
            {
                return TotalAmplitude / TotalFrequency;
            }
        }

        #endregion

        public void Add(int am)
        {
            for (int i = 0; i < S; i++)
                if (Amplitude[i] == am)
                {
                    Frequency[i]++;
                    return;
                }

            Amplitude.Add(am);
            Frequency.Add(1);
        }

        #region ToString()

        private void QSort(int p, int q)
        {
            int i = p;
            int j = q;
            int x = Amplitude[(i + j) / 2];

            do
            {
                while (Amplitude[i] < x)
                    i++;

                while (x < Amplitude[j])
                    j--;

                if (i <= j)
                {
                    int aux = Amplitude[i];
                    Amplitude[i] = Amplitude[j];
                    Amplitude[j] = aux;

                    aux = Frequency[i];
                    Frequency[i] = Frequency[j];
                    Frequency[j] = aux;

                    i++; j--;
                }
            }
            while (i < j);

            if (p < j)
                QSort(p, j);

            if (i < q)
                QSort(i, q);
        }

        public override string ToString()
        {
            if ( S > 1 )
                QSort(0, S - 1);

            string s = "HeadMovement: " + HeadTracking.moves[movement]  + ", Frequency: " +  TotalFrequency;
            s += "\nHeadMovement / Amplitude / Frequency\n";
            for (int i = 0; i < S; i++)
                s += movement + " " + Amplitude[i] + " " + Frequency[i] + "\n";
            return s;
        }

        #endregion
    }

    #endregion

    #region Class_Histogram

    public class Histogram
    {
        private List<Series> listS = new List<Series>();
        private int key = 0;

        public Histogram(int k)
        {
            key = k;
            listS.Add(new Series(0)); //Down
            listS.Add(new Series(1)); //Left
            listS.Add(new Series(2)); //Right
            listS.Add(new Series(3)); //Up
        }

        #region Properties

        public int H
        {
            get
            {
                return listS.Count;
            }
        }

        public int KeyPressed
        {
            get
            {
                return key;
            }
        }

        public Series this[int index]
        {
            get
            {
                if (index >= 0 && index < H)
                    return listS[index];
                else
                    throw new ArgumentException();
            }
        }

        #endregion

        public void Add(int m, int am)
        {
            listS[m].Add(am);
        }

        #region Measurements

        private int Max()
        {            
            
            int max = 0, poz = -1;

            for (int i = 0; i < H; i++)
                if (listS[i].TotalFrequency > max)
                {
                    max = listS[i].TotalFrequency;
                    poz = i;
                }

            return poz;
        }

        public string GetMovement()
        {
            if (H > 0)
            {
                int p = Max();
                return listS[p].sMovement;
            }
            else
                return "";
        }

        public int GetFrequency(string movement)
        {
            if (H > 0)
            {
                int i = 0;
                for (i = 0; i < H; i++)
                    if ( listS[i].sMovement == movement)
                        break;
                
                if ( i < H )        
                    return listS[i].TotalFrequency;
            }
            
            return 0;
        }

        public int GetThreshold()
        {
            if (H > 0)
            {
                int p = Max();
                return listS[p].Threshold;
            }
            else
                return 0;
        }

        #endregion

        public override string ToString()
        {
            string s = "Key pressed: " + (Keys)key + "\n";

            for (int i = 0; i < H; i++)
                if (listS[i].S > 0)
                {
                    s += listS[i] + "\n";
                    s += "\nThreshold: " + listS[i].Threshold + "\n\n\n";
                }
            return s;
        }
    }

    #endregion
}
