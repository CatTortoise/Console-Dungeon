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
        private static string _inputMessage;
        private static ConsoleKey _keyInput;
        /*+GetKey()*/
        private static Location PlayerInput()
        {
            _keyInput = Console.ReadKey(false).Key;
            Location location = new();
            switch (_keyInput)
            {
                case ConsoleKey.Enter:
                    //Menu
                    break;
                case ConsoleKey.Escape:
                    //Back or close
                    break;
                case ConsoleKey.UpArrow:
                    location.X = - 1;
                    break;
                case ConsoleKey.DownArrow:
                    location.X = 1;
                    break;
                case ConsoleKey.LeftArrow:
                    location.Y = -1;
                    break;
                case ConsoleKey.RightArrow:
                    location.Y = 1;
                    break;
                default:
                    location = PlayerInput();
                    break;
            }
            return location;
        }
    }
}
