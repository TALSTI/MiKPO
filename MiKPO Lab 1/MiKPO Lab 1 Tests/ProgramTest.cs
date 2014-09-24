using MiKPO_Lab_1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace MiKPO_Lab_1_Tests
{
    
    
    /// <summary>
    ///This is a test class for ProgramTest and is intended
    ///to contain all ProgramTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProgramTest
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
        ///A test for ReadFile
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadFileTest()
        {
            string inFile = string.Empty; // TODO: Initialize to an appropriate value
            string outFile = string.Empty; // TODO: Initialize to an appropriate value
            Program.ReadFile(inFile, outFile);
        }

        /// <summary>
        ///A test for Main
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MiKPO Lab 1.exe")]
        public void MainTest()
        {
            string[] args = null; // TODO: Initialize to an appropriate value
            Program_Accessor.Main(args);
        }

        /// <summary>
        ///A test for DegreeToRad
        ///</summary>
        [TestMethod()]
        public void DegreeToRadTest()
        {
            double degree = 0F; // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = Program.DegreeToRad(degree);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetAngle
        ///</summary>
        [TestMethod()]
        public void GetAngleTest()
        {
            double side1 = 0F; // TODO: Initialize to an appropriate value
            double side2 = 0F; // TODO: Initialize to an appropriate value
            double angle1 = 0F; // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = Program.GetAngle(side1, side2, angle1);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSide
        ///</summary>
        [TestMethod()]
        public void GetSideTest()
        {
            double side1 = 0F; // TODO: Initialize to an appropriate value
            double side2 = 0F; // TODO: Initialize to an appropriate value
            double angle1 = 0F; // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = Program.GetSide(side1, side2, angle1);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RadToDegree
        ///</summary>
        [TestMethod()]
        public void RadToDegreeTest()
        {
            double rad = 0F; // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = Program.RadToDegree(rad);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CorrectTest()
        {
            Assert.AreEqual(1, Program.GetSide(1, 1, 60));
        }

        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void FileNotFoundTest()
        {
            Program.ReadFile("FNF.txt", "output.txt");
        }

        [TestMethod()]
        public void VeryBigFileTest()
        {
            Program.ReadFile("../../../MiKPO Lab 1/bin/Debug/in/in_verybig.txt", "../../../MiKPO Lab 1/bin/Debug/out/out_verybig.txt");
        }

        [TestMethod()]
        public void EmptyFileTest()
        {
            Program.ReadFile("../../../MiKPO Lab 1/bin/Debug/in/in_empty.txt", "../../../MiKPO Lab 1/bin/Debug/out/out_empty.txt");
        }

        [TestMethod()]
        public void LettersTest()
        {
            Program.GetSide('a', 'b', 'c');
        }

        [TestMethod()]
        public void RandomNumbersTest()
        {
            Program.GetSide(2.374823748923671238467812356478012, 1.4173824204638246290364789223648923462389, 21.4389206856123478564785681247);
        }

        [TestMethod()]
        public void MinusTest()
        {
            Program.GetSide(-1, -2, -33);
        }
    }
}
