using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    class Envaironment
    {
        private string _name;
        private Element.Elements _ElementCode;
        private Location _locationTopLeft;
        private Location _locationBottomRight;

        public Location LocationTopLeft { get => _locationTopLeft; private set => _locationTopLeft = value; }
        public Location LocationBottomRight { get => _locationBottomRight; private set => _locationBottomRight = value; }
        public Element.Elements ElementCode { get => _ElementCode; set => _ElementCode = value; }

        public Envaironment(string name, Element.Elements elementCode, Location locationTopLeft, Location locationBottomRight)
        {
            _name = name;
            ElementCode = elementCode;
            LocationTopLeft = locationTopLeft;
            LocationBottomRight = locationBottomRight;
            Renderer.EnvaironmentQueue(this);
        }

    }
}
