using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CheckLibrary.Tests
{
    [TestClass()]
    public class TableTests
    {
        Table table;

        [TestInitialize]
        public void Initialize()
        {
            table = new Table();
        }

        [TestMethod()]
        public void TableTest()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => table[-1, 0]);
        }
        [TestMethod()]
        public void TableTest2()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => table[0, -1]);
        }
        [TestMethod()]
        public void TableTest3()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => table[0, 8]);
        }
        [TestMethod()]
        public void TableTest4()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => table[8, 0]);
        }
        [TestMethod()]
        public void TableFromTest()
        {
            Assert.AreEqual(table.From, null);
        }
        [TestMethod()]
        public void TableToTest()
        {
            Assert.AreEqual(table.To, null);
        }

        [TestMethod()]
        public void CountOfWhiteTest()
        {
            Assert.AreEqual(table.CountOfWhite, 12);
        }

        [TestMethod()]
        public void CountOfBlackTest()
        {
            Assert.AreEqual(table.CountOfBlack, 12);
        }

        [TestMethod()]
        public void CompleteGoTest()
        {
            Figure fig = table[0, 5].Figure as Figure;
            Cell cell2 = table[1, 4];
            table.CompleteGo(new Coordinate(0, 5), WhoGo.White);
            table.CompleteGo(new Coordinate(1, 4), WhoGo.White);

            Assert.AreEqual(fig.Cell, cell2);
        }
        [TestMethod()]
        public void ChangeOnWhiteKingTest()
        {
            Cell cell = table[0, 1];
            cell.Figure = new King(table, Color.WhiteColor);
            table[1, 0].ClearFigure();

            table.CompleteGo(new Coordinate(0, 1), WhoGo.White);
            table.CompleteGo(new Coordinate(1, 0), WhoGo.White);

            Assert.AreEqual(new King(table, Color.WhiteColor).GetType(), cell.Figure.GetType());
        }
        [TestMethod()]
        public void ChangeOnBlackKingTest()
        {
            Cell cell = table[1, 6];
            cell.Figure = new King(table, Color.BlackColor);
            table[0, 7].ClearFigure();

            table.CompleteGo(new Coordinate(1, 6), WhoGo.Black);
            table.CompleteGo(new Coordinate(0, 7), WhoGo.Black);

            Assert.AreEqual(new King(table, Color.BlackColor).GetType(), cell.Figure.GetType());
        }
    }
}