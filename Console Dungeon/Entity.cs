using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    class Entity
    {
        private string _name;
        private Element.Elements _ElementCode;
        private Location _previousLocation;
        private Location _location;
        private ConsoleColor _color;
        private int _id;
        private bool _IsPlayer;
        private bool _isAlive;


        private float _hp;
        private int _strength;
        private int _stamina;
        private int _senses;
        private int _reactionSpeed;
        private int _toughness;
        private bool _isNotQueued;


        public Entity(string name, Location location, Element.Elements elementCode , int id)
        {
            Name = name;
            Location = location;
            PreviousLocation = Location;
            ElementCode = elementCode;
            Id = id;
        }
        public Entity(string name, Location location, Location previousLocation, Element.Elements elementCode, int id)
        {
            Name = name;
            Location = location;
            PreviousLocation = previousLocation;
            ElementCode = elementCode;
            Id = id;
        }
        public string Name { get => _name; private set => _name = value; }
        public Location Location { get => _location; private set => _location = value; }
        public Location PreviousLocation { get => _previousLocation; private set => _previousLocation = value; }
        public Element.Elements ElementCode { get => _ElementCode; private set => _ElementCode = value; }
        public int Id { get => _id; set => _id = value; }
        

        public void MoveTo(Location moveTo)
        {
            PreviousLocation = Location;
            Location = moveTo;
        }

    }
}
