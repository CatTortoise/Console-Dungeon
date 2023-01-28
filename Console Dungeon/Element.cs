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
            NonVisibleArea,
            Wall,
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
            DoorVertical,
            DoorHorizontal,
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
        public enum ElementsStats
        {
            HP,
            strength,
            Energy,
            senses,
            reactionSpeed,
            toughness
        }

        static private Dictionary<Elements, char> _elementDictionary = new Dictionary<Elements, char>()
            {
                { Elements.NonVisibleArea,(char)32 },
                { Elements.Wall, (char)35 },
                { Elements.VisibleArea, (char)183 },
                { Elements.Player, (char)2 },
                { Elements.Goblin, (char)103 },
                { Elements.Hob_Goblin, (char)71 },
                { Elements.Minatore, (char)77 },
                { Elements.Mimic, (char)63 },
                { Elements.DoorVertical, (char)124 },
                { Elements.DoorHorizontal, (char)95 },
                { Elements.TrapArmed, (char)15 },
                { Elements.TrapDisarmed, (char)42 },
                { Elements.TreasureChest, (char)63 },
                { Elements.Item, (char)36 },
                { Elements.Empty,(char)32 },
                { Elements.MenuIndicator, (char)187 }
            };
        static private Dictionary<Elements, ConsoleColor> _elementForegroundColorDictionary = new Dictionary<Elements, ConsoleColor>()
            {
                { Elements.NonVisibleArea,ConsoleColor.White },
                { Elements.Wall, ConsoleColor.White },
                { Elements.DoorVertical, ConsoleColor.Gray },
                { Elements.DoorHorizontal, ConsoleColor.Gray },
                { Elements.VisibleArea, ConsoleColor.DarkGray },
                { Elements.Player, ConsoleColor.White },
                { Elements.Goblin, ConsoleColor.Green },
                { Elements.Hob_Goblin, ConsoleColor.DarkGreen },
                { Elements.Minatore, ConsoleColor.Red },
                { Elements.Mimic, ConsoleColor.Yellow },
                { Elements.TrapArmed, ConsoleColor.Yellow },
                { Elements.TrapDisarmed, ConsoleColor.DarkYellow },
                { Elements.TreasureChest, ConsoleColor.Yellow },
                { Elements.Item, ConsoleColor.Blue },
                { Elements.Empty,ConsoleColor.White },
                { Elements.MenuIndicator, ConsoleColor.White }
            };


        public static Dictionary<Elements, char> ElementDictionary { get => _elementDictionary; private set => _elementDictionary = value; }
        public static Dictionary<Elements, ConsoleColor> ElementForegroundColorDictionary { get => _elementForegroundColorDictionary; set => _elementForegroundColorDictionary = value; }
    }
}
