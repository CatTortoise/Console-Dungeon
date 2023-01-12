using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    static class Menu
    {
        private static Location _rootLocation;

        public static Location RootLocation { get => _rootLocation; private set => _rootLocation = value; }

        public static void RootMenu(ConsoleKey input)
        {

        }
    }
}
