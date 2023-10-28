using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprintZero1.Controllers.EnemyControllers
{
    public class AStarPathfinder
    {
        private List<Node> OpenList;
        private List<Node> ClosedList;
        private Task pathfindingTask;
        private Stack<Vector2> pathResult;
        private bool isPathfindingDone;

        public AStarPathfinder()
        {
            OpenList = new List<Node>();
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
            OpenList.Add(startNode);

            while (OpenList.Count > 0)
            {

                Node currentNode = OpenList.OrderBy(n => n.F).First();


                if (currentNode.Position == endNode.Position)
                {
                    Stack<Vector2> path = new Stack<Vector2>();
                    while (currentNode != null)
                    {
                        path.Push(currentNode.Position);
                        currentNode = currentNode.Parent;
                    }
                    return path;
                }

                OpenList.Remove(currentNode);
                ClosedList.Add(currentNode);


                List<Node> neighbors = GetNeighbors(currentNode);

                foreach (var neighbor in neighbors)
                {

                    if (ClosedList.Contains(neighbor))
                        continue;


                    float newMovementCostToNeighbor = currentNode.G + GetDistance(currentNode, neighbor);
                    if (newMovementCostToNeighbor < neighbor.G || !OpenList.Contains(neighbor))
                    {

                        neighbor.G = newMovementCostToNeighbor;
                        neighbor.H = GetDistance(neighbor, endNode);
                        neighbor.F = neighbor.G + neighbor.H;
                        neighbor.Parent = currentNode;


                        if (!OpenList.Contains(neighbor))
                            OpenList.Add(neighbor);
                    }
                }
            }
            return null;
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
            Vector2 topPosition = new Vector2(node.Position.X, node.Position.Y - 1);
            neighbors.Add(new Node(topPosition));

            Vector2 bottomPosition = new Vector2(node.Position.X, node.Position.Y + 1);
            neighbors.Add(new Node(bottomPosition));

            Vector2 leftPosition = new Vector2(node.Position.X - 1, node.Position.Y);
            neighbors.Add(new Node(leftPosition));

            Vector2 rightPosition = new Vector2(node.Position.X + 1, node.Position.Y);
            neighbors.Add(new Node(rightPosition));

            return neighbors;
        }


        private float GetDistance(Node nodeA, Node nodeB)
        {
            return Vector2.Distance(nodeA.Position, nodeB.Position);
        }

    }
}
