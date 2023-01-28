using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Console_Dungeon.My_IO;

namespace Console_Dungeon
{
    static class FightRenderer
    {
        private static int EntitySelection;

        public static void ResetEntitysAction(Entity[] Entitys)
        {
            foreach(Entity Entity in Entitys)
            {
                Action.GoIdol(Entity);
            }
        }
        public static void RenderArena(Entity[] firstCombatants, Entity[] secondCombatants)
        {
            RendererEntitys(firstCombatants);
            PrintColoredMessage("VS\n", ConsoleColor.Yellow);
            RendererEntitys(secondCombatants);
            VisualSeparator();
        }
        public static void RendererEntitys(Entity[] Entitys)
        {
            if (Entitys != null)
            {
                string _renderString = "";
                ConsoleColor color = Entitys[0].EntityColor;
                _renderString += $"Team: {Entitys[0].EntityColor}\n";
                int index = 0;
                foreach (Entity Entity in Entitys)
                {
                    if (Entity != null)
                    {
                        _renderString += "  ";
                        _renderString += Entity.GetEntityStats() + " (Action: " + Entity.EntityAction + ")\n";
                        index++;
                    }
                }
                PrintColoredMessage(_renderString, color);
            }
        }

        public static Action.Actions[] FightMenu(Entity[] Entitys)
        {
            Action.Actions[] result = new Action.Actions[Entitys.Length];
            string strForPrint = "";
            for (int i = 0; i < Entitys.Length; i++)
            {
                strForPrint += $"{Entitys[i].Name} needs orders:\n";
                for (int index = 0; index < Action.numberOfActions; index++)
                {
                    strForPrint += $"  {index + 1}) {(Action.Actions)index}\n";
                }
                PrintColoredMessage(strForPrint, ConsoleColor.Yellow);
                result[i] = (Action.Actions)ChooseAction("Choose Action ID:", Entitys[i].EntityColor);
                if(((int)result[i]) > Action.numberOfActions)
                {
                    result[i] = Action.Actions.GoIdol;
                }
            }

            return result;
        }
        public static Action.Actions FightMenu(Entity Entitys)
        {
            Entity[] Entity = new[] { Entitys };
            Action.Actions[] result =  FightMenu(Entity) ;
            return result[0];
        }
        public static int ChooseAction(string message ,ConsoleColor color)
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            int result = (GetIntInput(message) - 1);
            Console.ForegroundColor = foregroundColor;
            return result;
        }


        public static void InputErrer(string message)
        {
            ConsoleColor BackgroundColour = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            PrintColoredMessage(message, ConsoleColor.White);
            Console.BackgroundColor = BackgroundColour;
            Console.ReadKey();
        }
    }

}
