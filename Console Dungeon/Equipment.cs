using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    class Equipment
    {
        public enum EquipmentType
        {
            Shield,
            Weapon,
            Consumable 
        }

        readonly int min = 1;
        readonly int max = 10;
        readonly int _maxShield = 100;
        readonly int _minShield = 1;

        private float _defense = 0;
        private string _name;
        private int _damage = 0;
        EquipmentType type;
        int _weight;


        public int Damage { get => _damage; private set => _damage = value; }
        private float _accuracy = 0;
        public float Accuracy { get => _accuracy; private set => _accuracy = value; }

        

        public Weapon()
        {
            Damage = Random.Shared.Next(min, max);
            Accuracy = Random.Shared.Next(min, max);
        }
        public Weapon(int damage, float accuracy)
        {
            Damage = damage;
            Accuracy = accuracy;
        }
        public void Upgrad(int IncreaseAmount)
        {
            Damage += IncreaseAmount;
            Accuracy += IncreaseAmount;
        }


    }
}
