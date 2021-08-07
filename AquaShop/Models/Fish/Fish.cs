using System;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;

namespace AquaShop.Models.Fish
{
    public abstract class Fish : IFish
    {
        private string _name;
        private string _species;
        private decimal _price;

        protected Fish(string name, string species, decimal price)
        {
            Name = name;
            Species = species;
            Price = price;
        }

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidFishName);
                }

                _name = value;

            }
        }

        public string Species
        {
            get => _species;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidFishSpecies);
                }

                _species = value;

            }
        }

        public int Size { get; protected set; }

        public decimal Price
        {
            get => _price;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidFishPrice);
                }

                _price = value;

            }
        }


        public abstract void Eat();
    }
}
