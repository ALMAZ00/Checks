using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CheckLibrary.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void GameTest()
        {
            Game game;
            Assert.ThrowsException<ArgumentNullException>(() => game = new Game(null, null, null));
        }
        [TestMethod()]
        public void GameTest2()
        {
            Game game;
            Assert.ThrowsException<ArgumentNullException>(() => game = new Game(new GameSettings("1.txt"), null, null));
        }
    }
}