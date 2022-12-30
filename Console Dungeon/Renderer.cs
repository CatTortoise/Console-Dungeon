using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    static class Renderer
    {
        private static string _map;
        private static int _windowHeight;
        private static int _windowWidth;

        public static void LaodMap(string newMap)
        {
            _map = newMap;
        }
        public static void LaodMap(char[][] newMap)
        {

        }

        public static void MapRenderer()
        {
            Console.WriteLine(_map);
        }
    }
}
