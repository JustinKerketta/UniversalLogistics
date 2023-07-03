using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChipSecuritySystem
{
    internal class Program
    {
        /// <summary>
        ///  Pass in a text file that contains the list of bi-colored chips.
        ///  The contents of a sample text file are below:
        ///  
        ///  Blue, Yellow
        ///  Red, Green
        ///  Yellow, Red
        ///  Orange, Purple
        ///  
        /// The output outputs colors from source to destination, supplemented by the 
        /// number of chips used for the path.
        /// </summary>
        /// <see cref="https://www.youtube.com/watch?v=7fujbpJ0LB4"/>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No Input File");
                return;
            }

            string[] allInputLines = File.ReadAllLines(args[0]);
            DFSGraph dfsGraph = new DFSGraph();
            foreach (string singleInputLine in allInputLines)
            {
                string[] chips = singleInputLine.Split(',');
                Color color1 = (Color)Enum.Parse(typeof(Color), chips[0], true);
                Color color2 = (Color)Enum.Parse(typeof(Color), chips[1], true);

                dfsGraph.AddChip(color1, color2);
            }
            List<List<int>> pathsFound = dfsGraph.FindConnectionLinks();

            foreach (List<int> pathList in pathsFound)
            {
                Console.WriteLine($"Number of chips {pathList.Count - 1}, {string.Join(" ", pathList.Select(x=> (Color)x).ToList())}");
            }
        }
    }
}
