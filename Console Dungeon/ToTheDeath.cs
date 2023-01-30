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
                                Act(FightRenderer.FightMenu(Entity), Entity, npc, false);
                            }
                            Console.Clear();
                        }
                    }

                    AutomaticActionSelection(npc,player);
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
        private static void AutomaticActionSelection(Entity[] acters, Entity[] targets)
        {
            foreach (Entity Entity in acters)
            {
                Action.Actions action = (Action.Actions)Random.Shared.Next(Action.numberOfActions);
                Act(action, Entity, targets,true);
            }   
        }

        private static int AutomaticTarget(Entity[] target)
        {
            return Random.Shared.Next(target.Length);
        }

        private static void Act(Action.Actions action, Entity acter, Entity[] targets, bool isAutomatic)
        {
            int temp = 0;
            switch (action)
            {
                case Action.Actions.DealDamage:
                    if (isAutomatic)
                    {
                        temp = AutomaticTarget(targets);
                    }
                    else
                    {
                        temp = FightRenderer.ChooseAction($"Choose target to attack by ID:", Element.ElementFirstColorDictionary[targets[0].ElementCode]);
                    }
                    if (temp >= targets.Length)
                    {
                        FightRenderer.InputErrer($"{temp+1} is an Invalid target Attacking Entity will go idle \n");
                        Action.GoIdol(acter);
                    }
                    else
                    { 
                        Action.DealDamage(targets[temp], acter); 
                    }
                    break;
                case Action.Actions.ShieldYourself:
                    Action.ShieldYourself(acter);
                    break;
                case Action.Actions.Heal:
                    Action.UseHeal(acter, (int)Math.Ceiling(acter.MaxHP * 0.2f));
                    break;
                case Action.Actions.UpgradEquipment:
                    Action.UpgradEquipment(acter, 2, Entity.Equipment.Weapon);
                    break;
                default:
                    Action.GoIdol(acter);                
                    break;
            }
        }


    }
}
