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
        public enum Screen
        {
            Window,
            Log,
            Map,
            Menu
        }
        private static Envaironment _logeLocation;
        private static Dictionary<Screen, Location> _screens = new Dictionary<Screen, Location>();
        private static Envaironment _menuLocation;
        private static Queue<Entity> _entitiesMapQueue = new Queue<Entity>(10);

        private static Queue<Envaironment> _envaironmentsMapQueue = new Queue<Envaironment>(10);
        private static Queue<Envaironment> _logeScreenQueue = new Queue<Envaironment>(10);
        private static Queue<Envaironment> _MenuScreenQueue = new Queue<Envaironment>(10);


        public static void SetScreens()
        {
            Location _mapScreens;
            Location _logeScreens;
            Location _menuScreens;
            Console.SetWindowSize(Location.Xmax, Location.Ymax);
            Console.SetBufferSize(Location.Xmax, Location.Ymax);
            int screenDivaider = (int)Math.Ceiling(Location.Xmax * 0.2);

            _logeScreens = new Location(screenDivaider, Location.Ymax);
            _screens.Add(Screen.Log, _logeScreens);
            _logeLocation = new("Log", Elements.DoorVertical,  new(_logeScreens.X, 0), _logeScreens);
            EnvaironmentQueue(_logeLocation, Screen.Window);

            _menuScreens = new Location(screenDivaider * 4, Location.Ymax);
            _screens.Add(Screen.Menu, _menuScreens);
            _menuLocation = new("Menu", Elements.DoorVertical, new(_menuScreens.X, 0), _menuScreens);
            EnvaironmentQueue(_menuLocation, Screen.Window);

            _mapScreens = new Location(Location.Xmax/2, Location.Ymax/2);
            _screens.Add(Screen.Map, _mapScreens);
        }
        

        #region Queue
        public static bool EntitiesQueue(Entity entity, Screen screen)
        {
            Entity EntityForQueue; 
            Location res;
            _screens.TryGetValue(screen, out res);
            EntityForQueue = new(
                $"{entity.Name} for Queue",
                new(res.X + entity.Location.X, res.Y + entity.Location.Y),
                new(res.X + entity.PreviousLocation.X, res.Y + entity.PreviousLocation.Y),
                entity.ElementCode, entity.Id
                );
            _entitiesMapQueue.Enqueue(EntityForQueue);
            return true;
        }
        public static void EnvaironmentQueue(Envaironment envaironment,Screen screen)
        {
            Envaironment envaironmentForQueue = new($"{envaironment.Name} for Queue", envaironment.ElementCode, envaironment.LocationTopLeft, envaironment.LocationBottomRight);
            Location res;
            _screens.TryGetValue(screen, out res);
            res.X += envaironment.LocationTopLeft.X;
            res.Y += envaironment.LocationTopLeft.Y;
            envaironmentForQueue.ChangeLocation(res);
            _envaironmentsMapQueue.Enqueue(envaironmentForQueue);
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
            for (int i = envaironment.LocationTopLeft.Y; i <= envaironment.LocationBottomRight.Y; i++)
            {
                Erasure(new Location(envaironment.LocationTopLeft.X,i));
                Erasure(new Location(envaironment.LocationBottomRight.X, i));
            }
            for (int i = envaironment.LocationTopLeft.X; i <= envaironment.LocationBottomRight.X; i++)
            {
                Erasure (new Location( i, envaironment.LocationTopLeft.Y));
                Erasure(new Location(i, envaironment.LocationBottomRight.Y));
            }

        }

        private static void RendererEnvaironment(Envaironment envaironment)
        {
            for (int i = envaironment.LocationTopLeft.Y; i <= envaironment.LocationBottomRight.Y; i++)
            {
                RenderElement(envaironment.ElementCode, new Location(envaironment.LocationTopLeft.X, i));
                RenderElement(envaironment.ElementCode, new Location(envaironment.LocationBottomRight.X, i));
            }
            for (int i = envaironment.LocationTopLeft.X; i <= envaironment.LocationBottomRight.X; i++)
            {
                RenderElement(envaironment.ElementCode,new Location(i, envaironment.LocationTopLeft.Y));
                RenderElement(envaironment.ElementCode,new Location(i, envaironment.LocationBottomRight.Y));
            }

        }


        private static void RenderElement(Elements element, Location location,ConsoleColor color)
        {
            ConsoleColor tempColor = Console.ForegroundColor;
        }
        private static void RenderElement(Elements element, Location location)
        {
            char str = 'A';
            Console.SetCursorPosition(location.X, location.Y);
            ElementDictionary.TryGetValue(element, out str);
            Console.Write(str);
        }


        public static void Render()
        {
            foreach (Envaironment envaironment in _envaironmentsMapQueue)
            {
                ErasureEnvaironment(envaironment);
                RendererEnvaironment(envaironment);
            }
            _envaironmentsMapQueue.Clear();
            foreach (Entity entity in _entitiesMapQueue)
            {
                Erasure(entity.PreviousLocation);
                RenderEntity(entity);
            }

            _entitiesMapQueue.Clear();
        }

    }
}
