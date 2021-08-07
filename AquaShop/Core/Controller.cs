using System;
using System.Collections.Generic;
using System.Text;
using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories;
using AquaShop.Utilities.Messages;
using System.Linq;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private DecorationRepository decorations;
        private List<IAquarium> aquariums;

        public Controller()
        {
            aquariums = new List<IAquarium>();
            decorations = new DecorationRepository();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            if (aquariumType!=nameof(FreshwaterAquarium) && aquariumType!= nameof(SaltwaterAquarium))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            switch (aquariumType)
            {
                case "FreshwaterAquarium":
                    aquariums.Add(new FreshwaterAquarium(aquariumName));
                    break;
                case "SaltwaterAquarium":
                    aquariums.Add(new SaltwaterAquarium(aquariumName));
                    break;
            }

            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            if (decorationType!=nameof(Ornament) && decorationType!=nameof(Plant))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            switch (decorationType)
            {
                case "Ornament":
                    decorations.Add(new Ornament());
                    break;
                case "Plant":
                    decorations.Add(new Plant());
                    break;
            }

            return string.Format(OutputMessages.SuccessfullyAdded,decorationType);

        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration desireDecoration = decorations.FindByType(decorationType);

            if (desireDecoration is null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentDecoration,
                    decorationType));
            }

            decorations.Remove(desireDecoration);
            IAquarium desiredAquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            if (desiredAquarium != null) desiredAquarium.AddDecoration(desireDecoration);

            return string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            if (fishType != nameof(FreshwaterFish) && fishType != nameof(SaltwaterFish))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            IFish fish;
            IAquarium desireAquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);


            if (fishType == nameof(SaltwaterFish))
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
                if (desireAquarium.GetType().Name != nameof(SaltwaterAquarium))
                {
                    return OutputMessages.UnsuitableWater;
                }
            }
            else
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
                if (desireAquarium.GetType().Name != nameof(FreshwaterAquarium))
                {
                    return OutputMessages.UnsuitableWater;
                }

            }
            desireAquarium.AddFish(fish);

            return string.Format(OutputMessages.EntityAddedToAquarium,fishType,aquariumName);
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            
            
                aquarium.Feed();

                return string.Format(OutputMessages.FishFed, aquarium.Fish.Count);
            
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);

            var sumOfDecorations = aquarium.Decorations.Sum(x => x.Price);
            var sumOfFish = aquarium.Fish.Sum(x => x.Price);

            var totalSum = (sumOfDecorations + sumOfFish);

            return string.Format(OutputMessages.AquariumValue, aquariumName, totalSum);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var aquarium in aquariums)
            {
                sb.Append(aquarium.GetInfo() + Environment.NewLine);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
