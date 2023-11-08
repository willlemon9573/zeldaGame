using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Threading;


namespace SprintZero1.Controllers.EnemyControllers
{

    public class Node : IComparable<Node>
    {
        public Node Parent;
        public Vector2 Position;
        public float F;
        public float G;
        public float H;
        private static int nextId;
        public int Id { get; private set; }
        private const int BlockSize = 16;

        public Node(Vector2 position)
        {
            Position = new Vector2((int)position.X / BlockSize, (int)position.Y / BlockSize);
            Id = Interlocked.Increment(ref nextId);
        }

        public int CompareTo(Node other)
        {
            int compare = F.CompareTo(other.F);
            if (compare == 0)
            {
                compare = Id.CompareTo(other.Id);
            }
            return compare;
        }
    }
}