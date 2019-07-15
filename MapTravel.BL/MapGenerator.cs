using System;
using System.Collections.Generic;
using System.Text;

namespace MapTravel.BL
{
    public class MapGenerator
    {
        /// <summary>
        /// Create and validate array of chars based on input string
        /// </summary>
        /// <param name="asciiMap"></param>
        /// <returns></returns>
        public static MapDirections GenerateMap(string asciiMap)
        {
            if (string.IsNullOrEmpty(asciiMap))
                throw new MapException("Please input valid ASCII map file...");

            bool startFound = false;
            bool endFound = false;

            int startRowIndex = -1;
            int startColumnIndex = -1;

            var mapRows = asciiMap.Split(Environment.NewLine);
            int mapColumns = mapRows[0].Length;
            char[,] mapChars = new char[mapRows.Length, mapColumns];

            for (int currentRowIndex = 0; currentRowIndex < mapRows.Length; currentRowIndex++)
            {
                var inputRow = mapRows[currentRowIndex];

                if (mapColumns != inputRow.Length)
                    throw new MapException("For every character in row there should be column. Please correct ASCII map file...");

                for (int currentColumnIndex = 0; currentColumnIndex < inputRow.Length; currentColumnIndex++)
                {
                    char c = inputRow[currentColumnIndex];

                    if (!IsValidAcsii(c))
                    {
                        string warningMessage = "Character " + c + " is not valid ASCII character.";
                        throw new MapException(warningMessage);
                    }

                    if (IsStartChar(c))
                    {
                        if (startFound)
                            throw new MapException("ASCII map file is allowed to have only one start character. Please delete one start character from ASCII map file.");

                        startFound = true;
                        startRowIndex = currentRowIndex;
                        startColumnIndex = currentColumnIndex;
                    }

                    if (IsEndChar(c))
                        endFound = true;

                    mapChars[currentRowIndex, currentColumnIndex] = c;
                }
            }

            if (!startFound)
                throw new MapException("ASCII map file must have one start character. Please write one start character into ASCII map file.");

            if (!endFound)
                throw new MapException("ASCII map file must have one end character. Please write one end character into ASCII map file.");

            return new MapDirections(mapChars, mapRows.Length, mapColumns, startRowIndex, startColumnIndex);
        }
        /// <summary>
        /// Fancy way of checking for valid ASCII, ASCII ranges from 0 - 127
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsValidAcsii(char c)
        {
            return c <= sbyte.MaxValue;
        }

        public static bool IsStartChar(char c)
        {
            return c == MapConstant.Start;
        }

        public static bool IsEndChar(char c)
        {
            return c == MapConstant.End;
        }
    }
}
