using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CheckLibrary.Tests
{
    [TestClass()]
    public class CheckTests
    {
        Table table;
        Cell cell, cell2, cell3;
        Check check, check2;

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
            check = new Check(table, Color.WhiteColor);
            check2 = new Check(check.FigureTable, check.Color);
            Assert.AreEqual(check, check2);
        }        

        [TestMethod()]
        public void NullTableTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => check = new Check(null, Color.WhiteColor));
        }
        [TestMethod()]
        public void CanMoveTest()
        {            
            check = new Check(table, Color.WhiteColor);
            cell.Figure = check;

            Assert.AreEqual(true, check.CanMove(cell2));
        }
        [TestMethod()]
        public void CanMoveFailTest()
        {
            check = new Check(table, Color.WhiteColor);
            cell.Figure = check;
            cell2.Figure = new Check(check.FigureTable, check.Color);

            Assert.AreEqual(false, check.CanMove(cell2));
        }
        [TestMethod()]
        public void CanCutDownTest()
        {
            cell.Figure = new Check(table, Color.WhiteColor);
            cell2.Figure = new Check(table, Color.BlackColor);            

            Assert.AreEqual(true, (cell.Figure as Figure).CanCutDown(cell3));
        }
        [TestMethod()]
        public void CanCutDownFailTest()
        {
            cell.Figure = new Check(table, Color.WhiteColor);

            Assert.AreEqual(false, (cell.Figure as Figure).CanCutDown(cell3));
        }
        [TestMethod()]
        public void CanCutDownFailTest2()
        {
            cell.Figure = new Check(table, Color.WhiteColor);
            cell2.Figure = new Check(table, Color.WhiteColor);

            Assert.AreEqual(false, (cell.Figure as Figure).CanCutDown(cell3));
        }
        [TestMethod()]
        public void CanCutDownFailTest3()
        {
            cell.Figure = new Check(table, Color.WhiteColor);
            cell2.Figure = new Check(table, Color.BlackColor);
            cell3.Figure = new Check(table, Color.WhiteColor);

            Assert.AreEqual(false, (cell.Figure as Figure).CanCutDown(cell3));
        }
        [TestMethod()]
        public void HaveEnemyTest()
        {
            cell.Figure = new Check(table, Color.WhiteColor);
            cell2.Figure = new Check(table, Color.BlackColor);

            Assert.AreEqual(true, (cell.Figure as Figure).HaveEnemy());
        }
        [TestMethod()]
        public void HaveEnemyFailTest()
        {
            cell.Figure = new Check(table, Color.WhiteColor);
            cell2.Figure = new Check(table, Color.WhiteColor);

            Assert.AreEqual(false, (cell.Figure as Figure).HaveEnemy());
        }
        [TestMethod()]
        public void HaveEnemyFailTest2()
        {
            cell.Figure = new Check(table, Color.WhiteColor);
            cell3.Figure = new Check(table, Color.BlackColor);

            Assert.AreEqual(false, (cell.Figure as Figure).HaveEnemy());
        }
        [TestMethod()]
        public void MoveTest()
        {
            Check check = new Check(table, Color.WhiteColor);
            cell.Figure = check;

            check.Move(cell2);

            Assert.AreEqual(check.Cell, cell2);
        }

        [TestMethod()]
        public void CutDownTest()
        {
            Check check = new Check(table, Color.WhiteColor);
            cell.Figure = check;
            cell2.Figure = new Check(table, Color.BlackColor);

            (cell.Figure as Figure).CutDown(cell3);

            Assert.AreEqual(check.Cell, cell3);
        }
    }
}