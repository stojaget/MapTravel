using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MapTravel;

namespace MapTravel.Test
{
    [TestClass]
    public class MapTravelUnitTest
    {
        [TestMethod]
        public void FirstTestMapSolved()
        {
            var result = MapDirections.FindPath(DataSet.FirstTestMap);
            Assert.Equal(DataSet.FirstTestLetters, result.Letters);
            Assert.Equal(DataSet.FirstTestPath, result.CharacterPath);
        }

        [TestMethod]
        public void SecondTestMapSolved()
        {
            var result = MapDirections.FindPath(DataSet.SecondTestMap);
            Assert.Equal(DataSet.SecondTestLetters, result.Letters);
            Assert.Equal(DataSet.SecondTestPath, result.CharacterPath);
        }

        [TestMethod]
        public void ThirdTestMapSolved()
        {
            var result = MapDirections.FindPath(DataSet.ThirdTestMap);
            Assert.Equal(DataSet.ThirdTestLetters, result.Letters);
            Assert.Equal(DataSet.ThirdTestPath, result.CharacterPath);
        }

      
    }
}
