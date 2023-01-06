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
        private Location _location;
        private int _id;
        private bool _isAlive;


        private float _hp;
        private int _strength;
        private int _stamina;
        private int _senses;
        private int _reactionSpeed;
        private int _toughness;

        public Entity(string name, Location location, Element.Elements elementCode , int id)
        {
            Name = name;
            Location = location;
            ElementCode = elementCode;
            Id = id;
        }

        public string Name { get => _name; private set => _name = value; }
        public Location Location { get => _location; private set => _location = value; }
        public Element.Elements ElementCode { get => _ElementCode; private set => _ElementCode = value; }
        public int Id { get => _id; set => _id = value; }

        public void MoveTo(Location moveTo)
        {
            Location = moveTo;
        }


    }
}
