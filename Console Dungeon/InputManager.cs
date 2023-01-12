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
        public static Location PlayerInput(Entity entity)
        {
            _keyInput = Console.ReadKey(true).Key;
            Location location = entity.Location;
            switch (_keyInput)
            {
                case ConsoleKey.Enter:
                    //Menu
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
                    location = PlayerInput(entity);
                    break;
            }
            return location;
        }
    }
}
