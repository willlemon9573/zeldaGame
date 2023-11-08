using System.Collections.Generic;

namespace SprintZero1.Controllers.EnemyControllers
{
    public class NodeComparer : IComparer<Node>
    {
        public int Compare(Node x, Node y)
        {
            return x.CompareTo(y);
        }
    }
}