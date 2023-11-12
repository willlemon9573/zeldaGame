using SprintZero1.Entities;
using SprintZero1.Managers;
using System.Collections.Generic;

namespace SprintZero1.Colliders
{
    internal static class ColliderManager
    {
        // Static list of Static colliders
        static List<ICollidableEntity> staticColliderEntities = new List<ICollidableEntity>();
        // Static list of Dynamic colliders
        static List<ICollidableEntity> dynamicColliderEntities = new List<ICollidableEntity>();

        public static void Reset()
        {
            staticColliderEntities.Clear();
            dynamicColliderEntities.Clear();
        }

        // Add Static Collider to List
        public static void AddStaticCollider(ICollidableEntity entity)
        {
            staticColliderEntities.Add(entity);
        }

        public static void AddDynamicCollider(ICollidableEntity entity)
        {
            dynamicColliderEntities.Add(entity);
        }

        public static void RemoveAllExcept(ICollidableEntity entity)
        {
            staticColliderEntities.Clear();
            dynamicColliderEntities.Clear();
            dynamicColliderEntities.Add(entity);
        }

        public static void RemoveCollider(ICollidableEntity entity)
        {
            staticColliderEntities.Remove(entity);
            dynamicColliderEntities.Remove(entity);
        }

        /// <summary>
        /// Check each static collider against each dynamic collider and vice versa
        /// </summary>
        public static void CheckStaticAgainstDynamicCollisions()
        {
            /* Compare each static collider against each dynamic collider */
            for (int i = 0; i < staticColliderEntities.Count; i++)
            {
                ICollidableEntity staticColliderEntity = staticColliderEntities[i];
                for (int j = 0; j < dynamicColliderEntities.Count; j++)
                {
                    ICollidableEntity dynamicColliderEntity = dynamicColliderEntities[j];
                    if (dynamicColliderEntity.Collider.Collider.Intersects(staticColliderEntity.Collider.Collider))
                    {
                        CollisionsResponseManager.CollisionResponse(staticColliderEntity, dynamicColliderEntity);
                        CollisionsResponseManager.CollisionResponse(dynamicColliderEntity, staticColliderEntity);
                    }
                }
            }
        }

        /// <summary>
        /// Compare each dynamic collider against every other dynamic collider and vice versa
        /// </summary>
        public static void CheckDynamicAgainstDynamicCollisions()
        {
            // Compare each dynamic collider against each static collider */
            for (int i = 0; i < dynamicColliderEntities.Count; i++)
            {
                ICollidableEntity dynamicCollider1 = dynamicColliderEntities[i];
                for (int j = i + 1; j < dynamicColliderEntities.Count; j++)
                {

                    ICollidableEntity dynamicCollider2 = dynamicColliderEntities[j];
                    if (dynamicCollider1.Collider.Collider.Intersects(dynamicCollider2.Collider.Collider))
                    {
                        CollisionsResponseManager.CollisionResponse(dynamicCollider1, dynamicCollider2);
                        CollisionsResponseManager.CollisionResponse(dynamicCollider2, dynamicCollider1);
                    }
                }
            }
        }

        /// <summary>
        /// Checks Collisions on all types and fires collisions from Collisions Response
        /// </summary>
        /// <param name="entities"> List of ICollidableEntities</param>
        public static void CheckCollisions(List<ICollidableEntity> entities)
        {
            ParseColliders(entities);
            CheckStaticAgainstDynamicCollisions();
            CheckDynamicAgainstDynamicCollisions();
            staticColliderEntities.Clear();
            dynamicColliderEntities.Clear();
        }

        /// <summary>
        /// Private method. Used to parse Collider type
        /// </summary>
        /// <param name="entities">List of ICollidbleEntity's</param>
        private static void ParseColliders(List<ICollidableEntity> entities)
        {
            foreach (ICollidableEntity entity in entities)
            {
                switch (entity.Collider)
                {
                    case StaticCollider:
                        staticColliderEntities.Add(entity);
                        break;
                    case DynamicCollider:
                        dynamicColliderEntities.Add(entity);
                        break;
                }
            }
        }
    }
}