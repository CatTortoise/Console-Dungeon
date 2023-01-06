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

        private int _mapHeight = 10;
        private int _mapWidth = 10;
        private Envaironment[] _mapEnvironments;
        private Entity[] _mapEntities;
        private Interruptible[] _mapInterruptibles;
        private bool _isCleared;

        public Entity[] mapEntities { get => _mapEntities; set => _mapEntities = value; }

        public void populateMap()
        {
            _mapEntities = new Entity[1]{ new("P1",new Location(5,5),Elements.Player,0) };
        }

        public void MoveTo(Location location,Entity entity)
        {
            //Checks if love is possible
            mapEntities[entity.Id].MoveTo(location); 
        }


    }
}


