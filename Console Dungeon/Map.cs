﻿using System;
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
        private Envaironment[] _mapEnvironments;
        private Entity[] _mapEntities;
        private Interruptible[] _mapInterruptibles;
        Dictionary<Location, bool> _entityLocationsForFight;
        Stack<Location> _ScanLocationsForFight;
        private bool _isCleared;

        public Map()
        {
            GenerateMap();
            PopulateMap(10);
            GenerateCollisionsMap();
            LoadeAllMapElements();
        }

        public Entity[] mapEntities { get => _mapEntities; private set => _mapEntities = value; }
        public Location MapSize { get => _mapSize; private set => _mapSize = value; }
        internal ElementsTayp[,] MapCollisions { get => _mapCollisions; private set => _mapCollisions = value; }
        internal Envaironment[] MapEnvironments { get => _mapEnvironments; private set => _mapEnvironments = value; }

        private void PopulateMap(int number)
        {
            _mapEntities = new Entity[number];
            for (int i = 0; i < 2; i++)
            {
                _mapEntities[i] = new Entity(Generator.GeneratEntity(Elements.Player, this));
                MapCollisions[_mapEntities[i].Location.X, _mapEntities[i].Location.Y] = ElementsTayp.Entities;
            }
            for (int i = 2; i < number; i++)
            {
                _mapEntities[i] = new Entity(Generator.GeneratEntity(Elements.Goblin, this));
                MapCollisions[_mapEntities[i].Location.X, _mapEntities[i].Location.Y] = ElementsTayp.Entities;
            }
            
        }

        public void MoveTo(Location location,Entity entity)
        {
            //Checks if love is possible
            if (!entity.Location.CompareLocations(location) && !CheckCollision(location, entity))
            {
                MapCollisions[entity.Location.X, entity.Location.Y] = ElementsTayp.Empty;
                entity.MoveTo(location);
                Renderer.EntitiesQueue(entity, Renderer.Screen.Map);
                GenerateCollisionsMap();
            }
        }

     

        private void GenerateMap()
        {
            MapSize = new(20, 50);
            MapEnvironments = new Envaironment[4];
            MapEnvironments[0] = new Envaironment(Generator.GeneratEnvaironment(Elements.Wall, new(MapSize.X, MapSize.X), new(MapSize.Y, MapSize.Y), this));
            MapSize= new(MapEnvironments[0].LocationBottomRight);
            MapCollisions = new ElementsTayp[MapSize.X+1, MapSize.Y+1] ;
            // MapEnvironments[0] = new("Border", Elements.Wall, new Location(0, 0), MapSize);
            for (int i = 1; i < 4; i++)
            {
                MapEnvironments[i] = Generator.GeneratEnvaironment(Elements.Wall, new(5,5), new(3,3),this);
            }
        }

        private void LoadeAllMapElements()
        {
            foreach(Envaironment envaironment in MapEnvironments)
            {
                Renderer.EnvaironmentQueue(envaironment, Renderer.Screen.Map);
            }
            foreach (Entity entities in mapEntities)
            {
                if (entities.IsAlive)
                {
                    Renderer.EntitiesQueue(entities, Renderer.Screen.Map);
                }
            }
        }

        private void GenerateCollisionsMap() 
        {
            foreach (Envaironment envaironment in MapEnvironments)
            {if (envaironment != null)
                {
                    for (int i = envaironment.LocationTopLeft.X; i < envaironment.LocationBottomRight.X; i++)
                    {
                        MapCollisions[i, envaironment.LocationTopLeft.Y] = ElementsTayp.Environment;
                        MapCollisions[i, envaironment.LocationBottomRight.Y] = ElementsTayp.Environment;
                    }
                    for (int i = envaironment.LocationTopLeft.Y; i < envaironment.LocationBottomRight.Y; i++)
                    {
                        MapCollisions[envaironment.LocationTopLeft.X, i] = ElementsTayp.Environment;
                        MapCollisions[envaironment.LocationBottomRight.X, i] = ElementsTayp.Environment;
                    }
                }
            }
            foreach (Entity entity in mapEntities)
            {
                if (entity.IsAlive)
                {
                    MapCollisions[entity.Location.X, entity.Location.Y] = ElementsTayp.Entities;
                }
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
                            ToTheDeath.Fight(EntitiesCollisions(location));
                            GenerateCollisionsMap();
                            LoadeAllMapElements();
                        }
                    }
                    break;
                case ElementsTayp.Interruptibles:
                    break;
            }
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


