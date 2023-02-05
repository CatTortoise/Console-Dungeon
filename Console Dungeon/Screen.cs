//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Console_Dungeon
//{
//    class Screen
//    {
//        public enum ScreenType
//        {
//            Window,
//            Log,
//            Map,
//            Menu
//        }
//        private ScreenType _screen;  
//        private Element.Elements _elementCode;
//        private Location _locationTopLeft;
//        private Location _locationBottomRight;


//        public Location LocationTopLeft { get => _locationTopLeft; private set => _locationTopLeft = value; }
//        public Location LocationBottomRight { get => _locationBottomRight; private set => _locationBottomRight = value; }
//        public Element.Elements ElementCode { get => _elementCode; private set => _elementCode = value; }
//        public string Name { get => _name; private set => _name = value; }
//        internal ScreenType Screen { get => _screen; set => _screen = value; }

//        public Screen(ScreenType screenType, Element.Elements elementCode, Location locationTopLeft, Location locationBottomRight)
//        {
//            Screen = screenType;
//            ElementCode = elementCode;
//            LocationTopLeft = locationTopLeft;
//            LocationBottomRight = locationBottomRight;
//        }
//    }
//}

