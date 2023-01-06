using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Map> maps = new List<Map>();
            maps.Add(new Map());
            maps[0].populateMap();
            Element.SetElementDictionary();
            Renderer.LaodMap(maps[0]);
            Renderer.MapRenderer();
        }

    }
}