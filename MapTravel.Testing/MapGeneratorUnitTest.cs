using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using MapTravel.BL;

namespace MapTravel.Testing
{
    [TestClass]
   public class MapGeneratorUnitTest
    {
        [TestMethod]
        public void FirstMapCreated()
        {      
            var resultMap = MapGenerator.GenerateMap(DataSet.FirstTestMap);
            Assert.IsNotNull(resultMap);
        }

        [TestMethod]
        public void SecondMapCreated()
        {
            var resultMap = MapGenerator.GenerateMap(DataSet.SecondTestMap);
            Assert.IsNotNull(resultMap);
        }

        [TestMethod]
        public void InvalidAscii()
        {
            var resultChar = MapGenerator.IsValidAcsii(DataSet.TestInvalidAscii);
            Assert.IsFalse(resultChar);
        }

        [TestMethod]
        public void TestStartingMark()
        {
            var resultChar = MapGenerator.IsStartChar(DataSet.TestStartingMark);
            Assert.IsTrue(resultChar);
        }

        [TestMethod]
        public void TestEndMark()
        {
            var resultChar = MapGenerator.IsEndChar(DataSet.TestStartingMark);
            Assert.IsTrue(resultChar);
        }
    }
}
