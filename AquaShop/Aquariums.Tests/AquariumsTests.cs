namespace Aquariums.Tests
{
    using System;
    using NUnit.Framework;

    public class AquariumsTests
    {
        private Aquarium aquarium;
        private Fish fish;

        [SetUp]
        public void Setup()
        {
            aquarium = new Aquarium("saltWater",1);
            fish = new Fish("Gupa");
        }

        [Test]
        public void Add_ShouldThrowException()
        {
            Exception ex = Assert.Throws<InvalidOperationException>((() =>
            {
                aquarium.Add(new Fish("Ivan"));
                aquarium.Add(new Fish("Gosho"));

            }));

            Assert.AreEqual(ex.Message,"Aquarium is full!");
        }

        [Test]
        public void RemoveFish_ShouldThrowException()
        {
            Exception ex = Assert.Throws<InvalidOperationException>((() =>
            {
                aquarium.RemoveFish("Gosho");
            }));

            Assert.AreEqual(ex.Message,"Fish with the name Gosho doesn't exist!");
        }
        [Test]
        public void CellFish_ShouldThrowException()
        {
            Exception ex = Assert.Throws<InvalidOperationException>((() =>
            {
                aquarium.SellFish("Mimi");
            }));

            Assert.AreEqual(ex.Message,"Fish with the name Mimi doesn't exist!");
        }
        [Test]
        public void Name_Empty()
        {
            Assert.Throws<ArgumentNullException>((() => new Aquarium(null, 1)));
            Assert.Throws<ArgumentNullException>((() => new Aquarium(String.Empty, 1)));

        }
        [Test]
        public void Capacity_ThrowExeption()
        {
            Assert.Throws<ArgumentException>((() => new Aquarium("a", -1)));

        }

        [Test]
        public void Count()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            aquarium.Add(new Fish("Ivan"));
            int expectedCount = 1;
            Assert.AreEqual(expectedCount,aquarium.Count);

        }
        [Test]
        public void Remove()
        {
            Aquarium aquarium = new Aquarium("Big", 1);

            aquarium.Add(new Fish("alabala"));
            aquarium.RemoveFish("alabala");

            Assert.AreEqual(aquarium.Count,0);

        }
        [Test]
        public void sellFish()
        {
            Aquarium aquarium = new Aquarium("Big", 1);

            aquarium.Add(new Fish("alabala"));
            Fish fish = aquarium.SellFish("alabala");

            Assert.AreEqual(fish.Name,"alabala");
            Assert.AreEqual(fish.Available,false);

        }

        [Test]
        public void Report()
        {
            Aquarium aquarium = new Aquarium("Big", 1);
            aquarium.Add(new Fish("alabala"));

            string expectedMassege = "Fish available at Big: alabala";

            Assert.AreEqual(expectedMassege,aquarium.Report());
        }

    }
}
