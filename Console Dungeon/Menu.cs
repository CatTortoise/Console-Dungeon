﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    static class Menu
    {
        #region enum

        public enum MenuType
        {
            ArrayDataStructures
        }
        #endregion
        #region Location
        private static Location _startLocation;
        private static Location _endLocation;
        private static Location _startIndicatorLocation;
        #endregion
        
        private static MenuType _curentMenuType = MenuType.ArrayDataStructures;
        private static Dictionary<MenuType, Location> _startIndicatorLocationDictionary = new Dictionary<MenuType, Location> { { MenuType.ArrayDataStructures, new(0, 1) } };
        private static Entity menuIndicator = new("MenuIndicator", _startIndicatorLocation, Element.Elements.MenuIndicator, 1);

        public static Dictionary<MenuType, string> Menus { get => _menus; private set => _menus = value; }
        public static Dictionary<MenuType, Location> Endlocation { get => _endlocation; private set => _endlocation = value; }
        public static Entity MenuIndicator { get => menuIndicator; private set => menuIndicator = value; }
       

        
        static Dictionary<MenuType, Location> _startlocation = new Dictionary<MenuType, Location>
        {
            {
                MenuType.ArrayDataStructures,new(0,0)
            }
        };
        static Dictionary<MenuType, Location> _endlocation = new Dictionary<MenuType, Location>
        {
            {
                MenuType.ArrayDataStructures,new(0,8)
            }
        };

        static Dictionary<MenuType, string> _menus = new Dictionary<MenuType, string>()
            {
                {MenuType.ArrayDataStructures,"Data Structures - Array\n"  +
                                                "\t1. Configure initial array size\n" +
                                                "\t2. Insert an item\n" +
                                                "\t3. Delete an item\n" +
                                                "\t4. Show the number of items in the array\n" +
                                                "\t5. Print all items\n" +
                                                "\t6. Exit\n" }
            };
        
        public static void SetMenu(MenuType menuType)
        {
            _curentMenuType = menuType;

            _startlocation.TryGetValue(_curentMenuType, out _startLocation);
            _endlocation.TryGetValue(_curentMenuType, out _endLocation);

            _startIndicatorLocationDictionary.TryGetValue(_curentMenuType, out _startIndicatorLocation);
            menuIndicator.MoveTo(_startIndicatorLocation, _startIndicatorLocation);
        }
        public static void MoveIndicator()
        {
            Location location;
            location = InputManager.PlayerInput(MenuIndicator);
            if (location.Y < _endLocation.Y - 1 && location.Y >= _startIndicatorLocation.Y)
            {
                MenuIndicator.MoveTo(location);
                Renderer.EntitiesQueue(Menu.MenuIndicator, Renderer.Screen.Window);
            }

        }
        public static void GetStarEndLocations(out Location locationStart, out Location locationEnd)
        {
            locationStart = _startLocation;
            locationEnd = _endLocation;
        }

        public static string GetMenuString()
        {
            string str;
            Menus.TryGetValue(_curentMenuType, out str);
            return str;
        }

        public static int GetMenuChoice()
        {
            return MenuIndicator.Location.Y - _startIndicatorLocation.Y;
        }


    }
}
