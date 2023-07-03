using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    internal class DFSGraph
    {
        /// <summary>
        /// Array of colors. Each color is a node in the graph. Each of
        /// these colors will be connected to a list of other colors
        /// (specified by bicolored chips).
        /// </summary>
        private readonly List<int>[] nodesColor;

        /// <summary>
        /// Number of nodes in the graph, which is the number of colors.
        /// </summary>
        private int NodesInGraph { get { return nodesColor.Length; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public DFSGraph()
        {
            nodesColor = new List<int>[Enum.GetNames(typeof(Color)).Length];
            for (int ii=0; ii < nodesColor.Length; ii++)
            {
                // Each color can be connected to various other colors. The 
                // bicolored chips will show the connection from each color
                // to other colors. Store these associated colors for each
                // color in the List<int> object.
                nodesColor[ii] = new List<int>();
            }
        }

        /// <summary>
        /// Recursive function that will check source marker to destination marker
        /// connection.
        /// Visit each node that source connects to and recursively see if target
        /// can be reached.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="visitedNode"></param>
        /// <param name="nodesInPath"></param>
        /// <param name="pathsFound"></param>
        private void FindConnectionLinks(int source, int target,
            bool[] visitedNode, List<int> nodesInPath, List<List<int>> pathsFound)
        {
            if (source == target)
            {
                // Terminates the recursion.
                pathsFound.Add(new List<int>(nodesInPath));
            }
            else
            {
                // Mark the source node so that we know if we loop back to it.
                visitedNode[source] = true;

                // Iterate over other nodes that the source node is connected to.
                // Each of this nodes should be tested recursively to see if it
                // is the destination or can lead to the destination.
                foreach (int chipVertex in nodesColor[source])
                {
                    // Avoid a loop by checking if the node has already been visited.
                    if (!visitedNode[chipVertex])
                    {
                        nodesInPath.Add(chipVertex);
                        FindConnectionLinks(chipVertex, target, visitedNode, nodesInPath, pathsFound);
                        nodesInPath.Remove(chipVertex);
                    }
                }

                // Un-mark the source node since we are done testing its connectivity
                // in this iteration.
                visitedNode[source] = false;
            }
        }

        /// <summary>
        /// Indicate that color1 is associated with color2 as conveyed by
        /// a bicolored chip.
        /// </summary>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        public void AddChip(Color color1, Color color2)
        {
            nodesColor[(int)color1].Add((int)color2);
        }

        /// <summary>
        /// Find connection between blue marker and green marker.
        /// Allocate objects that can be passed to the private recursive function
        /// that does all the work.
        /// </summary>
        public List<List<int>> FindConnectionLinks()
        {
            int source = (int)Color.Blue;
            int target = (int)Color.Green;

            // Keep track of nodes traversed while attempting source to destination.
            List<int> nodesInPath = new List<int>();
            nodesInPath.Add(source);

            // Keep track of whether a node has already been visited to avoid
            // loops.
            bool[] visitedNode = new bool[NodesInGraph];

            // List of paths found from source to destination.
            List<List<int>> pathsFound = new List<List<int>>();

            FindConnectionLinks(source, target, visitedNode, nodesInPath, pathsFound);
            return pathsFound;
        }
    }
}
