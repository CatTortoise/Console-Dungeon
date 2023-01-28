﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    static class Generator
    {
        #region Entity Generator
        static Stack<int> id = new Stack<int>( new int[] { 1 }) ;
        static public Entity GeneratEntity(Element.Elements element,Map map )
        {
            Entity entities = new
                (
                $"{element} {id}",
                element == Element.Elements.Player,
                GeneratHP(element),
                GeneratStrength(element),
                GeneratEnergy(element),
                GeneratReactionSpeed(element),
                GeneratSenses(element),
                GeneratToughness(element),
                GeneratEntityLocation(element,map),
                element,
                Element.ElementForegroundColorDictionary[element] ,
                GeneratId(element)
                );
            return entities;
        }

        private static Location GeneratEntityLocation(Element.Elements element , Map map)
        {

            Location location = new
                (
                Random.Shared.Next(
                    map.MapEnvironments[0].LocationTopLeft.X+1, map.MapEnvironments[0].LocationBottomRight.X), 
                Random.Shared.Next(
                    map.MapEnvironments[0].LocationTopLeft.Y + 1, map.MapEnvironments[0].LocationBottomRight.Y)
                );
            //Location location = new(Random.Shared.Next(1, map.MapSize.X), Random.Shared.Next(1, map.MapSize.Y));

            switch (element)
            {
                case Element.Elements.Player:
                    location = new(1, 1);
                    break;
                case Element.Elements.Goblin:
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
            bool FoundAvailableLocation = ChecksIfLocationIsAvailable(location, map);
            while (!FoundAvailableLocation)
            {
                for (int y = 0; y < map.MapSize.Y; y++)
                {
                    for (int x = 0; x < map.MapSize.X; x++)
                    {
                        FoundAvailableLocation = ChecksIfLocationIsAvailable(new(location.X + x, location.Y + y), map);
                        if (FoundAvailableLocation)
                        {
                            location.X += x;
                            location.Y += y;
                            return location;
                        }
                    }
                }
                for (int y = 0; map.MapSize.Y-y > 0; y--)
                {
                    for (int x = 0; map.MapSize.X-x > 0; x--)
                    {
                        FoundAvailableLocation = ChecksIfLocationIsAvailable(new(location.X + x, location.Y + y), map);
                        if (FoundAvailableLocation)
                        {
                            location.X += x;
                            location.Y += y;
                            return location;
                        }
                    }
                }
            }
            return location;
        }

        private static int GeneratId(Element.Elements element)
        {
            id.Push(id.Peek()+1);
            return id.Peek();
        }

        private static int GeneratSenses(Element.Elements element)
        {
            int min = 0;
            int max = 1;
            switch (element)
            {
                case Element.Elements.Player:
                    min = 5;
                    max = 10;
                    break ;
                case Element.Elements.Goblin:
                    min = 5;
                    max = 10;
                    break ;
                case Element.Elements.Hob_Goblin:
                    min = 5;
                    max = 15;
                    break ;
                case Element.Elements.Minatore:
                    min = 0;
                    max = 1;
                    break ;
                case Element.Elements.Mimic:
                    min = 0;
                    max = 1;
                    break ;
                default: 
                    break ;
            }
            return Random.Shared.Next(min,max);
        }

        private static int GeneratToughness(Element.Elements element)
        {
            int min = 0;
            int max = 1;
            switch (element)
            {
                case Element.Elements.Player:
                    min = 5;
                    max = 10;
                    break;
                case Element.Elements.Goblin:
                    min = 1;
                    max = 5;
                    break;
                case Element.Elements.Hob_Goblin:
                    min = 5;
                    max = 15;
                    break;
                case Element.Elements.Minatore:
                    min = 0;
                    max = 1;
                    break;
                case Element.Elements.Mimic:
                    min = 0;
                    max = 1;
                    break;
                default:
                    break;
            }
            return Random.Shared.Next(min, max);
        }

        private static int GeneratReactionSpeed(Element.Elements element)
        {
            int min = 0;
            int max = 1;
            switch (element)
            {
                case Element.Elements.Player:
                    min = 10;
                    max = 20;
                    break;
                case Element.Elements.Goblin:
                    min = 10;
                    max = 20;
                    break;
                case Element.Elements.Hob_Goblin:
                    min = 10;
                    max = 25;
                    break;
                case Element.Elements.Minatore:
                    min = 0;
                    max = 1;
                    break;
                case Element.Elements.Mimic:
                    min = 0;
                    max = 1;
                    break;
                default:
                    break;
            }
            return Random.Shared.Next(min, max);
        }

        private static int GeneratEnergy(Element.Elements element)
        {
            int min = 0;
            int max = 1;
            switch (element)
            {
                case Element.Elements.Player:
                    min = 15;
                    max = 20;
                    break;
                case Element.Elements.Goblin:
                    min = 5;
                    max = 10;
                    break;
                case Element.Elements.Hob_Goblin:
                    min = 15;
                    max = 25;
                    break;
                case Element.Elements.Minatore:
                    min = 0;
                    max = 1;
                    break;
                case Element.Elements.Mimic:
                    min = 0;
                    max = 1;
                    break;
                default:
                    break;
            }
            return Random.Shared.Next(min, max);
        }

        private static int GeneratStrength(Element.Elements element)
        {
            int min = 0;
            int max = 1;
            switch (element)
            {
                case Element.Elements.Player:
                    min = 5;
                    max = 10;
                    break;
                case Element.Elements.Goblin:
                    min = 1;
                    max = 5;
                    break;
                case Element.Elements.Hob_Goblin:
                    min = 5;
                    max = 15;
                    break;
                case Element.Elements.Minatore:
                    min = 0;
                    max = 1;
                    break;
                case Element.Elements.Mimic:
                    min = 0;
                    max = 1;
                    break;
                default:
                    break;
            }
            return Random.Shared.Next(min, max);
        }

        private static int GeneratHP(Element.Elements element)
        {
            int min = 0;
            int max = 1;
            switch (element)
            {
                case Element.Elements.Player:
                    min = 100;
                    max = 150;
                    break;
                case Element.Elements.Goblin:
                    min = 30;
                    max = 60;
                    break;
                case Element.Elements.Hob_Goblin:
                    min = 60;
                    max = 70;
                    break;
                case Element.Elements.Minatore:
                    min = 0;
                    max = 1;
                    break;
                case Element.Elements.Mimic:
                    min = 0;
                    max = 1;
                    break;
                default:
                    break;
            }
            return Random.Shared.Next(min, max);
        }
        #endregion

        public static Envaironment GeneratEnvaironment(Element.Elements element, Location maxEnvaironmentSize, Location minEnvaironmentSize, Map map)
        {
            Location[] locations = GeneratEnvaironmentLocation(element, maxEnvaironmentSize, minEnvaironmentSize, map);
            Envaironment envaironment = new
                (
                $"{element} {id}",
                element,
                locations[0],
                locations[1]
                );
            return envaironment;
        }


        /// <summary>
        /// The function returns a two parameter Location array where the first "0" value is there top left location
        /// and there second value "1" is the bottom right location 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="maxEnvaironmentSize"></param>
        /// <param name="minEnvaironmentSize"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private static Location[] GeneratEnvaironmentLocation(Element.Elements element, Location minEnvaironmentSize, Location maxEnvaironmentSize,  Map map)
        {
            Location topLeftLocation = new();
            Location bottomRightLocation = new();
            switch (element)
            {
                case Element.Elements.Wall:
                    do
                    {
                        if (map.MapEnvironments[0] == null)
                        {
                            topLeftLocation = new(Random.Shared.Next(0, map.MapSize.X - minEnvaironmentSize.X), Random.Shared.Next(0, map.MapSize.Y - minEnvaironmentSize.Y));
                            bottomRightLocation = new(Random.Shared.Next(topLeftLocation.X, topLeftLocation.X + maxEnvaironmentSize.X), Random.Shared.Next(topLeftLocation.Y, topLeftLocation.Y + maxEnvaironmentSize.Y));
                        }
                        else
                        {
                            topLeftLocation = new(
                                                Random.Shared.Next(
                                                    map.MapEnvironments[0].LocationTopLeft.X, map.MapEnvironments[0].LocationBottomRight.X - minEnvaironmentSize.X),
                                                Random.Shared.Next(
                                                    map.MapEnvironments[0].LocationTopLeft.Y + 1, map.MapEnvironments[0].LocationBottomRight.Y - minEnvaironmentSize.Y)
                                                );
                            bottomRightLocation = new(
                                                Random.Shared.Next(topLeftLocation.X, topLeftLocation.X + maxEnvaironmentSize.X), 
                                                Random.Shared.Next(topLeftLocation.Y, topLeftLocation.Y + maxEnvaironmentSize.Y));
                        }
                    } while (!(bottomRightLocation.X <= map.MapSize.X && bottomRightLocation.Y <= map.MapSize.Y));
                    break;
            }


            return new Location[] { topLeftLocation , bottomRightLocation };
        }
        private static bool ChecksIfLocationIsAvailable(Location location , Map map)
        {
            return map.MapCollisions[location.X,location.Y] == Element.ElementsTayp.Empty;
        }
    }
}
