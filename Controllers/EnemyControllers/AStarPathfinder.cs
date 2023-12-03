using Microsoft.Xna.Framework;
using SprintZero1.Entities.EntityInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprintZero1.Controllers.EnemyControllers
{
    /// <summary>
    /// The AStarPathfinder class implements the A* pathfinding algorithm to find the shortest path
    /// from a start point to an end point in a game environment.
    /// </summary>
    /// <author>Zihe Wang</author>
    public class AStarPathfinder
    {
        private PriorityQueue<Node, float> OpenList; // List of nodes to be evaluated
        private List<Node> ClosedList; // List of nodes already evaluated
        private Task pathfindingTask; // Task for running pathfinding in the background
        private Stack<Vector2> pathResult; // Resulting path from start to end
        private bool isPathfindingDone; // Flag to indicate if pathfinding is complete
        private const int BlockSize = 16; // Size of each grid block
        private List<IEntity> _blockEntities;

        public AStarPathfinder(List<IEntity> blockEntities)
        {
            _blockEntities = blockEntities;
            OpenList = new PriorityQueue<Node, float>();
            ClosedList = new List<Node>();
            isPathfindingDone = false;
            pathResult = null;
        }

        /// <summary>
        /// Starts the pathfinding process from a start point to an end point asynchronously.
        /// </summary>
        /// <param name="start">Starting point of the path.</param>
        /// <param name="end">End point of the path.</param>
        public void StartFindingPath(Vector2 start, Vector2 end)
        {
            isPathfindingDone = false;
            pathResult = null;
            pathfindingTask = Task.Run(() =>
            {
                pathResult = FindPath(start, end);
                isPathfindingDone = true;
            });
        }

        /// <summary>
        /// Core method for finding the path using the A* algorithm.
        /// </summary>
        /// <param name="start">Starting point of the path.</param>
        /// <param name="end">End point of the path.</param>
        /// <returns>A stack representing the path from start to end.</returns>
        public Stack<Vector2> FindPath(Vector2 start, Vector2 end)
        {
            Node startNode = new Node(start);
            Node endNode = new Node(end);
            OpenList.Clear();
            ClosedList.Clear();
            OpenList.Enqueue(startNode, startNode.F);

            while (OpenList.Count > 0)
            {
                // Now we also need to receive the priority out parameter, which is the F cost
                if (OpenList.TryDequeue(out Node currentNode, out float currentPriority))
                {
                    if (currentNode.Position == endNode.Position)
                    {
                        Stack<Vector2> path = new Stack<Vector2>();
                        while (currentNode != null)
                        {
                            path.Push(currentNode.Position * BlockSize);
                            currentNode = currentNode.Parent;
                        }
                        return SmoothPath(path);
                    }

                    ClosedList.Add(currentNode);

                    List<Node> neighbors = GetNeighbors(currentNode);

                    foreach (var neighbor in neighbors)
                    {
                        if (ClosedList.Any(n => n.Position == neighbor.Position) || IsPositionBlocked(neighbor.Position))
                            continue;

                        float newMovementCostToNeighbor = currentNode.G + GetDistance(currentNode, neighbor);
                        if (newMovementCostToNeighbor < neighbor.G || !OpenList.UnorderedItems.Any(n => n.Element.Position == neighbor.Position))
                        {
                            neighbor.G = newMovementCostToNeighbor;
                            neighbor.H = GetDistance(neighbor, endNode);
                            neighbor.F = neighbor.G + neighbor.H;
                            neighbor.Parent = currentNode;

                            if (!OpenList.UnorderedItems.Any(n => n.Element.Position == neighbor.Position))
                                OpenList.Enqueue(neighbor, neighbor.F);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Smooths the path to remove unnecessary detours.
        /// </summary>
        /// <param name="path">The initial path stack to be smoothed.</param>
        /// <returns>A smoothed path stack.</returns>
        private Stack<Vector2> SmoothPath(Stack<Vector2> path)
        {
            if (path == null || path.Count < 3) // A path with less than 3 nodes is already "smooth"
                return path;

            Stack<Vector2> smoothedPath = new Stack<Vector2>();
            Vector2? lastDirection = null;
            Vector2 lastPosition = path.Pop();
            smoothedPath.Push(lastPosition);

            while (path.Count > 0)
            {
                Vector2 nextPosition = path.Pop();
                Vector2 newDirection = Vector2.Normalize(nextPosition - lastPosition);

                if (lastDirection.HasValue && newDirection != lastDirection.Value)
                {
                    smoothedPath.Push(lastPosition);
                }

                lastPosition = nextPosition;
                lastDirection = newDirection;
            }

            // Add the final point
            smoothedPath.Push(lastPosition);

            // Since we popped from the original stack, the order is reversed, we need to reverse it back
            return new Stack<Vector2>(smoothedPath.Reverse());
        }


        /// <summary>
        /// Checks if the pathfinding task is complete.
        /// </summary>
        /// <returns>True if pathfinding is complete, false otherwise.</returns>
        public bool Update()
        {
            return isPathfindingDone;
        }


        /// <summary>
        /// Retrieves the resulting path, if pathfinding is complete.
        /// </summary>
        /// <returns>The path stack if pathfinding is complete; otherwise, null.</returns>
        public Stack<Vector2> GetPath()
        {
            if (!isPathfindingDone) return null;
            return pathResult;
        }

        /// <summary>
        /// Generates a list of neighboring nodes around a given node.
        /// </summary>
        /// <param name="node">The node to find neighbors for.</param>
        /// <returns>A list of neighboring nodes.</returns>
        private List<Node> GetNeighbors(Node node)
        {
            var neighbors = new List<Node>();
            // Define neighbor positions
            var potentialNeighbors = new List<Vector2>
            {
                new Vector2(node.Position.X, node.Position.Y - 1),
                new Vector2(node.Position.X, node.Position.Y + 1),
                new Vector2(node.Position.X - 1, node.Position.Y),
                new Vector2(node.Position.X + 1, node.Position.Y)
            };

            foreach (var pos in potentialNeighbors)
            {
                var worldPosition = pos * BlockSize;
                if (!_blockEntities.Any(block => block.Position == worldPosition))
                {
                    neighbors.Add(new Node(worldPosition));
                }
            }

            return neighbors;
        }

        private bool IsPositionBlocked(Vector2 position)
        {
            return _blockEntities.Any(block => block.Position == position * BlockSize);
        }
        /// <summary>
        /// Calculates the distance between two nodes.
        /// </summary>
        /// <param name="nodeA">The first node.</param>
        /// <param name="nodeB">The second node.</param>
        /// <returns>The distance between the two nodes.</returns>
        private float GetDistance(Node nodeA, Node nodeB)
        {
            return Vector2.Distance(nodeA.Position, nodeB.Position);
        }

    }
}