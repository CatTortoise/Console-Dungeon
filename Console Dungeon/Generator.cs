//Basic C# for Games
//Dor Ben-Dor
//Final Project 
//Yshai flising
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    static class Generator
    {
        #region Entity Generator
        static int id = 0;
        static public Entity GeneratEntity(Element.Elements element,Map map )
        {
            Entity entities = new
                (
                $"{element}",
                element == Element.Elements.Player,
                GeneratHP(element),
                GeneratStrength(element),
                GeneratEnergy(element),
                GeneratReactionSpeed(element),
                GeneratSenses(element),
                GeneratToughness(element),
                GeneratEntityLocation(element,map),
                element,
                Element.ElementFirstColorDictionary[element] ,
                GeneratId(element)
                );
            return entities;
        }
        static public Entity RelocateEntity(Entity entity, Map map)
        {
            Location location = GeneratEntityLocation(entity.ElementCode, map);
            entity.MoveTo(location, location);
            return entity;
        }

        private static Location GeneratEntityLocation(Element.Elements element , Map map)
        {

            Location location = new
                (
                Random.Shared.Next(
                    map.MapBorder.LocationTopLeft.X+1, map.MapBorder.LocationBottomRight.X), 
                Random.Shared.Next(
                    map.MapBorder.LocationTopLeft.Y + 1, map.MapBorder.LocationBottomRight.Y)
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

        #region Envaironment Generat 
        public static Envaironment GeneratEnvaironment(Element.Elements element, int minEnvaironmentSize, int maxEnvaironmentSize, Map map)
        {
            Location[] locations = GeneratEnvaironmentLocation(element, minEnvaironmentSize, maxEnvaironmentSize, map);
            Envaironment envaironment = new
                (
                $"{element} {GeneratId(element)}",
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
        private static Location[] GeneratEnvaironmentLocation(Element.Elements element, int minEnvaironmentSize, int maxEnvaironmentSize,  Map map)
        {
            Location topLeftLocation = new();
            Location bottomRightLocation = new();
            switch (element)
            {
                case Element.Elements.Wall:
                    do
                    {
                        if (map.MapBorder == null)
                        {
                            topLeftLocation = new(0, 0);
                        }
                        else
                        {
                            topLeftLocation = new(
                                                Random.Shared.Next(
                                                    map.MapBorder.LocationTopLeft.X, map.MapBorder.LocationBottomRight.X - minEnvaironmentSize),
                                                Random.Shared.Next(
                                                    map.MapBorder.LocationTopLeft.Y, map.MapBorder.LocationBottomRight.Y - minEnvaironmentSize)
                                                );
                        }
                        bottomRightLocation = new(
                                                Random.Shared.Next(topLeftLocation.X + minEnvaironmentSize, topLeftLocation.X + maxEnvaironmentSize),
                                                Random.Shared.Next(topLeftLocation.Y + minEnvaironmentSize, topLeftLocation.Y + maxEnvaironmentSize));
                    } while (!(bottomRightLocation.X <= map.MapSize.X &&
                                bottomRightLocation.Y <= map.MapSize.Y &&
                                ChecksIfLocationIsAvailable(topLeftLocation, bottomRightLocation, map)));
                    break;
            }
            return new Location[] { topLeftLocation , bottomRightLocation };
        }
        private static bool ChecksIfLocationIsAvailable(Location location , Map map)
        {
            if (map.MapBorder.LocationBottomRight.X >= location.X && map.MapBorder.LocationTopLeft.X <= location.X  &&
                map.MapBorder.LocationBottomRight.Y >= location.Y && map.MapBorder.LocationTopLeft.Y <= location.Y)
            {
                return map.MapCollisions[location.X, location.Y] == Element.ElementsTayp.Empty;
            }
            return false;
        }

        private static bool ChecksIfLocationIsAvailable(Location TopLeft, Location BottomRight, Map map)
        {
            bool IsAvailable = true;
            if (map.MapCollisions != null)
            {
                for (int i = TopLeft.X; i < BottomRight.X; i++)
                {
                    if (map.MapCollisions[i, TopLeft.Y] != Element.ElementsTayp.Empty ||
                        map.MapCollisions[i, BottomRight.Y] != Element.ElementsTayp.Empty)
                    {
                        return IsAvailable = false ;
                    }

                }
                for (int i = TopLeft.Y; i < BottomRight.Y; i++)
                {
                    if (map.MapCollisions[TopLeft.X, i] != Element.ElementsTayp.Empty ||
                        map.MapCollisions[TopLeft.X, i] != Element.ElementsTayp.Empty)
                    {
                        return IsAvailable = false; 
                    }
                }
            }
            return IsAvailable;
        }
        #endregion

        # region GeneratInterruptible
        public static Interruptible GeneratInterruptible(Element.Elements element, Interruptible.ItemTayp ItemTayp, Map map)
        {
            Interruptible interruptible = new(
               $"{element} {GeneratId(element)}",
                id,
                element,
                ItemTayp,
                GeneratInterruptibleLocation(element, map),
                MatchIsHidden(element),
                false,
                MatchIsBlocking(element)
               ) ;
            return interruptible;
        }

        /// <summary>
        /// generate interactable inside a wall 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="ItemTayp"></param>
        /// <param name="envaironment"></param>
        /// <returns></returns>
        public static Interruptible GeneratInterruptible(Element.Elements element, Interruptible.ItemTayp ItemTayp, Map map, Envaironment envaironment)
        {
            Location location = GeneratInterruptibleLocation(map, envaironment);
            Interruptible interruptible = new(
               $"{element} {GeneratId(element)}",
                id,
                MatchElements(element, envaironment, location),
                ItemTayp,
                location,
                MatchIsHidden(element),
                false,
                MatchIsBlocking(element)
               );
            return interruptible;
        }

        private static Element.Elements MatchElements(Element.Elements element , Envaironment envaironment,Location location )
        {
            switch (element)
            {
                case Element.Elements.Door:
                    if( envaironment.LocationTopLeft.X == location.X || envaironment.LocationBottomRight.X == location.X)
                    {
                        return Element.Elements.DoorHorizontal;
                    }
                    else
                    {
                        return Element.Elements.DoorVertical;
                    }
                default:
                    return element;
            }
        }

        private static bool MatchIsBlocking(Element.Elements element)
        {
            return true;
        }

        private static bool MatchIsHidden(Element.Elements element)
        {
            if(element == Element.Elements.TrapArmed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Location GeneratInterruptibleLocation(Element.Elements element, Map map)
        {
            return GeneratEntityLocation(element, map);
        }
        public static Location GeneratInterruptibleLocation(Map map, Envaironment envaironment)
        {
            int numberOfLocation = 4;
            Location[] passableLocations = new Location[numberOfLocation];
            List<Location> passableLocationsList = new List<Location>();

            //top wall
            passableLocations[0] = new(Random.Shared.Next(envaironment.LocationTopLeft.X + 1, envaironment.LocationBottomRight.X), envaironment.LocationTopLeft.Y);
            //Bottom wall
            passableLocations[1] = new(Random.Shared.Next(envaironment.LocationTopLeft.X + 1, envaironment.LocationBottomRight.X), envaironment.LocationBottomRight.Y);
            //Left wall
            passableLocations[2] = new(envaironment.LocationTopLeft.X, Random.Shared.Next(envaironment.LocationTopLeft.Y + 1, envaironment.LocationBottomRight.Y));
            //Right wall
            passableLocations[3] = new(envaironment.LocationBottomRight.X, Random.Shared.Next(envaironment.LocationTopLeft.Y + 1, envaironment.LocationBottomRight.Y));

            if (map.MapBorder.LocationBottomRight.CompareLocations(envaironment.LocationBottomRight))
            {
                //Right wall
                if (passableLocations[3].X == map.MapSize.X || ChecksIfLocationIsAvailable(new(-1, passableLocations[3].Y), map))
                    passableLocationsList.Add(passableLocations[3]);
                //Left wall
                if (passableLocations[2].X == 0 || ChecksIfLocationIsAvailable(new(passableLocations[2].X + 1, passableLocations[2].Y), map))
                    passableLocationsList.Add(passableLocations[2]);
                //Bottom wall
                if (passableLocations[1].Y == map.MapSize.Y || ChecksIfLocationIsAvailable(new(passableLocations[1].X, passableLocations[1].Y - 1), map))
                    passableLocationsList.Add(passableLocations[1]);
                //top wall
                if (passableLocations[2].Y == 0 || ChecksIfLocationIsAvailable(new(passableLocations[0].X, passableLocations[0].Y + 1), map))
                    passableLocationsList.Add(passableLocations[0]);
            }
            else
            {
                //Right wall
                if (passableLocations[3].X == map.MapSize.X || ChecksIfLocationIsAvailable(new(+1, passableLocations[3].Y), map))
                    passableLocationsList.Add(passableLocations[3]);
                //Left wall
                if (passableLocations[2].X == 0 || ChecksIfLocationIsAvailable(new(passableLocations[2].X - 1, passableLocations[2].Y), map))
                    passableLocationsList.Add(passableLocations[2]);
                //Bottom wall
                if (passableLocations[1].Y == map.MapSize.Y || ChecksIfLocationIsAvailable(new(passableLocations[1].X, passableLocations[1].Y + 1), map))
                    passableLocationsList.Add(passableLocations[1]);
                //top wall
                if (passableLocations[2].Y == 0 || ChecksIfLocationIsAvailable(new(passableLocations[0].X, passableLocations[0].Y - 1), map))
                    passableLocationsList.Add(passableLocations[0]);
            }
            


            
            if(passableLocationsList.Count == 0)
            {
                return GeneratInterruptibleLocation(map, envaironment);
            }
            return passableLocationsList[Random.Shared.Next(passableLocationsList.Count)];
        }

        static public Weapon GeneratWeapon(Element.Elements WhatGenerates)
        {
            int min = 0;
            int max = 1;
            switch (WhatGenerates)
            {
                case Element.Elements.Player:
                    min = 10;
                    max = 50;
                    break;
                case Element.Elements.Goblin:
                    min = 5;
                    max = 20;
                    break;
                case Element.Elements.Hob_Goblin:
                    min = 60;
                    max = 70;
                    break;
                case Element.Elements.Minatore:
                    min = 50;
                    max = 80;
                    break;
                case Element.Elements.Mimic:
                    min = 60;
                    max = 100;
                    break;
                default:
                    break;   
            }

            return new Weapon(min, max);
        }


        #endregion

        private static int GeneratId(Element.Elements element)
        { 
            return id++;
        }
        


    }
}
