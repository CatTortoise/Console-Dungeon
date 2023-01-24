using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    static class Generator
    {
        static Stack<int> id = new Stack<int>( new int[] { 1 }) ;
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
                GeneratLocation(element,map),
                element,
                Element.ElementForegroundColorDictionary[element] ,
                GeneratId(element)
                );
            return entities;
        }

        private static Location GeneratLocation(Element.Elements element , Map map)
        {
            Location location = new(Random.Shared.Next(1,map.MapSize.X), Random.Shared.Next(1,map.MapSize.Y));

            switch (element)
            {
                case Element.Elements.Player:
                    location = new(1,1);
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
    }
}
