using Microsoft.Xna.Framework;
using SprintZero1.Colliders;
using SprintZero1.Entities;
using System.Collections.Generic;

namespace SprintZero1.Commands
{
    internal class PushBackCommand : ICommand
    {
        ICollidableEntity e1;
        ICollidableEntity e2;

        public PushBackCommand(ICollidableEntity c1, ICollidableEntity c2)
        {
            this.e1 = c1;
            this.e2 = c2;
            if (c1 != null && c2 != null)
                Execute();
        }

        public void Execute()
        {
            PriorityQueue<Vector2, float> colliderDistances = new PriorityQueue<Vector2, float>();
            Rectangle intersection = Rectangle.Intersect(e1.Collider.Collider, e2.Collider.Collider);
            if(intersection.Width > intersection.Height)
            {
                colliderDistances.Enqueue(new Vector2(0, -1), System.Math.Abs(intersection.Center.Y - e2.Collider.Collider.Top));
                colliderDistances.Enqueue(new Vector2(0, 1), System.Math.Abs(intersection.Center.Y - e2.Collider.Collider.Bottom));
            }
            else
            {
                colliderDistances.Enqueue(new Vector2(1,0), System.Math.Abs(intersection.Center.X - e2.Collider.Collider.Right));
                colliderDistances.Enqueue(new Vector2(-1, 0), System.Math.Abs(intersection.Center.X - e2.Collider.Collider.Left));
            }
            // Insert Pushback Code Here
            e1.Position += colliderDistances.Dequeue();
            e1.Collider.Update(null);
        }
    }
}
