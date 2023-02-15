//Basic C# for Games
//Dor Ben-Dor
//Final Project 
//Yshai flising
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    class Entity
    {
        public enum Equipment
        {
            Shield,
            Weapon
        }

        private string _name;
        private Element.Elements _ElementCode;
        private Location _previousLocation;
        private Location _location;
        private int _id;
        private bool _IsPlayer;
        private bool _isAlive = true;
        private bool _myTurn = false;
        private Shield _shield;
        private Weapon _weapon;

        private int _maxHP;
        private float _currentHP;
        private int _strength;
        private int _maxEnergy;
        private float _currentEnergy;
        private int _senses;
        private int _reactionSpeed;
        private int _toughness;
        private bool _isNotQueued;
        private bool _isShielding;

        private float _minEvasion = 0;
        private float _maxEvasion = 1;

        public float MinEvasion
        {
            get => _minEvasion;
            private set
            {
                if (value > MaxEvasion)
                {
                    Console.WriteLine($"The max Evasion value {MaxEvasion}");
                }
                else
                {
                    _minEvasion = value;
                }
            }
        }
        public float MaxEvasion
        {
            get => _maxEvasion;
            private set
            {
                if (value <= MinEvasion)
                {
                    Console.WriteLine($"The max Evasion value {MinEvasion}");
                }
                else
                {
                    _maxEvasion = value;
                }
            }
        }


        public float CurrentHP { get => _currentHP; private set => _currentHP = value; }


        private bool _isBusy;
        private Action.Actions _EntityAction;
        public Action.Actions EntityAction { get => _EntityAction; private set => _EntityAction = value; }

        public string GetEntityStats()
        {
            return $"{Name} HP: {CurrentHP:#,0.##}/{MaxHP:#,0.##}, Damage: {Weapon.Damage}, Accuracy: {Weapon.Accuracy},Defense: {Shield.Defense}, Evasion: {MinEvasion}/{MaxEvasion}";
        }


        //for Renderer
        public Entity(string name, bool isPlayer, Location location, Location previousLocation, Element.Elements elementCode, int id, bool myTurn)
        {
            Name = name;
            IsPlayer = isPlayer;
            IsAlive = true;
            Location = location;
            PreviousLocation = previousLocation;
            ElementCode = elementCode;
            Id = id;
            Weapon = Generator.GeneratWeapon(elementCode);
            Shield = new();
            MyTurn = myTurn;
        }
        //for Map
        public Entity(string name, bool isPlayer,int maxHP ,int strength,int energy, int reactionSpeed,int senses , int toughness,Location location, Element.Elements elementCode, ConsoleColor Color, int id)
        {
            Name = name;
            IsPlayer = isPlayer;
            IsAlive = true;
            Location = location;
            PreviousLocation = Location;
            ElementCode = elementCode;
            Id = id;
            _isShielding = false;
            _isBusy = false;
            EntityAction = Action.Actions.GoIdol;
            Senses = senses;
            Toughness = toughness;
            MaxHP = maxHP;
            _currentHP = MaxHP;
            Strength = strength;
            MaxEnergy = energy;
            CurrentEnergy = MaxEnergy;
            ReactionSpeed = reactionSpeed;
            CalculateEvasion();
            _isShielding = false;
            _isBusy = false;
            EntityAction = Action.Actions.GoIdol;
            _isNotQueued = false;
            Weapon = Generator.GeneratWeapon(elementCode);
            Shield = new();

        }
        public Entity(Entity entity)
        {
            Name = entity.Name;
            IsPlayer = entity.IsPlayer;
            IsAlive = true;
            Location = entity.Location;
            PreviousLocation = Location;
            ElementCode = entity.ElementCode;
            Id = entity.Id;
            _isShielding = false;
            _isBusy = false;
            EntityAction = Action.Actions.GoIdol;
            Senses = entity.Senses;
            Toughness = entity.Toughness;
            MaxHP = entity.MaxHP;
            _currentHP = MaxHP;
            Strength = entity.Strength;
            MaxEnergy = entity.MaxEnergy;
            CurrentEnergy = MaxEnergy;
            ReactionSpeed = entity.ReactionSpeed;
            CalculateEvasion();
            _isShielding = false;
            _isBusy = false;
            EntityAction = Action.Actions.GoIdol;
            _isNotQueued = false;
            Weapon = Generator.GeneratWeapon(entity.ElementCode);
            Shield = new();

        }
        private void CalculateEvasion()
        {
            MaxEvasion = ReactionSpeed;
            MinEvasion = MaxEvasion * (CurrentEnergy / MaxEnergy);

        }


        public int MaxHP { get => _maxHP; private set => _maxHP = value; }
        public string Name { get => _name; private set => _name = value; }
        public Location Location { get => _location; private set => _location = value; }
        public Location PreviousLocation { get => _previousLocation; private set => _previousLocation = value; }
        public Element.Elements ElementCode { get => _ElementCode; private set => _ElementCode = value; }
        public int Id { get => _id; set => _id = value; }
        public bool IsPlayer { get => _IsPlayer; private set => _IsPlayer = value; }
        public bool IsAlive { get => _isAlive; private set => _isAlive = value; }
        public int Strength { get => _strength; private set => _strength = value; }
        public int ReactionSpeed { get => _reactionSpeed; private set => _reactionSpeed = value; }
        public int MaxEnergy { get => _maxEnergy;private set => _maxEnergy = value; }
        public float CurrentEnergy { get => _currentEnergy; private set => _currentEnergy = value; }
        public int Senses { get => _senses; private set => _senses = value; }
        public int Toughness { get => _toughness; private set => _toughness = value; }
        public Shield Shield { get => _shield; private set => _shield = value; }
        public Weapon Weapon { get => _weapon; private set => _weapon = value; }
        public bool MyTurn { get => _myTurn; private set => _myTurn = value; }

        public void ActionsSelection(Action.Actions action)
        {
            if (!IsDead())
            {
                switch (action)
                {
                    case Action.Actions.DealDamage:
                        DealDamage(Action.Target);
                        break;
                    case Action.Actions.ShieldYourself:
                        ShieldYourself();
                        break;
                    case Action.Actions.Heal:
                        Heal(Action.Heal);
                        break;
                    case Action.Actions.UpgradEquipment:
                        UpgradEquipment(Action.IncreaseAmount, Action.Euipment);
                        break;
                    default:
                        EntityAction = Action.Actions.GoIdol;
                        return;
                }
                EntityAction = action;
            }

        }

        //3)
        #region Entity stats functions 
        /// <summary>
        /// damageDealt is reduced by shield percentage
        /// </summary>
        /// <param name="damageDealt"></param>
        /// <param name="attackerAccuracy"></param>
        private void TakeDamage(int damageDealt, float attackerAccuracy)
        {
            if (!IsDead())
            {
                float damagechans = MaxEvasion;
                if (attackerAccuracy > MinEvasion)
                {
                    damagechans = Random.Shared.Next((int)Math.Floor(MinEvasion), (int)Math.Ceiling(MaxEvasion));
                }
                if (attackerAccuracy > damagechans)
                {
                    if (_isShielding)
                    {
                        CurrentHP -= damageDealt - (damageDealt * (0.9f));
                    }
                    else
                    {
                        CurrentHP -= damageDealt - (damageDealt * Shield.Defense);
                    }
                }
            }
            IsDead();
        }

        private void DealDamage(Entity target)
        {
            target.TakeDamage(Weapon.Damage, Weapon.Accuracy);
        }

        private void ShieldYourself()
        {
            _isShielding = true;
        }

        /// <summary>
        /// Heal entity up in "heal" amount up to MaxHP
        /// </summary>
        /// <param name="heal"></param> 
        private void Heal(int heal)
        {
            if (!IsDead())
            {
                CurrentHP += heal;
                if (CurrentHP > MaxHP)
                {
                    CurrentHP = MaxHP;
                }
            }
        }

        /// <summary>
        /// checks if entities HP under 0
        /// </summary>
        /// <returns></returns>
        public bool IsDead()
        {
            
            if (CurrentHP <= 0)
            {
                EntityAction = Action.Actions.Dead;
                CurrentHP = 0;
                IsAlive = false;
                return !_isAlive;
            }
            else
            {
                return !_isAlive;
            }

        }
        #endregion

        private void EquipEquipment(Shield shield)
        {
            Shield = shield;
        }
        private void EquipEquipment(Weapon weapon)
        {
            Weapon = weapon;
        }
        /// <summary>
        /// Uses the enume Equipment to select the corresponding stats to increase
        /// </summary>
        /// <param name="IncreaseAmount">By how much to increase corresponding stats </param>
        /// <param name="equipment">Use the enum Equipment When calling this function </param>
        private void UpgradEquipment(int IncreaseAmount, Equipment equipment)
        {
            switch (equipment)
            {
                case Equipment.Shield:
                    Shield.Upgrad(IncreaseAmount);
                    break;
                case Equipment.Weapon:
                    Weapon.Upgrad(IncreaseAmount);
                    break;
            }
        }


        public void MoveTo(Location moveTo)
        {
            PreviousLocation = Location;
            Location = moveTo;
        }
        public void MoveTo(Location moveTo, Location moveFrom)
        {
            PreviousLocation = moveFrom;
            Location = moveTo;
        }

        public bool CollidedWithHostile(Element.Elements element)
        {
            return ElementCode != element;
        }
    } 
}

