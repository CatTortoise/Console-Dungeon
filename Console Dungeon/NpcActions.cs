//Basic C# for Games
//Dor Ben-Dor
//Final Project 
//Yshai flising
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Console_Dungeon
{
    static class NpcActions
    {
        public static ConsoleKey MoveNpc(Entity entity,Map map)
        {
            switch (entity.ElementCode)
            {
                case Element.Elements.Player:
                    return LookAround(entity, map);
                case Element.Elements.Goblin:
                    return LookAround( entity,  map);
                case Element.Elements.Hob_Goblin:
                    return LookAround(entity, map);
                case Element.Elements.Minatore:
                    return LookAround(entity, map);
                case Element.Elements.Mimic:
                    break;
                default:
                    break;
            }
            return RandomInput();
        }
        public static ConsoleKey RandomInput()
        {
            ConsoleKey consoleKey = new();
            int ran = Random.Shared.Next(4);
            switch (ran)
            {
                case 0:
                    consoleKey = ConsoleKey.UpArrow;
                    break;
                case 1:
                    consoleKey = ConsoleKey.DownArrow;
                    break;
                case 2:
                    consoleKey = ConsoleKey.LeftArrow;
                    break;
                case 3:
                    consoleKey = ConsoleKey.RightArrow;
                    break;
            }
            return consoleKey;
        }

        public static ConsoleKey LookAround(Entity entity, Map map)
        {
            Location closestFriendly = new();
            Location closestHostile = new();
            for (int x = entity.Location.X - entity.Senses; x <= entity.Location.X + entity.Senses; x++)
            {
                for (int y = entity.Location.Y - entity.Senses; y <= entity.Location.Y + entity.Senses; y++)
                {
                    foreach(Entity mapEntity in map.MapEntities)
                    {
                        if (mapEntity.Location.CompareLocations(new(x, y)) && !entity.Location.CompareLocations(mapEntity.Location))
                        {
                            if ( mapEntity.CollidedWithHostile(entity.ElementCode))
                            {
                                float hostileDistance = mapEntity.Location.CalculateDistance(entity.Location);
                                if (hostileDistance <= closestHostile.CalculateDistance(entity.Location)) 
                                {
                                    closestHostile = mapEntity.Location;
                                }                                
                            }
                            else
                            {
                                float friendlyDistance = mapEntity.Location.CalculateDistance(entity.Location);
                                if (friendlyDistance <= closestFriendly.CalculateDistance(entity.Location))
                                {
                                    closestFriendly = mapEntity.Location;
                                }
                            }
                        }
                        
                    }
                    
                }
            }
            return EntityBehaviour(entity, closestFriendly, closestHostile);
        }
        private static ConsoleKey EntityBehaviour(Entity entity, Location closestfriendly ,Location closestHostile)
        {
            switch (entity.ElementCode)
            {
                case Element.Elements.Player:
                    return RandomInput();
                case Element.Elements.Goblin:
                    if (closestHostile.X != 0)
                    {
                        float distance = entity.Location.CalculateDistance(closestfriendly);
                        float xDistance = entity.Location.CalculateDistance(closestHostile.X, 0);
                        float yDistance = entity.Location.CalculateDistance(0, closestHostile.Y);
                        if (xDistance != 0 &&( yDistance == 0 || xDistance < yDistance))
                        {
                            if (entity.Location.X > closestHostile.X)
                            {
                                if (distance > 0 && distance <= 2)
                                    return ConsoleKey.LeftArrow;
                                else
                                    return ConsoleKey.RightArrow;
                            }
                            else
                            {
                                if (distance > 0 && distance <= 2)
                                    return ConsoleKey.RightArrow;
                                else
                                    return ConsoleKey.LeftArrow;
                            }
                        }
                        else
                        {
                            if (entity.Location.Y > closestHostile.Y)
                            {
                                if (distance > 0 && distance <= 3)
                                    return ConsoleKey.UpArrow;
                                else
                                    return ConsoleKey.DownArrow;
                            }
                            else
                            {
                                if (distance > 0 && distance <= 3)
                                    return ConsoleKey.DownArrow;
                                else
                                    return ConsoleKey.UpArrow;
                            }
                        }

                    }
                    break;
                case Element.Elements.Hob_Goblin:
                    break;
                case Element.Elements.Minatore:
                    break;
                case Element.Elements.Mimic:
                    break;
                default:
                    break;
            }
            return RandomInput();
        }

        public static void AutomaticActionSelection(Entity[] acters, Entity[] targets)
        {
            foreach (Entity Entity in acters)
            {
                Action.Actions action = (Action.Actions)Random.Shared.Next(Action.numberOfActions);
                Act(action, Entity, targets, true);
            }
        }

        public static int AutomaticTarget(Entity[] target)
        {
            return Random.Shared.Next(target.Length);
        }
        public static void Act(Action.Actions action, Entity acter, Entity[] targets, bool isAutomatic)
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
                        FightRenderer.InputErrer($"{temp + 1} is an Invalid target Attacking Entity will go idle \n");
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

        private static ConsoleKey ChaseHostile(Entity entity,Location closestHostile)
        {
            if (closestHostile.X != 0)
            {
                float xDistance = entity.Location.CalculateDistance(closestHostile.X, 0);
                float yDistance = entity.Location.CalculateDistance(0, closestHostile.Y);
                if (xDistance != 0 && (yDistance == 0 || xDistance < yDistance))
                {
                    if (entity.Location.X > closestHostile.X)
                    {
                        return ConsoleKey.LeftArrow;
                    }
                    else
                    {
                        return ConsoleKey.RightArrow;
                    }
                }
                else
                {
                    if (entity.Location.Y > closestHostile.Y)
                    {
                        return ConsoleKey.UpArrow;
                    }
                    else
                    {
                        return ConsoleKey.DownArrow;
                    }

                }
            }
                return RandomInput();
            }

        private static ConsoleKey GoblinMoveBehaviour(Entity entity, Location closestfriendly, Location closestHostile)
        {
            if (closestHostile.X != 0)
            {
                float distance = entity.Location.CalculateDistance(closestfriendly);
                float xDistance = entity.Location.CalculateDistance(closestHostile.X, 0);
                float yDistance = entity.Location.CalculateDistance(0, closestHostile.Y);
                if (xDistance != 0 && (yDistance == 0 || xDistance < yDistance))
                {
                    if (entity.Location.X > closestHostile.X)
                    {
                        if (distance > 0 && distance <= 3)
                            return ConsoleKey.LeftArrow;
                        else
                            return ConsoleKey.RightArrow;
                    }
                    else
                    {
                        if (distance > 0 && distance <= 3)
                            return ConsoleKey.RightArrow;
                        else
                            return ConsoleKey.LeftArrow;
                    }
                }
                else
                {
                    if (entity.Location.Y > closestHostile.Y)
                    {

                        if (distance > 0 && distance <= 3)
                            return ConsoleKey.UpArrow;
                        else
                            return ConsoleKey.DownArrow;
                    }
                    else
                    {
                        if (distance > 0 && distance <= 3)
                            return ConsoleKey.DownArrow;
                        else
                            return ConsoleKey.UpArrow;
                    }
                }

            }
            return RandomInput();
        }
    }
}
