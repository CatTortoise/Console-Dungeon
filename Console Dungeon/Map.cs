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
        private bool _isCleared;

        public Map()
        {
            GenerateMap();
            PopulateMap();
            GenerateCollisionsMap();
            LoadeAllMapElements();
        }

        public Entity[] mapEntities { get => _mapEntities; private set => _mapEntities = value; }
        public Location MapSize { get => _mapSize; private set => _mapSize = value; }

        private void PopulateMap()
        {
            _mapEntities = new Entity[] { new("P1",true, new Location(5,5), new Location(5, 5), Elements.Player,0),
                                            new("g1", false, new Location(7, 5), new Location(7, 5), Elements.Goblin, 1) };

        }

        public void MoveTo(Location location,Entity entity)
        {
            //Checks if love is possible
            if (!entity.Location.CompareLocations(location) && !CheckCollision(location))
            {
                _mapCollisions[entity.Location.X, entity.Location.Y] = ElementsTayp.Empty;
                mapEntities[entity.Id].MoveTo(location);
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


        private bool CheckCollision(Location location)
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
                    collisions = true;
                    ToTheDeath.Fight(_mapEntities);
                    LoadeAllMapElements();
                    break;
                case ElementsTayp.Interruptibles:
                    break;
            }
            return collisions;
        }

    }
}


