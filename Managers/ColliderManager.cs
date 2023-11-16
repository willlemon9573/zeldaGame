using SprintZero1.Entities;
using SprintZero1.Managers;
using System.Collections.Generic;

/*
 * Refactored Colliders to be a single instance and have improved functionality.
 * - Aaron - 11/15/2023
 */
namespace SprintZero1.Colliders
{

    internal class ColliderManager
    {
        // Static list of Static colliders
        readonly List<ICollidableEntity> staticColliderEntities;
        // Static list of Dynamic colliders
        readonly List<ICollidableEntity> dynamicColliderEntities;
        // Handles the responses for collisions
        readonly CollisionsResponseManager _collisionsResponseManager;

        /// <summary>
        /// Construct a new object of collision manager to handle collisions
        /// </summary>
        public ColliderManager()
        {
            staticColliderEntities = new List<ICollidableEntity>();
            dynamicColliderEntities = new List<ICollidableEntity>();
            _collisionsResponseManager = new CollisionsResponseManager();
        }

        /// <summary>
        /// Add a List of Collidable Entities to check for collisions
        /// </summary>
        /// <param name="entities">The list of entities to add to the list</param>
        public void AddCollidableEntities(List<IEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity is ICollidableEntity collidableEntity)
                {
                    if (collidableEntity.Collider is DynamicCollider)
                    {
                        dynamicColliderEntities.Add(collidableEntity);
                    }
                    else
                    {
                        staticColliderEntities.Add(collidableEntity);
                    }
                }
            }
        }

        /// <summary>
        /// Add a single entity to the list of collidable entities
        /// </summary>
        /// <param name="entity">The entity to add to the list</param>
        public void AddCollidableEntity(IEntity entity)
        {
            var collidableEntity = entity as ICollidableEntity;
            if (collidableEntity == null) { return; }

            if (collidableEntity.Collider is DynamicCollider)
            {
                dynamicColliderEntities.Add(collidableEntity);
            }
            else
            {
                staticColliderEntities.Add(collidableEntity);
            }
        }

        /// <summary>
        /// Clears the list of collidable entities.
        /// </summary>
        public void ClearCollidableEntities()
        {
            dynamicColliderEntities.Clear();
            staticColliderEntities.Clear();
        }

        public void RemoveCollidableEntity(IEntity entity)
        {
            var collidableEntity = entity as ICollidableEntity;
            if (collidableEntity == null) { return; }

            if (collidableEntity.Collider is DynamicCollider)
            {
                dynamicColliderEntities.Remove(collidableEntity);
            }
            else
            {
                staticColliderEntities.Remove(collidableEntity);
            }
        }

        /// <summary>
        /// Check each  collider against each dynamic collider and vice versa
        /// </summary>
        public void CheckStaticAgainstDynamicCollisions()
        {
            /* Compare each  collider against each dynamic collider */
            for (int i = 0; i < staticColliderEntities.Count; i++)
            {
                ICollidableEntity staticColliderEntity = staticColliderEntities[i];
                for (int j = 0; j < dynamicColliderEntities.Count; j++)
                {
                    ICollidableEntity dynamicColliderEntity = dynamicColliderEntities[j];
                    if (dynamicColliderEntity.Collider.Collider.Intersects(staticColliderEntity.Collider.Collider))
                    {
                        _collisionsResponseManager.CollisionResponse(staticColliderEntity, dynamicColliderEntity);
                        _collisionsResponseManager.CollisionResponse(dynamicColliderEntity, staticColliderEntity);
                    }
                }
            }
        }

        /// <summary>
        /// Compare each dynamic collider against every other dynamic collider and vice versa
        /// </summary>
        public void CheckDynamicAgainstDynamicCollisions()
        {
            // Compare each dynamic collider against each  collider */
            for (int i = 0; i < dynamicColliderEntities.Count; i++)
            {
                ICollidableEntity dynamicCollider1 = dynamicColliderEntities[i];
                for (int j = i + 1; j < dynamicColliderEntities.Count; j++)
                {
                    ICollidableEntity dynamicCollider2 = dynamicColliderEntities[j];
                    if (dynamicCollider1.Collider.Collider.Intersects(dynamicCollider2.Collider.Collider))
                    {
                        _collisionsResponseManager.CollisionResponse(dynamicCollider1, dynamicCollider2);
                        _collisionsResponseManager.CollisionResponse(dynamicCollider2, dynamicCollider1);
                    }
                }
            }
        }

        /// <summary>
        /// Checks Collisions on all types and fires collisions from Collisions Response
        /// </summary>
        /// <param name="entities"> List of ICollidableEntities</param>
        public void CheckCollisions()
        {
            CheckStaticAgainstDynamicCollisions();
            CheckDynamicAgainstDynamicCollisions();
        }
    }
}