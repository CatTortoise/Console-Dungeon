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
        private bool hasADoor;

        public Location LocationTopLeft { get => _locationTopLeft; private set => _locationTopLeft = value; }
        public Location LocationBottomRight { get => _locationBottomRight; private set => _locationBottomRight = value; }
        public Element.Elements ElementCode { get => _ElementCode; private set => _ElementCode = value; }
        public string Name { get => _name; private set => _name = value; }

        public Envaironment(Envaironment envaironment)
        {
            Name = envaironment.Name;
            ElementCode = envaironment.ElementCode;
            LocationTopLeft = envaironment.LocationTopLeft;
            LocationBottomRight = envaironment.LocationBottomRight;
        }
        public Envaironment(string name, Element.Elements elementCode, Location locationTopLeft, Location locationBottomRight)
        {
            Name = name;
            ElementCode = elementCode;
            LocationTopLeft = locationTopLeft;
            LocationBottomRight = locationBottomRight;
        }
        public void EnvaironmentQueue(Renderer.Screen screen)
        {
            Renderer.EnvaironmentQueue(this, screen);
        }

        public void ChangeLocation(Location location)
        {
            LocationBottomRight = new(LocationBottomRight.X - LocationTopLeft.X + location.X, LocationBottomRight.Y - LocationTopLeft.Y + location.Y);
            LocationTopLeft = location;
        }

    }
}
