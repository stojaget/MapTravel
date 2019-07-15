using MapTravel.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapTravel.Testing
{
    [TestClass]
    public class MapDirectionUnitTest
    {
        [TestMethod]
        public void SearchNextChars()
        {
            MapDirections inputMap = new MapDirections(new char[2, 2] {
            {'@', 'c' },
            {'D', ' '} }, 2, 2, 0, 0);

            var resultChar = inputMap.SearchNextChar(MapMove.Right);
            Assert.IsNotNull(resultChar);
            Assert.AreEqual('c', resultChar);

            resultChar = inputMap.SearchNextChar(MapMove.Down);
            Assert.IsNotNull(resultChar);
            Assert.AreEqual('D', resultChar);

            resultChar = inputMap.SearchNextChar(MapMove.Up);
            Assert.IsNull(resultChar);
       
        }

   
        [TestMethod]
        public void MoveIsValid()
        {
            MapDirections inputMap = new MapDirections(new char[2, 2] {
            {'@', 'c' },
            {'D', ' '} }, 2, 2, 0, 0);

            var resultChar = inputMap.IsValidMove(MapMove.Up);
            Assert.IsFalse(resultChar);

            resultChar = inputMap.IsValidMove(MapMove.Right);
            Assert.IsTrue(resultChar);

        }
    }
}
