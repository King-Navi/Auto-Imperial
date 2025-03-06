namespace TestProject
{
    [TestClass]
    public sealed class Test1
    {
        [TestInitialize]
        public void TestInit()
        {
            // This method is called before each test method.
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // This method is called after each test method.
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(true);/
            //Assert.Fail("This test always fails.");
        }
    }
}
