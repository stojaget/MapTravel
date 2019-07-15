using System;
using System.Collections.Generic;
using System.Text;

namespace MapTravel.BL
{
    /// <summary>
    /// For custom exceptions when working with map
    /// </summary>
   public class MapException : Exception
    {
        public MapException(string message): base(message) { }
    }
}
