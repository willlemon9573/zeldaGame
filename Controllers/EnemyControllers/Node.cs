using Microsoft.Xna.Framework;

namespace SprintZero1.Controllers.EnemyControllers
{
    public class Node
    {
        public Node Parent;
        public Vector2 Position;
        public float F;
        public float G;
        public float H;

        public Node(Vector2 position)
        {
            this.Position = position;
        }
    }
}