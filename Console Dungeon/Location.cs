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
        int _x;
        /// <summary>
        /// y = height 
        /// </summary>
        int _y;

        /// <summary>
        /// x = Width 
        /// </summary>
        public int X { get => _x; set => _x = value; }

        /// <summary>
        /// y = height 
        /// </summary>
        public int Y { get => _y; set => _y = value; }


        /// <summary>
        /// x = Width 
        /// y = height 
        /// </summary>
        public Location(int x, int y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// x = Width 
        /// y = height = 0 
        /// </summary>
        public Location(int x)
        {
            _x = x;
            _y = 0;
        }

        /// <summary>
        /// x = Width = 0
        /// y = height = 0 
        /// </summary>
        public Location() 
        {
            _x = 0;
            _y = 0;
        }


    }		

}
