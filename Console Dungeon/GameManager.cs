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


        public static void StartConsoleDungeon()
        {
            Console.CursorVisible = false;
            _maps[0] = new Map();
            Renderer.Render();
            GameLoop();
        }

        private static void GameLoop()
        {
            do
            {
                foreach(Entity entity in _maps[0].mapEntities)
                {
                    _maps[0].MoveTo(InputManager.PlayerInput(entity), entity);
                }
                Renderer.Render();
            } while(true);
        }
    }
}

