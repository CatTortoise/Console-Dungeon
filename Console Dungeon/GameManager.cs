﻿using System;
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
            Renderer.SetScreens();
            Menu.SetMenu(Menu.MenuType.ArrayDataStructures);
            //Renderer.PrinteMenu();
            _maps[0] = new Map();
            Renderer.Render();
            GameLoop();

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
    }
}

