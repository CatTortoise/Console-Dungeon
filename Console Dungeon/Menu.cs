using System;
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
            Empty,
            ChangedFloor
        }
        #endregion
        #region Location
        private static Location _startLocation;
        private static Location _endLocation;
        private static Location _startIndicatorLocation;
        #endregion

        private static MenuType _curentMenuType;
        private static Dictionary<MenuType, Location> _startIndicatorLocationDictionary = new Dictionary<MenuType, Location> { { MenuType.ChangedFloor, new(1, 2) } };
        private static Entity menuIndicator = new("MenuIndicator",true, _startIndicatorLocation, _startIndicatorLocation, Element.Elements.MenuIndicator, 1,false);

        public static Dictionary<MenuType, string[]> Menus { get => _menus; private set => _menus = value; }
        public static Dictionary<MenuType, Location> Endlocation { get => _endlocation; private set => _endlocation = value; }
        public static Entity MenuIndicator { get => menuIndicator; private set => menuIndicator = value; }
        public static MenuType CurentMenuType { get => _curentMenuType; private set => _curentMenuType = value; }



        static Dictionary<MenuType, string[]> _menus = new Dictionary<MenuType, string[]>()
        {
            {
                MenuType.Empty , new string[]
                {
                    ""
                }
            },
            {
                MenuType.ChangedFloor,new string[]
                {
                    "Stairs:\n",
                    " 1. Advance \n",
                    " 2. Go back \n",
                }
            }
        };

        static Dictionary<MenuType, Location> _startlocation = new Dictionary<MenuType, Location>
        {
            {
                MenuType.Empty, new(0, 0) 
            },
            {
                MenuType.ChangedFloor,new(0,1)
            }
        };
        static Dictionary<MenuType, Location> _endlocation = new Dictionary<MenuType, Location>
        {
            {
                MenuType.Empty, new(0, 0)
            },
            {
                MenuType.ChangedFloor,new(0,Menus[MenuType.ChangedFloor].Length)
            }
        };

        

    

        public static void SetMenu(MenuType menuType)
        {
            CurentMenuType = menuType;

            _startLocation = _startlocation[CurentMenuType];
            _endLocation = _endlocation[CurentMenuType];

            _startIndicatorLocationDictionary.TryGetValue(CurentMenuType, out _startIndicatorLocation);
            menuIndicator.MoveTo(_startIndicatorLocation, _startIndicatorLocation);
        }
        
        public static void MoveIndicator()
        {
            Location location;
            location = InputManager.EntityInput(MenuIndicator);
            if (location.Y <= _endLocation.Y && location.Y >= _startIndicatorLocation.Y)
            {
                MenuIndicator.MoveTo(location);
                    
            }
            Renderer.EntitiesQueue(Menu.MenuIndicator, Renderer.Screen.Menu);

        }
        public static void GetStarEndLocations(Location ofsetLocation , out Location locationStart, out Location locationEnd)
        {
            locationStart = new(_startLocation.X + ofsetLocation.X, _startLocation.Y + ofsetLocation.Y);
            locationEnd = new(_endLocation.X + ofsetLocation.X, _endLocation.Y + ofsetLocation.Y) ;
        }
        public static void GetStarEndLocations(out Location locationStart, out Location locationEnd)
        {
            locationStart = _startLocation ;
            locationEnd = _endLocation;
        }
        public static void PopCurentMenuType()
        {
            
        }


        public static int GetMenuChoice()
        {
            return MenuIndicator.Location.Y - _startIndicatorLocation.Y;
        }


    }
}
