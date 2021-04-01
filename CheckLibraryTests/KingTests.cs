using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CheckLibrary.Tests
{
    [TestClass()]
    public class KingTests
    {
        Table table;
        Cell cell, cell2, cell3;
        King check, check2;

        [TestInitialize]
        public void Initialize()
        {
            table = new Table();
            cell = table[0, 5];
            cell2 = table[1, 4];
            cell3 = table[2, 3];
        }

        [TestMethod()]
        public void CheckTest()
        {
            check = new King(new Table(), Color.WhiteColor);
            check2 = new King(check.FigureTable, check.Color);
            Assert.AreEqual(check, check2);
        }
        [TestMethod()]
        public void NullTableTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => check = new King(null, Color.WhiteColor));
        }
        [TestMethod()]
        public void CanMoveTest()
        {
            King check = new King(table, Color.WhiteColor);
            cell.Figure = check;

            Assert.AreEqual(true, check.CanMove(cell2));
        }
        [TestMethod()]
        public void CanMoveTest2()
        {
            check = new King(table, Color.WhiteColor);
            cell.Figure = check;

            Assert.AreEqual(true, check.CanMove(cell3));
        }
        [TestMethod()]
        public void CanMoveTest3()
        {
            check = new King(table, Color.WhiteColor);
            cell3.Figure = check;
            cell2.ClearFigure();
            cell.ClearFigure();

            Assert.AreEqual(true, check.CanMove(cell));
        }
        [TestMethod()]
        public void CanMoveFailTest()
        {
            check = new King(table, Color.WhiteColor);
            cell.Figure = check;
            cell2.Figure = new King(check.FigureTable, check.Color);

            Assert.AreEqual(false, check.CanMove(cell2));
        }

        [TestMethod()]
        public void CanCutDownTest()
        {
            cell.Figure = new King(table, Color.WhiteColor);
            cell2.Figure = new King(table, Color.BlackColor);

            Assert.AreEqual(true, (cell.Figure as Figure).CanCutDown(cell3));
        }
        [TestMethod()]
        public void CanCutDownTest2()
        {
            check = new King(table, Color.WhiteColor);
            cell.Figure = check;
            cell3.Figure = new King(table, Color.BlackColor);
            Cell cell4 = table[3, 2];

            cell2.ClearFigure();
            cell4.ClearFigure();

            Assert.AreEqual(true, check.CanCutDown(cell4));
        }
        [TestMethod()]
        public void CanCutDownFailTest()
        {
            cell.Figure = new King(table, Color.WhiteColor);
            cell2.Figure = new King(table, Color.BlackColor);

            Cell cell4 = table[3, 2];
            cell4.ClearFigure();

            Assert.AreEqual(false, (cell.Figure as Figure).CanCutDown(cell4));
        }
        [TestMethod()]
        public void CanCutDownFailTest2()
        {
            cell.Figure = new King(table, Color.WhiteColor);
            cell2.Figure = new King(table, Color.WhiteColor);

            Assert.AreEqual(false, (cell.Figure as Figure).CanCutDown(cell3));
        }
        [TestMethod()]
        public void CanCutDownFailTest3()
        {
            cell.Figure = new King(table, Color.WhiteColor);
            cell2.Figure = new King(table, Color.BlackColor);
            cell3.Figure = new King(table, Color.WhiteColor);

            Assert.AreEqual(false, (cell.Figure as Figure).CanCutDown(cell3));
        }
        [TestMethod()]
        public void HaveEnemyTest()
        {
            cell.Figure = new King(table, Color.WhiteColor);
            cell2.Figure = new King(table, Color.BlackColor);

            Assert.AreEqual(true, (cell.Figure as Figure).HaveEnemy());
        }
        [TestMethod()]
        public void HaveEnemyTest2()
        {
            cell.Figure = new King(table, Color.WhiteColor);
            cell3.Figure = new King(table, Color.BlackColor);

            Cell cell4 = table[3, 2];
            cell4.ClearFigure();

            Assert.AreEqual(true, (cell.Figure as Figure).HaveEnemy());
        }
        [TestMethod()]
        public void HaveEnemyFailTest()
        {
            cell.Figure = new King(table, Color.WhiteColor);
            cell2.Figure = new King(table, Color.WhiteColor);

            Assert.AreEqual(false, (cell.Figure as Figure).HaveEnemy());
        }
        [TestMethod()]
        public void HaveEnemyFailTest2()
        {
            cell.Figure = new King(table, Color.WhiteColor);

            Cell cell2 = table[1, 5];
            cell2.Figure = new King(table, Color.BlackColor);

            Assert.AreEqual(false, (cell.Figure as Figure).HaveEnemy());
        }
        [TestMethod()]
        public void MoveTest()
        {
            King check = new King(table, Color.WhiteColor);
            cell.Figure = check;

            check.Move(cell3);

            Assert.AreEqual(check.Cell, cell3);
        }

        [TestMethod()]
        public void CutDownTest()
        {
            King king = new King(table, Color.WhiteColor);
            cell.Figure = king;
            cell3.Figure = new Check(table, Color.BlackColor);

            Cell cell4 = table[3, 2];
            cell4.ClearFigure();

            king.CutDown(cell4);

            Assert.AreEqual(king.Cell, cell4);
        }
    }
}