using System;
using System.IO;
using MapTravel.BL;

namespace MapTravel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WELCOME TO ASCII MAP TRAVEL SOFTWARE");
            Console.WriteLine("Please input file path (ex. C:\\Documents\\TravelMap.txt) and we will find letters and the path for you");
            string pathToFile = Console.ReadLine();
            // string pathToFile = @"c:\deploy\FirstTravelMap.txt";
            string inputString;
            try
            {
                inputString = HelperReadAllText(pathToFile);
                if (inputString == null || inputString.Length == 0)
                {
                    Console.WriteLine("Please input file path...");
                    return;
                }
                ProcessInputMap(inputString);
                Console.ReadLine();

            }
            catch (Exception)
            {

                Console.WriteLine("Error occured during file opening, please try again...");
                return;
            }


        }

        private static void ProcessInputMap(string inputString)
        {
            try
            {
                var result = MapDirections.SearchMap(inputString);
                Console.WriteLine($"Letters: {result.Letters}");
                Console.WriteLine($"Path as characters: {result.Path}");
            }
            catch (System.IO.IOException error)
            {
                Console.WriteLine($"Error while reading file:  { error.Message}");
            }
        }

        /// <summary>
        /// Read all text from file 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string HelperReadAllText(string path)
        {
            string resultString = "";
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader streamReader = new StreamReader(path))
                    {
                        resultString = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                throw new MapException("Please input valid path to file with ASCII map...");
            }
            return resultString;
        }

    }
}
