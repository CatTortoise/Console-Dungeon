using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    class Interruptible
    {

        public enum ItemTayp
        {
            NotAnItem,
            Weapon,
            Shield,
            Consumable
        }
        private string _name;
        private Element.Elements _elementCode;
        private Element.ElementsTayp _elementsTayp = Element.ElementsTayp.Interruptibles;
        private ItemTayp _thisItemTayp;
        private Location _location;
        private bool _isHidden;
        private bool _isTriggered;
        private bool _isBlocking;


        public string Name { get => _name; private set => _name = value; }
        public Element.Elements ElementCode { get => _elementCode; private set => _elementCode = value; }
        public Location Location { get => _location; private set => _location = value; }
        public bool IsHidden { get => _isHidden; private set => _isHidden = value; }
        public bool IsTriggered { get => _isTriggered; private set => _isTriggered = value; }
        public ItemTayp ThisItemTayp { get => _thisItemTayp; private set => _thisItemTayp = value; }
        public bool IsBlocking { get => _isBlocking; private set => _isBlocking = value; }
        public Element.ElementsTayp ElementsTayp { get => _elementsTayp; private set => _elementsTayp = value; }

        public Interruptible(string name, Element.Elements elementCode, ItemTayp itemTayp, Location location, bool isHidden, bool isTriggered, bool isBlocking)
        {
            Name = name;
            ElementCode = elementCode;
            Location = location;
            IsHidden = isHidden;
            IsTriggered = false;
            ThisItemTayp = itemTayp;
            IsBlocking = isBlocking;
        }
        public Interruptible(Interruptible interruptible)
        {
            Name = interruptible.Name;
            ElementCode = interruptible.ElementCode;
            Location = interruptible.Location;
            IsHidden = interruptible.IsHidden;
            IsTriggered = interruptible.IsTriggered;
            ThisItemTayp = interruptible.ThisItemTayp;
            IsBlocking = interruptible.IsBlocking;
        }
        public void InteractWith(Entity entityInteractor)
        {
            switch (_elementCode)
            {
                case (Element.Elements.DoorHorizontal or Element.Elements.DoorVertical):
                    DoorInteractions();
                    break;
                case (Element.Elements.TreasureChest):
                    break;
                case (Element.Elements.Item):
                    switch (ThisItemTayp)
                    {
                        case ItemTayp.Weapon:
                            break;
                        case ItemTayp.Shield:
                            break;
                        default:
                            break;
                    }
                    break;
                case (Element.Elements.TrapArmed):
                    break;
                case (Element.Elements.TrapDisarmed):
                    break;
            }

        }

        private void DoorInteractions()
        {
            if (IsBlocking)
            {
                IsBlocking = false;
                if (ElementCode == Element.Elements.DoorHorizontal)
                    ElementCode = Element.Elements.DoorVertical;
                else
                    ElementCode = Element.Elements.DoorHorizontal;
            }
            else
            {
                IsBlocking = true;
                if (ElementCode == Element.Elements.DoorHorizontal)
                    ElementCode = Element.Elements.DoorVertical;
                else
                    ElementCode = Element.Elements.DoorHorizontal;
            }
        }
    }
}
