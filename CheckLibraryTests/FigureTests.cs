using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckLibrary.Tests
{
    [TestClass()]
    public class FigureTests
    {

        [TestMethod()]
        public void IsDifferentColorTest()
        {
            Table table = new Table();
            Figure fig1 = new Check(table, Color.WhiteColor);
            Figure fig2 = new King(table, Color.BlackColor);
            Figure fig3 = new Check(table, Color.WhiteColor);

            Assert.AreEqual(true, fig1.IsDifferentColor(fig2));
        }
        [TestMethod()]
        public void IsDifferentColorTest2()
        {
            Table table = new Table();
            Figure fig1 = new Check(table, Color.WhiteColor);
            Figure fig2 = new King(table, Color.BlackColor);
            Figure fig3 = new Check(table, Color.WhiteColor);

            Assert.AreEqual(false, fig3.IsDifferentColor(fig1));
        }

        [TestMethod()]
        public void IsWhiteColorTest()
        {
            Table table = new Table();
            Figure fig1 = new Check(table, Color.WhiteColor);
            Figure fig2 = new Check(table, Color.BlackColor);

            Assert.AreEqual(true, fig1.IsWhiteColor());
        }
        [TestMethod()]
        public void IsWhiteColorTest2()
        {
            Table table = new Table();
            Figure fig1 = new Check(table, Color.WhiteColor);
            Figure fig2 = new Check(table, Color.BlackColor);

            Assert.AreEqual(false, fig2.IsWhiteColor());
        }

        [TestMethod()]
        public void IsBlackColorTest()
        {
            Table table = new Table();
            Figure fig1 = new Check(table, Color.BlackColor);
            Figure fig2 = new Check(table, Color.WhiteColor);

            Assert.AreEqual(true, fig1.IsBlackColor());
        }
        [TestMethod()]
        public void IsBlackColorTest2()
        {
            Table table = new Table();
            Figure fig1 = new Check(table, Color.BlackColor);
            Figure fig2 = new Check(table, Color.WhiteColor);

            Assert.AreEqual(false, fig2.IsBlackColor());
        }
    }
}