using System.Collections.Generic;

namespace SprintZero1.Controllers.EnemyControllers
{
    /// <summary>
    /// The NodeComparer class provides a comparison logic for Node objects.
    /// It implements the IComparer interface to define a custom comparison
    /// strategy for Nodes, particularly useful for sorting or ordering collections.
    /// </summary>
    /// <author>Zihe Wang</author>
    public class NodeComparer : IComparer<Node>
    {
        /// <summary>
        /// Compares two Node objects and returns a value indicating whether one is less than,
        /// equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first Node object to compare.</param>
        /// <param name="y">The second Node object to compare.</param>
        /// <returns>An integer that indicates the relative values of the x and y parameters.</returns>
        public int Compare(Node x, Node y)
        {
            // Delegate to the Node's CompareTo method
            return x.CompareTo(y);
        }
    }
}
