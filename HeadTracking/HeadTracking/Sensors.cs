using System;
using System.Collections.Generic;
using System.Text;
using WiimoteLib;

namespace HeadTracking
{
    #region Class_Sensors

    public class Sensors
    {
        private IRSensor p1, p2;
        private double z;

        public Sensors()
        {
            p1 = new IRSensor();
            p2 = new IRSensor();
            z = 0;
        }

        public Sensors(IRSensor s1, IRSensor s2)
        {
            p1 = s1;
            p2 = s2;
            z = Distance(p1, p2);
        }

        public Sensors(Sensors s)
        {
            p1 = s.P1;
            p2 = s.P2;
            z = s.Z;
        }

        private double Distance(IRSensor s1, IRSensor s2)
        {
            double d = 0;
            int x1 = s1.RawPosition.X;
            int y1 = s1.RawPosition.Y;
            int x2 = s2.RawPosition.X;
            int y2 = s2.RawPosition.Y;

            d = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

            return d;
        }

        private IRSensor middlePoint(IRSensor s1, IRSensor s2)
        {
            IRSensor middle = new IRSensor();
            middle.RawPosition.X = (s1.RawPosition.X + s2.RawPosition.X) / 2;
            middle.RawPosition.Y = (s1.RawPosition.Y + s2.RawPosition.Y) / 2;
            return middle;
        }

        #region Properties

        public IRSensor MiddlePoint
        {
            get
            {
                return middlePoint(p1, p2);
            }
        }

        public double XDistance
        {
            get
            {
                return (p1.RawPosition.X - p2.RawPosition.X);
            }
        }

        public double YDistance
        {
            get
            {
                return (p1.RawPosition.Y - p2.RawPosition.Y);
            }
        }

        public double Z
        {
            get
            {
                return z;
            }
        }

        public double Angle
        {
            get
            {
                return Math.Abs( Math.Asin(YDistance/Z) * 180 / Math.PI) ;
            }
        }

        public IRSensor P1
        {
            get
            {
                return p1;
            }
        }

        public IRSensor P2
        {
            get
            {
                return p2;
            }
        }

        #endregion

        #region PublicMethods

        public bool Left(Sensors s)        
        {
            IRSensor m1 = MiddlePoint;
            IRSensor m2 = s.MiddlePoint;

            if (m1.RawPosition.X > m2.RawPosition.X)
                    return true;
            return false;
        }

        public bool Right(Sensors s)
        {
            IRSensor m1 = MiddlePoint;
            IRSensor m2 = s.MiddlePoint;

            if ( m1.RawPosition.X < m2.RawPosition.X )
                    return true;
            return false;
        }

        public bool Up(Sensors s)
        {
            IRSensor m1 = MiddlePoint;
            IRSensor m2 = s.MiddlePoint;

            if ( m1.RawPosition.Y > m2.RawPosition.Y )
                return true;
            return false;
        }

        public bool Down(Sensors s)
        {
            IRSensor m1 = MiddlePoint;
            IRSensor m2 = s.MiddlePoint;

            if ( m1.RawPosition.Y < m2.RawPosition.Y )
                return true;
            return false;
        }

        public bool Closer(Sensors s)
        {
            if (z  > s.Z )
                return true;
            return false;
        }

        public bool Farther(Sensors s)
        {
            if ( z < s.Z )
                return true;
            return false;
        }

        public bool Equals(Sensors s)
        {
            IRSensor m1 = this.MiddlePoint;
            IRSensor m2 = s.MiddlePoint;
            if ( Math.Abs(m1.RawPosition.X - m2.RawPosition.X) >= 3 || Math.Abs(m1.RawPosition.Y - m2.RawPosition.Y) >= 3)
                    return false;
            return true;
        }

        #endregion

        public override string ToString()
        {
            return "{ (" + p1.RawPosition.X + ", " + p1.RawPosition.Y + ")   ( " + p2.RawPosition.X + ", " + p2.RawPosition.Y + ") }";
        }
    }

    #endregion
}
