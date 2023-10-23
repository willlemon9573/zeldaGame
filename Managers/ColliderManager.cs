using Microsoft.Xna.Framework;
using SprintZero1.Managers;
using System.Collections.Generic;

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


        public static void RemoveAllExceptLink()
        {
            int i = 0;
            while (i < staticColliders.Count)
            {

                ColliderManager.RemoveCollider(staticColliders[i]);

            }

           


        }

        public static void RemoveCollider(ICollider collider)
        {
            staticColliders.Remove(collider);
            dynamicColliders.Remove(collider);
        }

        public static void Update(GameTime gameTime)
        {
            foreach (ICollider collider1 in staticColliders)
            {
                foreach (ICollider collider2 in dynamicColliders)
                {
                    if (collider1.Collider.Intersects(collider2.Collider))
                    {
                        CollisionsResponseManager.CollisionResponse(collider1, collider2);
                        CollisionsResponseManager.CollisionResponse(collider2, collider1);
                    }
                }
            }

            foreach (ICollider collider1 in dynamicColliders)
            {
                foreach (ICollider collider2 in dynamicColliders)
                {
                    if (collider1.Collider.Intersects(collider2.Collider))
                    {
                        CollisionsResponseManager.CollisionResponse(collider1, collider2);
                        CollisionsResponseManager.CollisionResponse(collider2, collider1);
                    }
                }
            }
        }
    }
}