using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    static class Generator
    {
        static Stack<int> id = new(2);
        static public Entity GeneratEntity(Element.Elements element,Location location )
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
                Generattoughness(element),
                location,
                element,
                Element.ElementForegroundColorDictionary[element] ,
                GeneratId(element)
                );
            return entities;
        }

        private static int GeneratId(Element.Elements element)
        {
            id.Push(id.Peek()+1);
            return id.Peek();
        }

        private static int GeneratSenses(Element.Elements element)
        {
            throw new NotImplementedException();
        }

        private static int Generattoughness(Element.Elements element)
        {
            throw new NotImplementedException();
        }

        private static int GeneratReactionSpeed(Element.Elements element)
        {
            throw new NotImplementedException();
        }

        private static int GeneratEnergy(Element.Elements element)
        {
            throw new NotImplementedException();
        }

        private static int GeneratStrength(Element.Elements element)
        {
            throw new NotImplementedException();
        }

        private static int GeneratHP(Element.Elements element)
        {
            throw new NotImplementedException();
        }
    }
}
