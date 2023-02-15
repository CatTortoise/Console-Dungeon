//Basic C# for Games
//Dor Ben-Dor
//Final Project 
//Yshai flising

namespace Console_Dungeon
{
    class Shield
    {
        readonly int _maxShield = 100;
        readonly int _minShield = 1;

        private float _defense = 0;

        /// <summary>
        /// Create a shield with random Defense stats between 0 and 100  
        /// </summary>
        public Shield()
        {
            int def = Random.Shared.Next(_minShield, _maxShield);
            Defense = def;
        }
        /// <summary>
        /// Create a Shield with a Specified Defense stats 
        /// </summary>
        public Shield(int defense)
        {
            Defense = defense;
        }
        /// <summary>
        /// Create a shield with random Defense stats between min and max  
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public Shield(int min, int max)
        {
            Defense = Random.Shared.Next(min, max);
        }



        /// <summary>
        /// Shield works on percentage the value entered must be between 0 to 100 
        /// </summary>
        public float Defense
        {
            get => _defense;
            private set
            {
                if (value <= _maxShield && value >= _minShield)
                    _defense = value / 100;
                else
                    Console.WriteLine($"Shield value entered must be between {_minShield} to {_maxShield} ");
            }
        }

        public void Upgrad(int IncreaseAmount)
        {
            Defense += IncreaseAmount;
        }


    }
}
