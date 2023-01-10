using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Console_Dungeon.Element;

namespace Console_Dungeon
{
    static class Renderer
    {
        private static Map _map;
        private static Queue<Entity> _entities = new Queue<Entity>(10);
        private static Queue<Envaironment> _envaironments = new Queue<Envaironment>(10);
        private static int _windowHeight;
        private static int _windowWidth; 


        public static void LaodMap(Map newMap)
        {
            _map = newMap;
        }
        #region Queue
        public static bool EntitiesQueue(Entity entity)
        {
            _entities.Enqueue(entity);
            return true;
        }
        public static void EnvaironmentQueue(Envaironment envaironment)
        {
            _envaironments.Enqueue(envaironment);
        }
        #endregion 

        #region IElement
        public static bool Erasure(Location location)
        {
            RenderElement(Elements.NonVisibleArea, location);
            return true;
        }

        private static void RenderEntity(Entity entity)
        {
            RenderElement(entity.ElementCode, new Location(entity.Location.X, entity.Location.Y));
        }

        #endregion
        private static void ErasureEnvaironment(Envaironment envaironment)
        {
            for (int i = envaironment.LocationTopLeft.X; i < envaironment.LocationBottomRight.X; i++)
            {
                Erasure (new Location( i, envaironment.LocationTopLeft.Y));
                Erasure(new Location(i, envaironment.LocationBottomRight.Y));
            }
            for (int i = envaironment.LocationTopLeft.Y; i < envaironment.LocationBottomRight.Y; i++)
            {
                Erasure(new Location(envaironment.LocationTopLeft.X));
                Erasure(new Location(envaironment.LocationBottomRight.X, i));
            }
        }

        private static void RendererEnvaironment(Envaironment envaironment)
        {
            for (int i = envaironment.LocationTopLeft.X; i < envaironment.LocationBottomRight.X; i++)
            {
                RenderElement(envaironment.ElementCode,new Location(i, envaironment.LocationTopLeft.Y));
                RenderElement(envaironment.ElementCode,new Location(i, envaironment.LocationBottomRight.Y));
            }
            for (int i = envaironment.LocationTopLeft.Y; i < envaironment.LocationBottomRight.Y; i++)
            {
                RenderElement(envaironment.ElementCode,new Location(envaironment.LocationTopLeft.X, i));
                RenderElement(envaironment.ElementCode,new Location(envaironment.LocationBottomRight.X, i));
            }
        }


        private static void RenderElement(Elements element, Location location,ConsoleColor color)
        {
            ConsoleColor tempColor = Console.ForegroundColor;
        }
        private static void RenderElement(Elements element, Location location)
        {
            char str;
            Console.SetCursorPosition(location.X, location.Y);
            ElementDictionary.TryGetValue(element, out str);
            Console.Write(str);
        }


        public static void Render()
        {
            foreach (Envaironment envaironment in _envaironments)
            {
                ErasureEnvaironment(envaironment);
                RendererEnvaironment(envaironment);
            }
            _envaironments.Clear();
            foreach (Entity entity in _entities)
            {
                Erasure(entity.PreviousLocation);
                RenderEntity(entity);
            }

            _entities.Clear();
        }

    }
}
