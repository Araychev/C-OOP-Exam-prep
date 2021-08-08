using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {

        [SetUp]
        public void Setup()
        {
            UnitDriver driver = new UnitDriver("Ivan", new UnitCar("Fiat", 150, 1900));
            Dictionary<string, UnitDriver> drivers = new Dictionary<string, UnitDriver>();
        }

        [Test]
        public void AddDriver_ThrowExceptionWhenNullisPassed()
        {
            var raceEntry = new RaceEntry();

           Assert.Throws<InvalidOperationException>
               ((() => raceEntry.AddDriver(null)));
        }
        [Test]
        public void AddDriver_ThrowExceptionWhenDriverExist()
        {
            var raceEntry = new RaceEntry();
            UnitDriver driver = new UnitDriver("Ivan", new UnitCar("Fiat", 150, 1900));
            raceEntry.AddDriver(driver);

           Assert.Throws<InvalidOperationException>
               ((() => raceEntry.AddDriver(driver)));
           Assert.AreEqual(1,raceEntry.Counter);
        }

        [Test]
        public void CalculateAverageHorsePower_ThrowExceptionWhenDriverCountIsSmallerThanMinParticipant()
        {
            int MinParticipants = 2;
            var raceEntry = new RaceEntry();
            Dictionary<string, UnitDriver> driver = new Dictionary<string, UnitDriver>();

           Assert.Throws<InvalidOperationException>
               ((() => raceEntry.CalculateAverageHorsePower()));
        }

        [Test]
        public void CalculateAverageHorsePower()
        {
            
            var raceEntry = new RaceEntry();

            UnitDriver driver = new UnitDriver("Ivan", new UnitCar("Fiat", 100, 1900));
            UnitDriver driver1 = new UnitDriver("Dragan", new UnitCar("BMW", 100, 2000));
            UnitDriver driver2 = new UnitDriver("Miro", new UnitCar("Lada", 100, 1500));


            raceEntry.AddDriver(driver);
            raceEntry.AddDriver(driver1);
            raceEntry.AddDriver(driver2);
           
            var actual = raceEntry.CalculateAverageHorsePower();
                

            Assert.AreEqual(100, actual);

         
        }
    }
}