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
        private static int _currentMap = 0;
        private static int _nextNewMap = 0;

        public static void StartConsoleDungeon()
        {
            Console.CursorVisible = false;
            Renderer.SetScreens();
            Menu.SetMenu(Menu.MenuType.ArrayDataStructures);
            //Renderer.PrinteMenu();
            NewMap();
            Renderer.Render();
            GameLoop();
        }
        public static void ChangedFloor()
        {

        }

        private static void GameLoop()
        {
             
            do
            {
                foreach(Entity entity in _maps[0].MapEntities)
                {
                    if (entity != null)
                    {
                        if (entity.IsPlayer == true)
                        {
                            Renderer.EntitiesQueue(entity,Renderer.Screen.Map,true);
                            Renderer.Render();
                        }
                            if (InputManager.InputSystem == InputManager.inputType.Game)
                            {
                                _maps[0].MoveTo(InputManager.EntityInput(entity, _maps[0]), entity);
                            }
                        while (InputManager.InputSystem == InputManager.inputType.Menu)
                        {
                            Menu.MoveIndicator();
                        }
                        Renderer.Render();
                    }
                }
               
            } while(true); 
            
        }
        private static void NewMap()
        {
            int minSize = Random.Shared.Next(5, Location.Ymax);
            int maxSize = Random.Shared.Next(minSize, Location.Ymax);
            if(_currentMap == _nextNewMap) 
            { 
                _maps[_currentMap] = new Map(minSize, maxSize, 4 ,minSize / 3);
            }
            else
            {
                _maps[_nextNewMap] = new Map(minSize, maxSize, FilterPlayers(), minSize / 3, _nextNewMap);
            }
        }
        private static Entity[] FilterPlayers()
        {
            return _maps[_currentMap].MapEntities.Where(entity => entity.IsPlayer).ToArray();
        }
    }
}

