using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        private string _name;
        protected Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
            Name = name;
            BaseHealth = health;
            BaseArmor = armor;
            AbilityPoints = abilityPoints;
            Bag = bag;

            Health = BaseHealth;
            Armor = BaseArmor;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);
                }

                _name = value;
            }
        }

        public double BaseHealth { get; private set; }

        public double Health { get; protected internal set; }

        public double BaseArmor { get; private set; }

        public double Armor { get; private set; }

        public double AbilityPoints { get; private set; }
        public Bag Bag { get; private set; }

        public void TakeDamage(double hitPoints)
        {
            EnsureAlive();
            if (Armor >= hitPoints)
            {
                Armor -= hitPoints;
            }
            else
            {
                Armor -= hitPoints;
                Health -= Math.Abs(Armor);

                Armor = 0;

                if (Health <= 0)
                {
                    Health = 0;

                    IsAlive = false;
                }
            }
        }

        public void UseItem(Item item)
        {
            EnsureAlive();
            item.AffectCharacter(this);
        }
        public bool IsAlive { get; set; } = true;

        protected void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }
    }
}