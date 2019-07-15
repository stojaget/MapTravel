using MapTravel.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapTravel.Testing
{

    [TestClass]
    public class MapTravelUnitTest
    {
        [TestMethod]
        public void FirstTestMapSolved()
        {
            var result = MapDirections.SearchMap(DataSet.FirstTestMap);
            Assert.AreEqual(DataSet.FirstTestLetters, result.Letters);
            Assert.AreEqual(DataSet.FirstTestPath, result.Path);
        }

        [TestMethod]
        public void SecondTestMapSolved()
        {
            var result = MapDirections.SearchMap(DataSet.SecondTestMap);
            Assert.AreEqual(DataSet.SecondTestLetters, result.Letters);
            Assert.AreEqual(DataSet.SecondTestPath, result.Path);
        }

        [TestMethod]
        public void ThirdTestMapSolved()
        {
            var result = MapDirections.SearchMap(DataSet.ThirdTestMap);
            Assert.AreEqual(DataSet.ThirdTestLetters, result.Letters);
            Assert.AreEqual(DataSet.ThirdTestPath, result.Path);
        }


    }

}
