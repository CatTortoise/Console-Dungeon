using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Console_Dungeon.Element;

namespace Console_Dungeon
{
    static class Renderer
    {
        private static Map _map;
        private static Entity[] _entities;
        private static int _windowHeight;
        private static int _windowWidth;

        public static void LaodMap(Map newMap)
        {
            _map = newMap;
            _entities = _map.mapEntities;
        }
        public static void Erasure(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                char str;
                Elements elements = Elements.NonVisibleArea;
                Console.SetCursorPosition(location.X, location.Y);
                //Checks for another Entity at the location 
                //Checks for another Interruptible at the location 
                //Checks for another Envaironment at the location 
                ElementDictionary.TryGetValue(elements, out str);
                Console.Write(str);
            }
        }

        public static void MapRenderer()
        {
            
        }

        public static void Render()
        {
            
        }

    }
}
