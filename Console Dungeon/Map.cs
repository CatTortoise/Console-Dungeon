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
    class Map
    {
        private Location _mapSize = new Location(20,10);
        private ElementsTayp[,] _mapCollisions;
        private Envaironment _mapBorder;
        private Envaironment[] _mapEnvironments;
        private Entity[] _mapEntities;
        private Dictionary<Location, Interruptible> _mapInterruptibles;
        private Dictionary<Location, bool> _entityLocationsForFight;
        private Stack<Location> _ScanLocationsForFight;
        private bool _isCleared;


        public Entity[] MapEntities { get => _mapEntities; private set => _mapEntities = value; }
        public Location MapSize { get => _mapSize; private set => _mapSize = value; }
        public ElementsTayp[,] MapCollisions { get => _mapCollisions; private set => _mapCollisions = value; }
        public Envaironment[] MapEnvironments { get => _mapEnvironments; private set => _mapEnvironments = value; }
        public Envaironment MapBorder { get => _mapBorder; private set => _mapBorder = value; }
        public Dictionary<Location, Interruptible> MapInterruptibles { get => _mapInterruptibles; private set => _mapInterruptibles = value; }

        /// <summary>
        /// This map constructor Only to be used for the first map 
        /// </summary>
        /// <param name="minMapSize"></param>
        /// <param name="maxMapSize"></param>
        /// <param name="numberOfPlayer"></param>
        /// <param name="numberOfNpc"></param>
        public Map(int minMapSize, int maxMapSize, int numberOfPlayer, int numberOfNpc)
        {
            GenerateMap(minMapSize, maxMapSize);
            SpreadInterruptibles(MapEnvironments.Length);
            PopulateMap(numberOfPlayer, numberOfNpc);
            LoadeAllMapElements();
        }
        public Map(int minMapSize, int maxMapSize, Entity[] players, int numberOfNpc,int numberOfHobGoblins)
        {
            GenerateMap(minMapSize, maxMapSize);
            SpreadInterruptibles(MapEnvironments.Length);
            PopulateMap(players, numberOfNpc, numberOfHobGoblins);
            LoadeAllMapElements();
        }

        public void MoveTo(Location location, Entity entity)
        {
            if (location.X <= MapSize.X && location.Y <= MapSize.Y)
            {
                if (!entity.Location.CompareLocations(location) && !CheckCollision(location, entity))
                {
                    MapCollisions[entity.Location.X, entity.Location.Y] = ElementsTayp.Empty;
                    entity.MoveTo(location);
                    GenerateCollisionsEntityMap();
                }
                else
                {
                    entity.MoveTo(entity.Location);
                }
                Renderer.EntitiesQueue(entity, Renderer.Screen.Map);
            }
        }

        public void LoadeAllMapElements()
        {
            Renderer.EnvaironmentQueue(MapBorder, Renderer.Screen.Map);
            foreach (Envaironment envaironment in MapEnvironments)
            {
                Renderer.EnvaironmentQueue(envaironment, Renderer.Screen.Map);
            }
            foreach (Interruptible interruptible in MapInterruptibles.Values)
            {
                Renderer.InterruptibleQueue(interruptible, Renderer.Screen.Map);
            }
            foreach (Entity entities in MapEntities)
            {
                if (entities.IsAlive)
                {
                    Renderer.EntitiesQueue(entities, Renderer.Screen.Map);
                }
            }
        }

        private void PopulateMap(int numberOfPlayer,int numberOfNpc)
        {
            
            _mapEntities = new Entity[numberOfPlayer + numberOfNpc];
            for (int i = 0; i < numberOfPlayer; i++)
            {
                _mapEntities[i] = new Entity(Generator.GeneratEntity(Elements.Player, this));
                GenerateCollisionsEntityMap();
            }
            for (int i = numberOfPlayer; i < _mapEntities.Length; i++)
            {
                _mapEntities[i] = new Entity(Generator.GeneratEntity(Elements.Goblin, this));
                GenerateCollisionsEntityMap();
            }
            
        }
        private void PopulateMap(Entity[] players, int numberOfNpc, int numberOfHobGoblins)
        {
            if(numberOfNpc < numberOfHobGoblins)
            {
                numberOfHobGoblins = numberOfNpc;
            }
            _mapEntities = new Entity[players.Length + numberOfNpc];
            for (int i = 0; i < players.Length; i++)
            {
                _mapEntities[i] = Generator.RelocateEntity(players[i],this);
                GenerateCollisionsEntityMap();
            }
            for (int i = players.Length; i < _mapEntities.Length - numberOfHobGoblins; i++)
            {
                _mapEntities[i] = new Entity(Generator.GeneratEntity(Elements.Goblin, this));
                GenerateCollisionsEntityMap();
            }
            for(int i = _mapEntities.Length - numberOfHobGoblins; i < _mapEntities.Length; i++)
            {
                _mapEntities[i] = new Entity(Generator.GeneratEntity(Elements.Hob_Goblin, this));
                GenerateCollisionsEntityMap();
            }

        }

        private void GenerateMap(int minMapSize,int maxMapSize)
        {
            Location maxLocation = Renderer.MaxScreenSize(Renderer.Screen.Map);
            if (minMapSize < 0 || minMapSize > maxMapSize || maxMapSize > maxLocation.X || maxMapSize > Location.Ymax)
            {
                if (maxLocation.X >= maxLocation.Y)
                {
                    maxMapSize = maxLocation.X;
                }
                else
                {
                    maxMapSize = maxLocation.Y;
                }
                minMapSize = maxMapSize;
            }
            int numberOfEnvaironments;
            MapSize = new(maxMapSize, maxMapSize);
            MapBorder = new Envaironment(Generator.GeneratEnvaironment(Elements.Wall, minMapSize, maxMapSize, this));
            numberOfEnvaironments = (int)Math.Ceiling(MapBorder.LocationBottomRight.X / 6f);
            MapEnvironments = new Envaironment[numberOfEnvaironments];
            MapEnvironments[0] = MapBorder;
            MapSize = new(MapBorder.LocationBottomRight);
            MapCollisions = new ElementsTayp[MapSize.X+1, MapSize.Y+1] ;
            GenerateCollisionsEnvaironmentMap();
            for (int i = 1; i < numberOfEnvaironments; i++)
            {
                MapEnvironments[i] = Generator.GeneratEnvaironment(Elements.Wall, 3, 6,this);
                GenerateCollisionsEnvaironmentMap();
            }
        }

        private void SpreadInterruptibles(int numberOfInstructables)
        {
            MapInterruptibles = new(numberOfInstructables);
            foreach (Envaironment environment in MapEnvironments)
            {
                Interruptible interruptible = new(Generator.GeneratInterruptible(Elements.Door, Interruptible.ItemTayp.NotAnItem, this, environment));
                MapInterruptibles.Add(interruptible.Location, interruptible);
            }
            GenerateInterruptibleCollisions();
        }

        

        private void GenerateCollisionsEnvaironmentMap() 
        {
            foreach (Envaironment envaironment in MapEnvironments)
            {if (envaironment != null)
                {
                    for (int i = envaironment.LocationTopLeft.X; i <= envaironment.LocationBottomRight.X; i++)
                    {
                        MapCollisions[i, envaironment.LocationTopLeft.Y] = ElementsTayp.Environment;
                        MapCollisions[i, envaironment.LocationBottomRight.Y] = ElementsTayp.Environment;
                    }
                    for (int i = envaironment.LocationTopLeft.Y; i <= envaironment.LocationBottomRight.Y; i++)
                    {
                        MapCollisions[envaironment.LocationTopLeft.X, i] = ElementsTayp.Environment;
                        MapCollisions[envaironment.LocationBottomRight.X, i] = ElementsTayp.Environment;
                    }
                }
            }

        }
        private void LoadeAllInterruptibleMap()
        {
            foreach (Interruptible interruptible in MapInterruptibles.Values)
            {
                Renderer.InterruptibleQueue(interruptible, Renderer.Screen.Map);
            }
        }
        private void GenerateCollisionsEntityMap()
        { 
            foreach (Entity entity in MapEntities)
            {
                if (entity != null)
                {
                    if (entity.IsAlive == true)
                    {
                        MapCollisions[entity.Location.X, entity.Location.Y] = ElementsTayp.Entities;
                    }
                    else
                    {
                        MapCollisions[entity.Location.X, entity.Location.Y] = ElementsTayp.Empty;
                    }
                }
            }
        }
        private void GenerateInterruptibleCollisions()
        {
            foreach(Interruptible interruptible in MapInterruptibles.Values)
            {
                MapCollisions[interruptible.Location.X, interruptible.Location.Y] = ElementsTayp.Interruptibles;
            }
        }


        private bool CheckCollision(Location location,Entity entity)
        {

            bool collisions = false;
                switch (MapCollisions[location.X, location.Y])
                {
                    case ElementsTayp.Empty:
                        break;
                    case ElementsTayp.Environment:
                        collisions = true;
                        break;
                    case ElementsTayp.Entities:
                        foreach (Entity other in _mapEntities.Where<Entity>(checkEntity => checkEntity.Location.CompareLocations(location)))
                        {
                            collisions = true;
                            if (entity.CollidedWithHostile(other.ElementCode))
                            {
                                Entity[] entitysFortoTheDeath = EntitiesCollisions(location);
                                ToTheDeath.Fight(entitysFortoTheDeath);
                                GenerateCollisionsEntityMap();
                                LoadeAllMapElements();
                                Renderer.ScreensQueue();
                                break;
                            }
                        }
                        break;
                    case ElementsTayp.Interruptibles:
                        collisions = MapInterruptibles[location].IsBlocking;
                        MapInterruptibles[location].InteractWith(entity);
                        break;
                }
                GenerateInterruptibleCollisions();
                LoadeAllInterruptibleMap();
            return collisions;
        }


        private Entity[] EntitiesCollisions(Location StartingLocation)
        {
            _entityLocationsForFight = new Dictionary<Location, bool>();
            _ScanLocationsForFight = new Stack<Location>();
            List<Entity> entitys = new List<Entity>();
            GetEntitysLocationsForFight(StartingLocation);
            foreach(Entity entity in _mapEntities)
            {
                if (_entityLocationsForFight.ContainsKey(entity.Location))
                {
                    entitys.Add(entity);
                }
            }
            return entitys.ToArray();
        }

        /// <summary>
        /// Checks the tiles around a given location to see if it contains any entities. 
        /// The location that was used for Centre point get it's value set to true.
        /// locations That were not used for center point yet are set to false.
        /// </summary>
        /// <param name="location"></param>
        private void GetEntitysLocationsForFight(Location centerPoint )
        {
            Location scanLocation = new();
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    scanLocation.X = centerPoint.X + x;
                    scanLocation.Y = centerPoint.Y + y;
                    if (!_entityLocationsForFight.ContainsKey(scanLocation))
                    {
                        if (MapCollisions[scanLocation.X, scanLocation.Y] == ElementsTayp.Entities)
                        {
                            _entityLocationsForFight.Add(new(scanLocation), false);
                            _ScanLocationsForFight.Push(scanLocation);
                        } 
                    }
                }
            }
            if (_entityLocationsForFight.ContainsKey(centerPoint))
            {
                _entityLocationsForFight[centerPoint] = true;
            }
            CheckedIfAllLocationsWereScanned();
        }

        /// <summary>
        /// Checks if There are any unscanned locations 
        /// If there are any unscanned locations 
        /// They are send to be scanned 
        /// </summary>
        private void CheckedIfAllLocationsWereScanned()
        {
            Location location = new();
            if (_entityLocationsForFight.ContainsValue(false))
            {
                foreach (Location entityLocations in _entityLocationsForFight.Keys)
                {
                    if (!_entityLocationsForFight[entityLocations])
                    {
                        location = entityLocations;
                        break;
                    }
                }
                GetEntitysLocationsForFight(location);
            }
            
        }

    }
}


