using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CheckLibrary.Tests
{
    [TestClass()]
    public class CellTests
    {
        Cell cell;
        [TestInitialize]
        public void Initialize()
        {
            cell = new Cell(0, 0, new Check(new Table(), Color.WhiteColor));
        }
        [TestMethod()]
        public void SetNullFigureTest()
        {
            cell.Figure = null;
            Assert.AreEqual(new EmptyFigure().GetType(), cell.Figure.GetType());
        }
        [TestMethod()]
        public void NullTableTest()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => cell = new Cell(-1, 0));
        }
        [TestMethod()]
        public void NullTableTest2()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => cell = new Cell(0, -1));
        }
        [TestMethod()]
        public void NullTableTest3()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => cell = new Cell(8, 0));
        }
        [TestMethod()]
        public void NullTableTest4()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => cell = new Cell(0, 8));
        }

        [TestMethod()]
        public void ClearFigureTest()
        {
            cell.ClearFigure();
            Assert.AreEqual(new EmptyFigure().GetType(), cell.Figure.GetType());
        }

        [TestMethod()]
        public void ClearCheckFigureTest()
        {
            Cell cell2 = new Cell(0, 0);
            cell.ClearFigure();
            Assert.AreEqual(cell, cell2);
        }
        [TestMethod()]
        public void ClearKingFigureTest1()
        {
            Cell cell2 = new Cell(0, 0);
            cell = new Cell(0, 0, new King(new Table(), Color.WhiteColor));
            cell.ClearFigure();
            Assert.AreEqual(cell, cell2);
        }
        [TestMethod()]
        public void IsNotEmptyTest()
        {
            Cell cell2 = new Cell(0, 0);
            Assert.AreEqual(false, cell2.IsNotEmpty());
        }
        [TestMethod()]
        public void IsNotEmptyTest2()
        {
            Assert.AreEqual(true, cell.IsNotEmpty());
        }
    }
}