using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    static class Element
    {
        public enum Elements
        {
            # region Environment
            Wall,
            Doors,
            VisibleArea,
            #endregion
            # region Entities 
            Player,
            Goblin,
            Hob_Goblin,
            Minatore,
            Mimic,
            #endregion
            #region Interruptibles
            Trap,
            TreasureChest,
            Equipment,
            Consumables
            #endregion           
        }


    }
}
