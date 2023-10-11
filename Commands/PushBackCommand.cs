using SprintZero1.Colliders;
using SprintZero1.Entities;
using SprintZero1.Managers;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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
