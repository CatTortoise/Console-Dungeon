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
        private Elements[,] _mapElements;
        private Envaironment[] _mapEnvironments;
        private Entity[] _mapEntities;
        private Interruptible[] _mapInterruptibles;
        private bool _isCleared;

        public Map()
        {
            generateMap();
            populateMap();
        }

        public Entity[] mapEntities { get => _mapEntities; set => _mapEntities = value; }



        private void populateMap()
        {
            _mapEntities = new Entity[1]{ new("P1",new Location(5,5),Elements.Player,0) };
        }

        public void MoveTo(Location location,Entity entity)
        {
            //Checks if love is possible

            mapEntities[entity.Id].MoveTo(location);
        }
        private void generateMap()
        {
            _mapElements = new Elements[_mapSize.X, _mapSize.Y];
            _mapEnvironments = new Envaironment[4];
            _mapEnvironments[0] = new("Border", Elements.Wall, new Location(0, 0), _mapSize);
        }
        private void CheckCollision()

    }
}


