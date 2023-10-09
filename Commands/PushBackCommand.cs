using SprintZero1.Colliders;
using SprintZero1.Entities;
using SprintZero1.Managers;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Commands
{
    internal class PushBackCommand : ICommand
    {
        public void Execute()
        {
            ICollider c1 = CollisionsResponseManager._c1;
            ICollider c2 = CollisionsResponseManager._c2;
            PriorityQueue<Vector2, float> colliderDistances = new PriorityQueue<Vector2, float>();
            colliderDistances.Enqueue(new Vector2(0, -1), Vector2.Distance(new Vector2(c1.Collider.Center.X, c1.Collider.Center.Y), new Vector2(c2.Collider.Left + (c2.Collider.Width / 2), c2.Collider.Top)));
            colliderDistances.Enqueue(new Vector2(0, 1), Vector2.Distance(new Vector2(c1.Collider.Center.X, c1.Collider.Center.Y), new Vector2(c2.Collider.Left + (c2.Collider.Width / 2), c2.Collider.Bottom)));
            colliderDistances.Enqueue(new Vector2(-1, 0), Vector2.Distance(new Vector2(c1.Collider.Center.X, c1.Collider.Center.Y), new Vector2(c2.Collider.Left, c2.Collider.Top + (c2.Collider.Height / 2))));
            colliderDistances.Enqueue(new Vector2(1, 0), Vector2.Distance(new Vector2(c1.Collider.Center.X, c1.Collider.Center.Y), new Vector2(c2.Collider.Right, c2.Collider.Top + (c2.Collider.Height / 2))));

            Vector2 pos = c1.Parent.Position + colliderDistances.Dequeue();
            // Insert Pushback Code Here
            c1.Parent.Position = pos;
            
        }
    }
}
