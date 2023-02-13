using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    struct Location
    {
        
        /// <summary>
        /// x = Width 
        /// </summary>
        int _x = 0;
        public static readonly int Xmax = 160;
        /// <summary>
        /// y = height 
        /// </summary>
        int _y = 0;
        public static readonly int Ymax = 45;


        /// <summary>
        /// x = Width 
        /// </summary>
        public int X { get => _x; 
            set { 
                if(Xmax <= value)
                {
                    _x = Xmax - 1;
                }
                else
                {
                    _x = value;
                }
            }
        }

        /// <summary>
        /// y = height 
        /// </summary>
        public int Y { get => _y;
            set {
                if (Ymax <= value)
                {
                    _y = Ymax - 1;
                }
                else
                {
                    _y = value;
                }
            }
        }




        /// <summary>
        /// x = Width 
        /// y = height 
        /// </summary>
        public Location GetLocation()
        {
            return this;
        }
        public Location(int x,int y)
        {
            this.X = x;
            this.Y = y;
        }
        public Location(Location location)
        {
            this.X = location.X;
            this.Y = location.Y;
            
        }

        /// <summary>
        /// x = Width 
        /// y = height = 0 
        /// </summary>
        public Location(int x)
        {
            this.X = x;
            this.Y = 0;
        }

        /// <summary>
        /// x = Width = 0
        /// y = height = 0 
        /// </summary>
        public Location() 
        {
            this.X = 0;
            this.Y = 0;
        }

        /// <summary>
        /// Returns true if both locations are the same 
        /// </summary>
        public bool CompareLocations(Location compareToLocation) => CompareLocations(compareToLocation.X, compareToLocation.Y);
        public bool CompareLocations(int x, int y)
        {
            return this.X == x && this.Y == y;
        }
        public float CalculateDistance(int x, int y) => CalculateDistance(new(x, y));
        public float CalculateDistance(Location compareToLocation)
        {
            float xDistance = this.X - compareToLocation.X;
            float yDistance = this.Y - compareToLocation.Y;
            float res = 0;
            if (compareToLocation.X == 0)
            {
                res = yDistance;
            }
            else if (compareToLocation.Y == 0)
            {
                res = xDistance;
            }
            else
            {
                res = MathF.Sqrt(xDistance * xDistance + yDistance * yDistance);
            }
            return Math.Abs( res);
        }

    }		

}
