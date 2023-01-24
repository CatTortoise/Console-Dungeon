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
        private static Dictionary<Screen, Envaironment> _screens = new Dictionary<Screen, Envaironment>();
        private static Envaironment _menuLocation;
        private static Queue<Entity> _entitiesMapQueue = new Queue<Entity>(10);

        private static Queue<Envaironment> _envaironmentsMapQueue = new Queue<Envaironment>(10);
        private static Queue<Envaironment> _logeScreenQueue = new Queue<Envaironment>(10);
        private static Queue<Envaironment> _MenuScreenQueue = new Queue<Envaironment>(10);
        private static bool _printeMenu = false;
       

        public static void SetScreens()
        {
            Location _mapScreens;
            Location _logeScreens;
            Location _menuScreens;
            Console.SetWindowSize(Location.Xmax, Location.Ymax);
            Console.SetBufferSize(Location.Xmax, Location.Ymax);
            int screenDivaider = (int)Math.Ceiling(Location.Xmax * 0.2);
            
            _screens.Add(Screen.Window, new("Window", Elements.Empty, new(), new(Location.Xmax, Location.Ymax)));

            _logeScreens = new Location(0, 0);
            _screens.Add(Screen.Log,new( "Log",Elements.DoorVertical,  new(screenDivaider, 0), new(screenDivaider, Location.Ymax)));
            EnvaironmentQueue(_screens[Screen.Log], Screen.Window);

            _menuScreens = new Location(screenDivaider * 4 + 1, 0);
            _screens.Add(Screen.Menu, new("Menu", Elements.DoorVertical, new(screenDivaider * 4, 0), new(screenDivaider*4, Location.Ymax)));
            EnvaironmentQueue(_screens[Screen.Menu], Screen.Window);

            _mapScreens = new Location(Location.Xmax/2, Location.Ymax/2);
            _screens.Add(Screen.Map, new("Map", Elements.Empty, new(_screens[Screen.Log].LocationBottomRight.X, 0), new(_screens[Screen.Menu].LocationTopLeft.X, Location.Ymax)));
            EnvaironmentQueue(_screens[Screen.Map], Screen.Window);
        }


        #region Queue
        public static void PrinteMenu()
        {
            _printeMenu = true;
        }
        public static bool EntitiesQueue(Entity entity, Screen screen)
        {
            if (entity != null && entity.IsAlive)
            {
                Entity EntityForQueue;
                Location res = _screens[screen].LocationTopLeft;
                EntityForQueue = new(
                    $"{entity.Name} for Queue",
                    false,
                    new(res.X + entity.Location.X, res.Y + entity.Location.Y),
                    new(res.X + entity.PreviousLocation.X, res.Y + entity.PreviousLocation.Y),
                    entity.ElementCode,
                    entity.Id
                    );
                _entitiesMapQueue.Enqueue(EntityForQueue);
                return true;
            }
            return false;
        }
        public static void EnvaironmentQueue(Envaironment envaironment,Screen screen)
        {
            if (envaironment != null)
            {
                Envaironment envaironmentForQueue = new($"{envaironment.Name} for Queue", envaironment.ElementCode, envaironment.LocationTopLeft, envaironment.LocationBottomRight);
                Location res = _screens[screen].LocationTopLeft;
                res.X += envaironment.LocationTopLeft.X;
                res.Y += envaironment.LocationTopLeft.Y;
                envaironmentForQueue.ChangeLocation(res);
                _envaironmentsMapQueue.Enqueue(envaironmentForQueue);
            }
        }
        #endregion 

        
        public static bool Erasure(Location location)
        {
            RenderElement(Elements.NonVisibleArea, location);
            return true;
        }

        private static void RenderEntity(Entity entity)
        {
            RenderElement(entity.ElementCode, new Location(entity.Location.X, entity.Location.Y));
        }

        private static void ErasureMenu()
        {
            Location locationStart;
            Location locationEnd;
            Menu.GetStarEndLocations(out locationStart, out locationEnd);

            for (int i = locationStart.Y; i <= locationEnd.Y; i++)
            {
                Erasure(new Location(locationStart.X, i));
                Erasure(new Location(locationEnd.X, i));
            }
            for (int i = locationStart.X; i <= locationEnd.X; i++)
            {
                Erasure(new Location(i, locationStart.Y));
                Erasure(new Location(i, locationEnd.Y));
            }

        }

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
        private static void RenderMenu()
        {
            string[] strs = Menu.Menus[Menu.CurentMenuType];
            Location locationStart;
            Location locationEnd;
            Menu.GetStarEndLocations(_screens[Screen.Menu].LocationTopLeft, out locationStart, out locationEnd);
            foreach (string str in strs)
            {
                RenderStrin(str, locationStart,Screen.Menu);
                locationStart.Y++;
            }
            
        }
        private static void RenderStrin(string str, Location location, Screen screen)
        {
            Console.SetCursorPosition(location.X, location.Y);
            string WriteStr = str;
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

            if (_printeMenu)
            {
                ErasureMenu();
                RenderMenu();
                _printeMenu = false;
            }

            foreach (Entity entity in _entitiesMapQueue)
            {
                Erasure(entity.PreviousLocation);
                RenderEntity(entity);
            }
            _entitiesMapQueue.Clear();
        }

    }
}
