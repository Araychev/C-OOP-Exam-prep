using System;
using NUnit.Framework;

namespace Computers.Tests
{
    public class Tests
    {
        private ComputerManager computerManager;

        [SetUp]
        public void Setup()
        {
            computerManager = new ComputerManager();
        }

        [Test]
        public void AddComputer_ThrowExeption()
        {
            Computer computer = new Computer("IBM", "LT50", 1580m);
            computerManager.AddComputer(computer);

            Assert.Throws<ArgumentException>((() =>computerManager.AddComputer(computer)));
            Assert.AreEqual(1,computerManager.Count);
        }

        [Test]
        public void RemoveComputer_Success()
        {
            Computer computer = new Computer("IBM", "LT50", 1580m);
            computerManager.AddComputer(computer);

            computerManager.RemoveComputer("IBM", "LT50");
        }

        [Test]
        public void GetComputer_ThrowExeption()
        {

            Assert.Throws<ArgumentException>((() =>computerManager.GetComputer("1","2") ));
        }

        [Test]
        public void GetComputersByManufacturer_Success()
        {
            Computer computer = new Computer("IBM", "LT50", 1580m);
            computerManager.AddComputer(computer);

            computerManager.GetComputersByManufacturer("IBM");

        }
    }
}