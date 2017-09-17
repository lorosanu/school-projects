using System;
using System.Collections.Generic;
using System.Text;

namespace HeadTracking
{
    public class Moves
    {
        private string[] direction = { "Down", "Left", "Right", "Up" };

        public string this[int index]
        {
            get
            {
                if (index >= 0 && index < 4)
                    return direction[index];
                else
                    return "";
            }
        }

        public int this[string dir]
        {
            get
            {
                for (int i=0; i<4; i++)
                    if (direction[i] == dir)
                        return i;
                return -1;
            }
        }
    }
}
