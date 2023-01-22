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
            
            //Entity[] player = new Entity[entities.Length];
            //Entity[] npc = new Entity[entities.Length];
            //int playerIndex = 0;
            //int npcIndex = 0;
            //foreach (Entity entity in entities)
            //{
            //    if (entity.IsPlayer)
            //    {
            //        player[playerIndex] = entity;
            //        playerIndex++;
            //    }
            //    else
            //    {
            //        npc[npcIndex] = entity;
            //        npcIndex++;
            //    }
            //}
            //player = player;
            FightGameLoop(entities.Where<Entity>(entiy => entiy != null && entiy.IsPlayer).ToArray(), entities.Where<Entity>(entiy => entiy != null && !entiy.IsPlayer).ToArray());
        }

        private static void FightGameLoop(Entity[] player, Entity[] npc)
        {
            bool isNewFightLoop = true;
            bool fightOver = false;
            int numberOfEntitys = 4;
            do
            {
                fightOver = false;
                isNewFightLoop = true;
                while (!fightOver)
                {
                    if (isNewFightLoop)
                    {
                        //InstantiateEntitys(player, "Entity", 10, ConsoleColor.Blue);
                        //InstantiateEntitys(npc, "Minotaur", 5, ConsoleColor.Red);
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
                        if (Entity.EntityAction != Action.Actions.Dead)
                        {
                            fightOver = false;
                            break;
                        }
                    }
                    if (!fightOver)
                    {
                        foreach (Entity Entity in npc)
                        {
                            if (Entity.EntityAction != Action.Actions.Dead)
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
                        Console.Clear();
                        FightRenderer.ResetEntitysAction(player);
                        FightRenderer.ResetEntitysAction(npc);
                    }
                }
                if (fightOver)
                {
                    Console.Clear();
                    FightRenderer.Winner(player[0]);
                    FightRenderer.Winner(npc[0]);
                }
                Array.Clear(player);
                Array.Clear(npc);

            } while (!ExitCheck(""));
        }
        private static void AutomaticActionSelection(Entity[] acters, Entity[] targets)
        {
            foreach (Entity Entity in acters)
            {
                Action.Actions action = (Action.Actions)Random.Shared.Next(Action.numberOfActions);
                Act(action, Entity, targets,true);
            }   
        }

        //private static Entity[] InstantiateEntitys(Entity[] player, string name, int maxHP, ConsoleColor color)
        //{

        //    //for (int i = 0; i < player.Length; i++)
        //    //{
        //    //    int maxEvasion = Random.Shared.Next(1, 5);
        //    //    int minEvasion = Random.Shared.Next(maxEvasion);
        //    //    Shield shield = new Shield();
        //    //    Weapon weapon = new Weapon();
        //    //    player[i] = new($"ID:({i + 1}) {name}_{i + 1}", maxHP, shield, weapon, minEvasion, maxEvasion, color);
        //    //}
        //    //return player;
        //}

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
                        temp = FightRenderer.ChooseAction($"Choose target to attack by ID:", targets[0].EntityColor);
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
                    Action.UseHeal(acter, 10);
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
