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
                    return RandomInput();
                case Element.Elements.Goblin:
                    return LookAround( entity,  map);
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
                        if (mapEntity.Location.CompareLocations(new(x, y)))
                        {
                            if (mapEntity.CollidedWithHostile(entity.ElementCode))
                            {
                                float hostileDistance = mapEntity.Location.CalculateDistance(entity.Location);
                                if (closestHostile.X  == 0 ||  hostileDistance < mapEntity.Location.CalculateDistance(entity.Location)) 
                                {
                                    closestHostile = new(x, y);
                                }                                
                            }
                            else if (entity != null)
                            {
                                float friendlyDistance = closestHostile.CalculateDistance(entity.Location);
                                if (closestFriendly.X == 0 || friendlyDistance < mapEntity.Location.CalculateDistance(entity.Location))
                                {
                                    closestFriendly = new(x, y);        
                                }
                                    
                            }
                        }
                        
                    }
                    
                }
            }
            return PostmateBehaviour(entity, closestFriendly, closestHostile);
        }
        public static ConsoleKey PostmateBehaviour(Entity entity, Location closestfriendly ,Location closestHostile)
        {
            switch (entity.ElementCode)
            {
                case Element.Elements.Player:
                    return RandomInput();
                case Element.Elements.Goblin:
                    if (closestHostile.X != 0)
                    {
                    float distance = entity.Location.CalculateDistance(closestfriendly);
                    Debug.WriteLineIf(distance > 0, $"{entity.Location.X}, {entity.Location.Y}: {distance}");
                    Debug.WriteLineIf(distance > 0, $"{distance > 0 && distance <= 3} {closestfriendly.X},{closestfriendly.Y}");

                        if (distance > 0 && distance <= 3 )
                        {
                            distance = entity.Location.CalculateDistance(closestHostile.X,0);
                            if (distance != 0 && distance < entity.Location.CalculateDistance(0,closestHostile.Y))
                            {
                                if(entity.Location.X > closestHostile.X)
                                {
                                    return ConsoleKey.RightArrow;
                                }
                                else 
                                {
                                    return ConsoleKey.LeftArrow;
                                }
                            }
                            else
                            {
                                if (entity.Location.Y > closestHostile.Y)
                                {
                                    return ConsoleKey.DownArrow;
                                }
                                else 
                                {
                                    return ConsoleKey.UpArrow;
                                }
                            }  
                        }
                        else
                        {
                            float xDistance = entity.Location.CalculateDistance(closestHostile.X, 0);
                            float yDistance = entity.Location.CalculateDistance(0,closestHostile.Y);
                            if (yDistance != 0 && xDistance < yDistance)
                            {
                                if (entity.Location.X > closestHostile.X)
                                {
                                    return ConsoleKey.RightArrow;
                                }
                                else
                                {
                                    return ConsoleKey.LeftArrow;
                                }
                            }
                            else
                            {
                                if (entity.Location.Y > closestHostile.Y)
                                {
                                    return ConsoleKey.DownArrow;
                                }
                                else
                                {
                                    return ConsoleKey.UpArrow;
                                }
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
    }
}
