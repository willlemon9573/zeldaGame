using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprintZero1.Controllers.EnemyControllers
{
    public class AStarPathfinder
    {
        private PriorityQueue<Node, float> OpenList;
        private List<Node> ClosedList;
        private Task pathfindingTask;
        private Stack<Vector2> pathResult;
        private bool isPathfindingDone;
        private const int BlockSize = 16;


        public AStarPathfinder()
        {
            OpenList = new PriorityQueue<Node, float>();
            ClosedList = new List<Node>();
            isPathfindingDone = false;
            pathResult = null;
        }

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
                        if (ClosedList.Any(n => n.Position == neighbor.Position))
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


        public bool Update()
        {
            return isPathfindingDone;
        }


        public Stack<Vector2> GetPath()
        {
            if (!isPathfindingDone) return null;
            return pathResult;
        }


        private List<Node> GetNeighbors(Node node)
        {
            var neighbors = new List<Node>();
            Vector2 topPosition = new Vector2(node.Position.X, node.Position.Y - 1) * BlockSize;
            neighbors.Add(new Node(topPosition));

            Vector2 bottomPosition = new Vector2(node.Position.X, node.Position.Y + 1) * BlockSize;
            neighbors.Add(new Node(bottomPosition));

            Vector2 leftPosition = new Vector2(node.Position.X - 1, node.Position.Y) * BlockSize;
            neighbors.Add(new Node(leftPosition));

            Vector2 rightPosition = new Vector2(node.Position.X + 1, node.Position.Y) * BlockSize;
            neighbors.Add(new Node(rightPosition));

            return neighbors;
        }


        private float GetDistance(Node nodeA, Node nodeB)
        {
            return Vector2.Distance(nodeA.Position, nodeB.Position);
        }

    }
}