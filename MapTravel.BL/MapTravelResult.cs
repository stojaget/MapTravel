using System;
using System.Collections.Generic;
using System.Text;

namespace MapTravel.BL
{
    /// <summary>
    /// Store result of map travel
    /// </summary>
   public class MapTravelResult
    {
        public string Letters { get; }
        public string Path { get; }

        public MapTravelResult(string letters, string path)
        {
            Letters = letters;
            Path = path;
        }
    }
}
