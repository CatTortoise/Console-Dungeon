﻿using System;
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
            NonVisibleArea,
            Wall,
            DoorVertical,
            DoorHorizontal,
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
            TrapArmed,
            TrapDisarmed,
            TreasureChest,
            Item,
            #endregion
            #region Menu
            Empty,
            MenuIndicator,
            IsString
            #endregion

        }
        public enum ElementsTayp
        {
            Empty,
            Environment,
            Entities,
            Interruptibles,
            Menu
        }
        

        static private Dictionary<Elements, char> _elementDictionary = new Dictionary<Elements, char>()
            {
                { Elements.NonVisibleArea,(char)32 },
                { Elements.Wall, (char)35 },
                { Elements.DoorVertical, (char)124 },
                { Elements.DoorHorizontal, (char)95 },
                { Elements.VisibleArea, (char)183 },
                { Elements.Player, (char)2 },
                { Elements.Goblin, (char)103 },
                { Elements.Hob_Goblin, (char)71 },
                { Elements.Minatore, (char)77 },
                { Elements.Mimic, (char)63 },
                { Elements.TrapArmed, (char)15 },
                { Elements.TrapDisarmed, (char)42 },
                { Elements.TreasureChest, (char)63 },
                { Elements.Item, (char)36 },
                { Elements.Empty,(char)32 },
                { Elements.MenuIndicator, (char)187 }
            };


        public static Dictionary<Elements, char> ElementDictionary { get => _elementDictionary; private set => _elementDictionary = value; }


    }
}
