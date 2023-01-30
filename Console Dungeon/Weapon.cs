//Basic C# for Games
//Dor Ben-Dor
//Class 05 - Classes I
//Yshai flising
namespace Console_Dungeon
{
    class Weapon
    {
        
        private int _damage = 0;
        public int Damage { get => _damage; private set => _damage = value; }

        private float _accuracy = 0;
        public float Accuracy { get => _accuracy; private set => _accuracy = value; }

        /// <summary>
        /// Generator random weapon with values between 1 to 10 
        /// </summary>
        public Weapon(int min,int max)
        {
            Damage = Random.Shared.Next(min, max);
            Accuracy = Random.Shared.Next(min, max);
        }
        public Weapon(int damage, float accuracy)
        {
            Damage = damage;
            Accuracy = accuracy;
        }
        public void Upgrad(int IncreaseAmount)
        {
            Damage += IncreaseAmount;
            Accuracy += IncreaseAmount;
        }

    }
}

