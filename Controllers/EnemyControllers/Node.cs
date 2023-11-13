using Microsoft.Xna.Framework;
using System;
using System.Threading;

namespace SprintZero1.Controllers.EnemyControllers
{
    /// <summary>
    /// The Node class represents a node in a pathfinding context, such as in A* pathfinding algorithm.
    /// Each node has a position, cost estimates (F, G, H), and an ID for unique identification.
    /// </summary>
    /// <author>Zihe Wang</author>
    public class Node : IComparable<Node>
    {
        public Node Parent; // Reference to the parent node in the path
        public Vector2 Position; // The position of the node on the grid
        public float F; // Total cost of the node (G + H)
        public float G; // Cost from the start node to this node
        public float H; // Heuristic cost estimate from this node to the end node
        private static int nextId; // Static counter for generating unique IDs
        public int Id { get; private set; } // Unique identifier for the node
        private const int BlockSize = 16; // Size of each grid block

        /// <summary>
        /// Initializes a new instance of the Node class with the specified position.
        /// </summary>
        /// <param name="position">The position of the node in world coordinates.</param>
        public Node(Vector2 position)
        {
            // Position is adjusted to grid coordinates based on BlockSize
            Position = new Vector2((int)position.X / BlockSize, (int)position.Y / BlockSize);
            // Assign a unique ID to the node
            Id = Interlocked.Increment(ref nextId);
        }

        /// <summary>
        /// Compares this node to another node based on the total cost (F) and then by ID for tie-breaking.
        /// </summary>
        /// <param name="other">The Node to compare this node against.</param>
        /// <returns>An integer indicating the relative order of this node compared to the other.</returns>
        public int CompareTo(Node other)
        {
            // Compare based on total cost (F)
            int compare = F.CompareTo(other.F);
            // If costs are equal, compare based on node ID
            if (compare == 0)
            {
                compare = Id.CompareTo(other.Id);
            }
            return compare;
        }
    }
}
