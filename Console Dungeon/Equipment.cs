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

        EquipmentType type;
        int _weight;


    }
}
