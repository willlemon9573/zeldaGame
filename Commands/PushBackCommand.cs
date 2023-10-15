using Microsoft.Xna.Framework;
using SprintZero1.Colliders;
using System.Collections.Generic;

namespace SprintZero1.Commands
{
    internal class PushBackCommand : ICommand
    {
        ICollider c1;
        ICollider c2;

        public PushBackCommand(ICollider c1, ICollider c2)
        {
            this.c1 = c1;
            this.c2 = c2;
            if (c1 != null && c2 != null)
                Execute();
        }

        public void Execute()
        {
            PriorityQueue<Vector2, float> colliderDistances = new PriorityQueue<Vector2, float>();
            Rectangle intersection = Rectangle.Intersect(c1.Collider, c2.Collider);
            if(intersection.Width > intersection.Height)
            {
                colliderDistances.Enqueue(new Vector2(0, -1), System.Math.Abs(intersection.Center.Y - c2.Collider.Top));
                colliderDistances.Enqueue(new Vector2(0, 1), System.Math.Abs(intersection.Center.Y - c2.Collider.Bottom));
            }
            else
            {
                colliderDistances.Enqueue(new Vector2(1,0), System.Math.Abs(intersection.Center.X - c2.Collider.Right));
                colliderDistances.Enqueue(new Vector2(-1, 0), System.Math.Abs(intersection.Center.X - c2.Collider.Left));
            }
            // Insert Pushback Code Here
            c1.Parent.Position += colliderDistances.Dequeue();
            c1.Update(null);
        }
    }
}
