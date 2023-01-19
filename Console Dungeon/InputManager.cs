using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Console_Dungeon
{
    static class InputManager
    {
        public enum inputType
        {
            Menu,
            Game
        }
        private static string _inputMessage;
        private static inputType _inputSystem = inputType.Game;
        private static ConsoleKey _keyInput;

        public static inputType InputSystem { get => _inputSystem; set => _inputSystem = value; }

        /*+GetKey()*/
        public static Location PlayerInput(Entity entity)
        {
            Location location = entity.Location;
            switch (InputSystem)
            {
                case inputType.Game:
                    PlayerGameInput(ref location);
                    break;
                case inputType.Menu:
                    location = Menu.MenuIndicator.Location;
                    PlayerMenuInput(ref location);
                    break;
            }
            return location;


        }

        private static void PlayerGameInput(ref Location location)
        {
            _keyInput = Console.ReadKey(true).Key;
            switch (_keyInput)
            {
                case ConsoleKey.Enter:
                    InputSystem = inputType.Menu;
                    break;
                case ConsoleKey.Escape:
                    //Back or close
                    break;
                case ConsoleKey.UpArrow:
                    location.Y += - 1;
                    break;
                case ConsoleKey.DownArrow:
                    location.Y += + 1;
                    break;
                case ConsoleKey.LeftArrow:
                    location.X += - 1;
                    break;
                case ConsoleKey.RightArrow:
                    location.X += 1;
                    break;
                default:
                    PlayerGameInput(ref location);
                    break;
            }

        }
        private static void PlayerMenuInput(ref Location location)
        {
            Renderer.PrinteMenu();
            Renderer.EntitiesQueue(Menu.MenuIndicator, Renderer.Screen.Menu);
            Renderer.Render();
            _keyInput = Console.ReadKey(true).Key;
            switch (_keyInput)
            {
                case ConsoleKey.Enter:
                    Menu.GetMenuChoice();
                    break;
                case ConsoleKey.Escape:
                    InputSystem = inputType.Game;
                    Menu.SetMenu(Menu.MenuType.Empty);
                    break;
                case ConsoleKey.UpArrow:
                    location.Y += -1;
                    break;
                case ConsoleKey.DownArrow:
                    location.Y += +1;
                    break;
                default:
                    PlayerMenuInput(ref location);
                    break;
            }
            
        }
    }
}
