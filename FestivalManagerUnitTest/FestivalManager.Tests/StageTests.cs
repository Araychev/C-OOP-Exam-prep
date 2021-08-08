// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 

//using System.Collections.Generic;
//using FestivalManager.Entities;

using FestivalManager.Entities;

namespace FestivalManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
	public class StageTests
    {
        private Stage stage;

        [SetUp]
        public void Setup()
        {
            stage = new Stage();
        }

		[Test]
	    public void AddPerformer_ThrowException()
        {
            Performer performer = new Performer("Ivan", "Tolev", 17);

            Assert.Throws<ArgumentException>((() => stage.AddPerformer(performer)));
        }

		[Test]
	    public void AddPerformer_Success()
        {
            Performer performer = new Performer("Ivan", "Tolev", 37);

            stage.AddPerformer(performer);
          
        }

        [Test]
        public void AddSong_ThrowException()
        {
            Song song = new Song("Lambada", TimeSpan.Zero);

            Assert.Throws<ArgumentException>((() => stage.AddSong(song)));
        }

        [Test]
        public void AddSong_Success()
        {
            Song song = new Song("Lambada", TimeSpan.FromMinutes(30));

           stage.AddSong(song);
        }

        [Test]
        public void AddSongToPerformer_Success()
        {
            var performer = new Performer("Ivan", "Ivanov", 19);
            var song = new Song("Ветрове", new TimeSpan(0,3,30));
            stage.AddSong(song);
            stage.AddPerformer(performer);
           

            string expected = "Ветрове (03:30) will be performed by Ivan Ivanov";
            string actual = stage.AddSongToPerformer("Ветрове", "Ivan Ivanov");

          Assert.AreEqual(expected,actual);

        }

        [Test]
        public void Play_Success()
        {

            var performer = new Performer("Ivan", "Ivanov", 19);
            var song = new Song("Ветрове", new TimeSpan(0,3,30));
            stage.AddSong(song);
            stage.AddPerformer(performer);
            stage.AddSongToPerformer("Ветрове", "Ivan Ivanov");
           

            string expected = "1 performers played 1 songs";
            string actual = stage.Play();

          Assert.AreEqual(expected,actual);

        }




	}
}