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

        public static void MapRenderer()
        {
            Console.SetCursorPosition(_entities[0].Location.X, _entities[0].Location.Y);
            Console.Write(ElementDictionary[_entities[0].ElementCode]);
            Console.SetCursorPosition(_entities[0].Location.X, _entities[0].Location.Y);
            Console.Write(ElementDictionary[Elements.NonVisibleArea]);
           _map.MoveTo(new Location(0,0), _entities[0]);
            Console.SetCursorPosition(_entities[0].Location.X, _entities[0].Location.Y);
            Console.Write(ElementDictionary[_entities[0].ElementCode]);

        }

        public static void Render()
        {
            
        }

    }
}
