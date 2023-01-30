using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    class Interruptible
    {

        private string _name;
        private Element.Elements _ElementCode;
        private Location _location;
        private bool _isHidden;
        private bool _isTriggered;


        public string Name { get => _name; private set => _name = value; }
        public Element.Elements ElementCode { get => _ElementCode; private set => _ElementCode = value; }
        public Location Location { get => _location; private set => _location = value; }
        public bool IsHidden { get => _isHidden; private set => _isHidden = value; }
        public bool IsTriggered { get => _isTriggered; private set => _isTriggered = value; }


        public Interruptible(string name, Element.Elements elementCode, Location location, bool isHidden, bool isTriggered)
        {
            Name = name;
            ElementCode = elementCode;
            Location = location;
            IsHidden = isHidden;
            IsTriggered = isTriggered;
        }

        public void InteractWith(Entity entity)
        {
            switch (_ElementCode)
            {
                case (Element.Elements.DoorHorizontal or Element.Elements.DoorVertical):
                    break;
                case (Element.Elements.TreasureChest):
                    break;
                case (Element.Elements.Item):

                    break;
                case (Element.Elements.TrapArmed):
                    break;
                case (Element.Elements.TrapDisarmed):
                    break;
            }



        }
    }
}
