using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Colliders
{
    internal static class ColliderManager
    {
        // Static list of Static colliders
        static List<ICollider> staticColliders = new List<ICollider>();
        // Static list of Dynamic colliders
        static List<ICollider> dynamicColliders = new List<ICollider>();

        // Add Static Collider to List
        public static void AddStaticCollider(ICollider collider)
        {
            staticColliders.Add(collider);
        }

        public static void AddDynamicCollider(ICollider collider)
        {
            dynamicColliders.Add(collider);
        }

        public static void RemoveCollider(ICollider collider) { 
            staticColliders.Remove(collider);
            dynamicColliders.Remove(collider);
        }

        public static void Update(GameTime gameTime)
        {
            foreach (ICollider collider1 in staticColliders)
            {
                foreach (ICollider collider2 in dynamicColliders)
                {
                    if(collider1.Collider.Intersects(collider2.Collider))
                    {
                        collider1.OnCollision(collider2.Parent, collider2);
                        collider2.OnCollision(collider1.Parent, collider1);
                    }
                }
            }

            foreach (ICollider collider1 in dynamicColliders)
            {
                foreach (ICollider collider2 in dynamicColliders)
                {
                    if (collider1.Collider.Intersects(collider2.Collider))
                    {
                        collider1.OnCollision(collider2.Parent, collider2);
                        collider2.OnCollision(collider1.Parent, collider1);
                    }
                }
            }
        }
    }
}
