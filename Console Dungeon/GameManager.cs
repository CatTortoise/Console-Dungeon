//Basic C# for Games
//Dor Ben-Dor
//Final Project 
//Yshai flising
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
            Menu.SetMenu(Menu.MenuType.ChangedFloor);
            ChangedFloor(0);
            Renderer.Render();
            GameLoop();
        }
        public static void ChangedFloor(int choice)
        {
            InputManager.InputSystem = InputManager.inputType.Game;
            Renderer.Render();
            Console.Clear();
            Renderer.ScreensQueue();
            if (choice == 0)
            {
                if (_nextNewMap < _maps.Length)
                {
                    if (_maps[_currentMap + 1] == null)
                    {
                        NewMap();
                    }
                    else
                    {
                        _currentMap++;
                    }
                }
            }
            else if(_currentMap > 0)
            {
                _currentMap--;
            }
            _maps[_currentMap].LoadeAllMapElements();
        }

        private static void GameLoop()
        {
            bool isGameOver = true;
            do
            {
                isGameOver = true;
                foreach (Entity entity in _maps[_currentMap].MapEntities)
                {
                    if (entity != null && entity.IsAlive)
                    {
                        if (entity.IsPlayer == true)
                        {
                            isGameOver = false;
                            Renderer.EntitiesQueue(entity,Renderer.Screen.Map,true);
                            Renderer.Render();
                        }
                            if (InputManager.InputSystem == InputManager.inputType.Game)
                            {
                                _maps[_currentMap].MoveTo(InputManager.EntityInput(entity, _maps[_currentMap]), entity);
                            }
                        while (InputManager.InputSystem == InputManager.inputType.Menu)
                        {
                            Menu.MoveIndicator();
                            break;
                        }
                        Renderer.Render();
                    }
                }
               
            } while(!isGameOver);
            Console.Clear();
            My_IO.PrintColoredMessage("Game Over", ConsoleColor.Red);
        }
        private static void NewMap()
        {
            int minSize = Random.Shared.Next(5, Location.Ymax);
            int maxSize = Random.Shared.Next(minSize, Location.Ymax);
            if(_currentMap == _nextNewMap) 
            { 
                _maps[_nextNewMap] = new Map(minSize, maxSize, 4 ,minSize / 3);
            }
            else
            {
                _maps[_nextNewMap] = new Map(minSize, maxSize, FilterPlayers(), minSize / 3, _nextNewMap);
            }
            if(_nextNewMap != _maps.Length)
            {
                _currentMap = _nextNewMap;
                _nextNewMap++;
            }
            
        }
        private static Entity[] FilterPlayers()
        {
            List<Entity> entities = new List<Entity>();
            foreach (Entity entity in _maps[_currentMap].MapEntities)
            {
                if (entity.IsPlayer)
                {
                    entities.Add(entity);
                }
            }
            return entities.ToArray();
        }
    }
}

