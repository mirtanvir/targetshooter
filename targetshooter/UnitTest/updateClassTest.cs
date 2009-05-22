using targetshooter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace UnitTest
{
    
    
    /// <summary>
    ///This is a test class for updateClassTest and is intended
    ///to contain all updateClassTest Unit Tests
    ///</summary>
    [TestClass()]
    public class updateClassTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for updateTankPositionDown
        ///By Mir Tanvir Hossain
        ///</summary>
        [TestMethod()]
        public void updateTankPositionDownTestPass()
        {
            int tankAngleInDegree = 0; // TODO: Initialize to an appropriate value
            Vector2 position = new Vector2(10,10); // TODO: Initialize to an appropriate value
            float tankSpeed = 100F; // TODO: Initialize to an appropriate value
            float gameTimeChanged = .001F; // TODO: Initialize to an appropriate value
            Vector2 expected = new Vector2(10,10.1F); // TODO: Initialize to an appropriate value
            Vector2 actual;
            actual = updateClass.updateTankPositionDown(true, tankAngleInDegree, position, tankSpeed, gameTimeChanged);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void updateTankPositionDownTestFail()
        {
            int tankAngleInDegree = 0; // TODO: Initialize to an appropriate value
            Vector2 position = new Vector2(10, 10); // TODO: Initialize to an appropriate value
            float tankSpeed = 100F; // TODO: Initialize to an appropriate value
            float gameTimeChanged = .001F; // TODO: Initialize to an appropriate value
            Vector2 expected = new Vector2(10, 10.3F); // TODO: Initialize to an appropriate value
            Vector2 actual;
            actual = updateClass.updateTankPositionDown(true, tankAngleInDegree, position, tankSpeed, gameTimeChanged);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
