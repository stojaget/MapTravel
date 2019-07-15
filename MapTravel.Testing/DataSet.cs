using System;
using System.Collections.Generic;
using System.Text;

namespace MapTravel.Testing
{
    /// <summary>
    /// Sample data for testing purposes
    /// </summary>
    class DataSet
    {

        public static char TestInvalidAscii = '©';
        public static char TestStartingMark = '@';
        public static char TestEndMark = 'x';


        public static string FirstTestMap = string.Join(Environment.NewLine,
               "@---A---+",
               "        |",
               "x-B-+   C",
               "    |   |",
               "    +---+");
        public static string FirstTestLetters = "ACB";
        public static string FirstTestPath = "@---A---+|C|+---+|+-B-x";

        public static string SecondTestMap = string.Join(Environment.NewLine,
                "@         ",
                "| C----+  ",
                "A |    |  ",
                "+---B--+  ",
                "  |      x",
                "  |      |",
                "  +---D--+");
        public static string SecondTestLetters = "ABCD";
        public static string SecondTestPath = "@|A+---B--+|+----C|-||+---D--+|x";

        public static string ThirdTestMap = string.Join(Environment.NewLine,
                "  @---+   ",
                "      B   ",
                "K-----|--A",
                "|     |  |",
                "|  +--E  |",
                "|  |     |",
                "+--E--Ex C",
                "   |     |",
                "   +--F--+");
        public static string ThirdTestLetters = "BEEFCAKE";
        public static string ThirdTestPath = "@---+B||E--+|E|+--F--+|C|||A--|-----K|||+--E--Ex";

       
    }
}
