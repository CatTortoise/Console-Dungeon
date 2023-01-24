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
        private Envaironment[] _mapEnvironments;
        private Entity[] _mapEntities;
        private Interruptible[] _mapInterruptibles;
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

        private void PopulateMap(int number)
        {
            _mapEntities = new Entity[number];
            for (int i = 0; i < 2; i++)
            {
                _mapEntities[i] = new Entity(Generator.GeneratEntity(Elements.Player, this));
            }
            for (int i = 2; i < number; i++)
            {
                _mapEntities[i] = new Entity(Generator.GeneratEntity(Elements.Goblin, this));
            }
            
        }

        public void MoveTo(Location location,Entity entity)
        {
            //Checks if love is possible
            if (!entity.Location.CompareLocations(location) && !CheckCollision(location, entity))
            {
                _mapCollisions[entity.Location.X, entity.Location.Y] = ElementsTayp.Empty;
                entity.MoveTo(location);
                Renderer.EntitiesQueue(entity, Renderer.Screen.Map);
                GenerateCollisionsMap();
            }
        }

     

        private void GenerateMap()
        {
            _mapCollisions = new ElementsTayp[MapSize.X+1, MapSize.Y+1] ;
            _mapEnvironments = new Envaironment[4];
            _mapEnvironments[0] = new("Border", Elements.Wall, new Location(0, 0), MapSize);
        }

        private void LoadeAllMapElements()
        {
            foreach(Envaironment envaironment in _mapEnvironments)
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
            foreach (Envaironment envaironment in _mapEnvironments)
            {if (envaironment != null)
                {
                    for (int i = envaironment.LocationTopLeft.X; i < envaironment.LocationBottomRight.X; i++)
                    {
                        _mapCollisions[i, envaironment.LocationTopLeft.Y] = ElementsTayp.Environment;
                        _mapCollisions[i, envaironment.LocationBottomRight.Y] = ElementsTayp.Environment;
                    }
                    for (int i = envaironment.LocationTopLeft.Y; i < envaironment.LocationBottomRight.Y; i++)
                    {
                        _mapCollisions[envaironment.LocationTopLeft.X, i] = ElementsTayp.Environment;
                        _mapCollisions[envaironment.LocationBottomRight.X, i] = ElementsTayp.Environment;
                    }
                }
            }
            foreach (Entity entity in mapEntities)
            {
                if (entity.IsAlive)
                {
                    _mapCollisions[entity.Location.X, entity.Location.Y] = ElementsTayp.Entities;
                }
            }

        }


        private bool CheckCollision(Location location,Entity entity)
        {

            bool collisions = false;
            switch (_mapCollisions[location.X, location.Y])
            {
                case ElementsTayp.Empty:
                    break;
                case ElementsTayp.Environment:
                    collisions = true;
                    break;
                case ElementsTayp.Entities:
                    
                    foreach (Entity other in _mapEntities.Where<Entity>(checkEntity => checkEntity.Location.CompareLocations(location)))
                    {
                        if (other.CollidedWithHostile(entity.ElementCode))
                        {
                            collisions = true;
                        }
                    }
                    if (collisions)
                    {
                        ToTheDeath.Fight(EntitiesCollisions(location));
                        LoadeAllMapElements();
                    }
                    break;
                case ElementsTayp.Interruptibles:
                    break;
            }
            return collisions;
        }

        private Entity[] EntitiesCollisions(Location location)
        {
            return _mapEntities.Where<Entity>(entiy => entiy.Location.CompareLocations(location)).ToArray();
        }
    }
}


