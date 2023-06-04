//Basic C# for Games
//Dor Ben-Dor
//Final Project 
//Yshai flising
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
        private static Queue<Interruptible> _interruptiblesMapQueue = new Queue<Interruptible>(10);
        private static Queue<Envaironment> _envaironmentsMapQueue = new Queue<Envaironment>(10);

        private static Queue<Envaironment> _logeScreenQueue = new Queue<Envaironment>(10);
        private static Queue<Envaironment> _MenuScreenQueue = new Queue<Envaironment>(10);
        private static bool _printeMenu = false;
        private static bool _clearScreen = false;


        public static Location MaxScreenSize(Screen screen)
        {
            Location location = new(_screens[screen].LocationBottomRight.X - _screens[screen].LocationTopLeft.X,
                                    _screens[screen].LocationBottomRight.Y - _screens[screen].LocationTopLeft.Y);
            return location;
        }
        public static void SetScreens()
        {
            //Location _mapScreens;
            //Location _menuScreens;
            Console.SetWindowSize(Location.Xmax, Location.Ymax);
            Console.SetBufferSize(Location.Xmax, Location.Ymax);
            int screenDivaiderX = (int)Math.Ceiling(Location.Xmax * 0.2);
            int screenDivaiderY = (int)Math.Ceiling(Location.Ymax * 0.33);

            _screens.Add(Screen.Window, new("Window", Elements.Empty, new(), new(Location.Xmax, Location.Ymax)));

            _screens.Add(Screen.Log,
                new( 
                    "Log",
                    Elements.VisibleArea,
                    new(_screens[Screen.Window].LocationTopLeft.X ,_screens[Screen.Window].LocationTopLeft.Y),
                    new(screenDivaiderX, Location.Ymax)
                ));

            _screens.Add(Screen.Map,
                new("Map",
                    Elements.VisibleArea, 
                    new(_screens[Screen.Log].LocationBottomRight.X, _screens[Screen.Log].LocationTopLeft.Y),
                    new(_screens[Screen.Log].LocationBottomRight.X + screenDivaiderX * 3, Location.Ymax)
                    ));

            _screens.Add(Screen.Menu,
                new(
                    "Menu",
                    Elements.VisibleArea,
                    new(_screens[Screen.Map].LocationBottomRight.X, _screens[Screen.Map].LocationTopLeft.Y),
                    new(_screens[Screen.Map].LocationBottomRight.X + screenDivaiderX, Location.Ymax)
                    ));
            ScreensQueue();
        }


        #region Queue
        public static void ScreensQueue()
        {
            EnvaironmentQueue(_screens[Screen.Log], Screen.Window);
            EnvaironmentQueue(_screens[Screen.Map], Screen.Window);
            EnvaironmentQueue(_screens[Screen.Menu], Screen.Window);
        }
        public static void PrinteMenu()
        {
            _printeMenu = true;
        }

        public static bool InterruptibleQueue(Interruptible interruptible, Screen screen)
        {
            if (interruptible != null && !interruptible.IsHidden)
            {
                Interruptible InterruptibleForQueue;
                Location res = _screens[screen].LocationTopLeft;
                InterruptibleForQueue = new(
                    $"{interruptible.Name} for Queue",
                    interruptible.Id,
                    interruptible.ElementCode,
                    new(res.X + interruptible.Location.X, res.Y + interruptible.Location.Y),
                    false
                    );
                _interruptiblesMapQueue.Enqueue(InterruptibleForQueue);
                return true;
            }
            return false;
        }

        public static bool EntitiesQueue(Entity entity, Screen screen, bool myTurn)
        {
            if (entity != null && entity.IsAlive)
            {
                Entity EntityForQueue;
                Location res = _screens[screen].LocationTopLeft;
                EntityForQueue = new(
                    $"{entity.Name} for Queue",
                    false,
                    new(res.X + entity.Location.X, res.Y + entity.Location.Y),
                    myTurn? new(res.X + entity.Location.X, res.Y + entity.Location.Y) : new(res.X + entity.PreviousLocation.X, res.Y + entity.PreviousLocation.Y),
                    entity.ElementCode,
                    entity.Id,
                    myTurn
                    );
                _entitiesMapQueue.Enqueue(EntityForQueue);
                return true;
            }
            return false;
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
                    entity.Id,
                    false
                    ) ;
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

        public static void ClearScreen()
        {
            _clearScreen = true;
        }
        

        private static bool Erasure(Location location)
        {
            RenderElement(Elements.NonVisibleArea, location);
            return true;
        }

        private static void RenderEntity(Entity entity)
        {
            RenderElement(entity.ElementCode, new Location(entity.Location.X, entity.Location.Y), entity.MyTurn,false);
        }
        private static void RenderInterruptible(Interruptible interruptible)
        {
            RenderElement(interruptible.ElementCode, new Location(interruptible.Location.X, interruptible.Location.Y));
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
                Erasure(new Location(envaironment.LocationTopLeft.X, i));
                Erasure(new Location(envaironment.LocationBottomRight.X, i));
            }
            for (int i = envaironment.LocationTopLeft.X; i <= envaironment.LocationBottomRight.X; i++)
            {
                Erasure (new Location(i, envaironment.LocationTopLeft.Y));
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


        private static void RenderElement(Elements element, Location location,bool useSecondColor, bool useBackgroundColor)
        {
            ConsoleColor holdForegroundColor = Console.ForegroundColor;
            ConsoleColor holdBackgroundColor = Console.BackgroundColor;
            if (useSecondColor)
            {
                Console.ForegroundColor = ElementSecondColorDictionary[element];
            }
            else
            {
                Console.ForegroundColor = ElementFirstColorDictionary[element]; 
            }
            if (useBackgroundColor) 
            { 
                Console.BackgroundColor = ElementSecondColorDictionary[element];
            }
            RenderElement(element, location);
            Console.ForegroundColor = holdForegroundColor;
            Console.BackgroundColor = holdBackgroundColor;

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
            //Location locationStart;
            //Location locationEnd;
            Location ofsetLocation = _screens[Screen.Menu].LocationTopLeft;
            ofsetLocation.X++;
            ofsetLocation.Y++;
            //Menu.GetStarEndLocations(ofsetLocation, out locationStart, out locationEnd);
            foreach (string str in strs)
            {
                RenderStrin(str, ofsetLocation, Screen.Menu);
                ofsetLocation.Y++;
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
            if (_clearScreen)
            {
                Console.Clear();
                _clearScreen = false;
            }
            if (_printeMenu)
            {
                ErasureMenu();
                RenderMenu();
                _printeMenu = false;
            }

            foreach (Envaironment envaironment in _envaironmentsMapQueue)
            {
                ErasureEnvaironment(envaironment);
                RendererEnvaironment(envaironment);
            }
            _envaironmentsMapQueue.Clear();

            
            foreach (Interruptible interruptible in _interruptiblesMapQueue)
            {
                RenderInterruptible(interruptible);
            }
            _interruptiblesMapQueue.Clear();
            foreach (Entity entity in _entitiesMapQueue)
            {
                Erasure(entity.PreviousLocation);
                RenderEntity(entity);
            }
            _entitiesMapQueue.Clear();
    
        }

    }
}
