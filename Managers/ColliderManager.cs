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

        public static void RemoveCollider(ICollider collider)
        {
            staticColliders.Remove(collider);
            dynamicColliders.Remove(collider);
        }

        /// <summary>
        /// Check each static collider against each dynamic collider and vice versa
        /// </summary>
        public static void CheckStaticAgainstDynamicCollisions()
        {
            /* Compare each static collider against each dynamic collider */
            for (int i = 0; i < staticColliders.Count; i++)
            {
                ICollider staticCollider = staticColliders[i];
                for (int j = 0; j < dynamicColliders.Count; j++)
                {
                    ICollider dynamicCollider = dynamicColliders[j];
                    CollisionsResponseManager.CollisionResponse(staticCollider, dynamicCollider);
                    CollisionsResponseManager.CollisionResponse(dynamicCollider, staticCollider);
                }
            }
        }
        /// <summary>
        /// Compare each dynamic collider against every other dynamic collider and vice versa
        /// </summary>
        public static void CheckDynamicAgainstDynamicCollisions()
        {
            // Compare each dynamic collider against each static collider */
            for (int i = 0; i < dynamicColliders.Count; i++)
            {
                ICollider dynamicCollider1 = dynamicColliders[i];
                for (int j = i + 1; j < dynamicColliders.Count; j++)
                {
                    ICollider dynamicCollider2 = dynamicColliders[j];
                    CollisionsResponseManager.CollisionResponse(dynamicCollider1, dynamicCollider2);
                    CollisionsResponseManager.CollisionResponse(dynamicCollider2, dynamicCollider1);
                }
            }
        }

        public static void Update()
        {
            CheckStaticAgainstDynamicCollisions();
            CheckDynamicAgainstDynamicCollisions();
        }
    }
}