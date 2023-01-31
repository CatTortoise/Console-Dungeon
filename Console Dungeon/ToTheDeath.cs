using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Console_Dungeon.My_IO;

namespace Console_Dungeon
{
    static class ToTheDeath
    {
        public static void Fight(Entity[] entities)
        {
            List<Entity> player = new List<Entity>();
            List<Entity> npc = new List<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity != null)
                {
                    if (entity.IsPlayer)
                    {
                        player.Add(entity);
                    }
                    else
                    {
                        npc.Add(entity);
                    }
                }
            }
            FightGameLoop(player.ToArray(), npc.ToArray());
        }

        private static void FightGameLoop(Entity[] player, Entity[] npc)
        {
            bool isNewFightLoop = true;
            bool fightOver = false;

                fightOver = false;
                isNewFightLoop = true;
                while (!fightOver)
                {
                    if (isNewFightLoop)
                    {
                        isNewFightLoop = false;
                        Console.Clear();
                    }
                    foreach (Entity Entity in player)
                    {
                        if (Entity != null)
                        {
                            FightRenderer.RenderArena(player, npc);
                            if (Entity.EntityAction != Action.Actions.Dead)
                            {
                            NpcActions.Act(FightRenderer.FightMenu(Entity), Entity, npc, false);
                            }
                            Console.Clear();
                        }
                    }

                    NpcActions.AutomaticActionSelection(npc,player);
                    FightRenderer.RenderArena(player, npc);
                    foreach (Entity Entity in player)
                    {
                        fightOver = true;
                        if (Entity.IsAlive)
                        {
                            fightOver = false;
                            break;
                        }
                    }
                    if (!fightOver)
                    {
                    fightOver = true;
                    foreach (Entity Entity in npc)
                        {
                            if (Entity.IsAlive)
                            {
                                fightOver = false;
                                break;
                            }
                        }
                    }
                    if(!fightOver)
                    {
                        PrintColoredMessage($"Press any key to continue\n", ConsoleColor.Yellow);
                        Console.ReadKey();
                        
                        FightRenderer.ResetEntitysAction(player);
                        FightRenderer.ResetEntitysAction(npc);
                    }
                Console.Clear();
            }

        }
        
    }
}
