using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    static class GameManager
    {
        private static Map[] _maps = new Map[10];
        private static Entity[] _entities;


        public static void StartConsoleDungeon()
        {
            _maps[0] = new Map();
        }

        private static void GameLoop()
        {
            do
            {
                
            } while(true);
        }
    }
}

