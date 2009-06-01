using targetshooter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace UnitTest
{
    
    
    /// <summary>
    ///This is a test class for TargetShooterTest and is intended
    ///to contain all TargetShooterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TargetShooterTest
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
        ///A test for tankscollide
        ///Author: Shailaja Raju
        ///</summary>
        [TestMethod()]
        [DeploymentItem("targetshooter.exe")]
        public void tankscollideTestpass()
        {
            TargetShooter_Accessor target = new TargetShooter_Accessor(); // TODO: Initialize to an appropriate value
            Vector2 object1Pos = new Vector2(1000, 1000); // TODO: Initialize to an appropriate value
            int object1Width = 500; // TODO: Initialize to an appropriate value
            int object1Height = 800; // TODO: Initialize to an appropriate value
            Vector2 object2Pos = new Vector2(50, 50); // TODO: Initialize to an appropriate value
            int object2Width = 500; // TODO: Initialize to an appropriate value
            int object2Height = 800; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.tankscollide(object1Pos, object1Width, object1Height, object2Pos, object2Width, object2Height);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void tankscollideTestfail()
        {
            TargetShooter_Accessor target = new TargetShooter_Accessor(); // TODO: Initialize to an appropriate value
            Vector2 object1Pos = new Vector2(500, 500); // TODO: Initialize to an appropriate value
            int object1Width = 500; // TODO: Initialize to an appropriate value
            int object1Height = 800; // TODO: Initialize to an appropriate value
            Vector2 object2Pos = new Vector2(500, 500); // TODO: Initialize to an appropriate value
            int object2Width = 500; // TODO: Initialize to an appropriate value
            int object2Height = 800; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual =   target.tankscollide(object1Pos, object1Width, object1Height, object2Pos, object2Width, object2Height);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
