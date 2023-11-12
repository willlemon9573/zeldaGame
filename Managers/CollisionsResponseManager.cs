using SprintZero1.Commands.CollisionCommands;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using System;
using System.Collections.Generic;

namespace SprintZero1.Managers
{
    internal static class CollisionsResponseManager
    {
        private static readonly Dictionary<Tuple<Type, Type>, Action<ICollidableEntity, ICollidableEntity>> colliderDict = new Dictionary<Tuple<Type, Type>, Action<ICollidableEntity, ICollidableEntity>>()
        {
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(LevelBlockEntity)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute()},
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(DungeonWallEntity)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(FireTrapEntity)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(OpenDoorEntity)), (entity1, entity2) => new EnterNextLevelCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(LockedDoorEntity)), (entity1, entity2) => new UnlockDoorCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(EnemyEntityWithDirection)), (entity1, entity2) => new DestroyEntity(entity1, entity2).Execute() },
        };

        private static readonly Dictionary<Tuple<Type, Type>, Action<ICollidableEntity, ICollidableEntity>> collisionResponseDictionary = new Dictionary<Tuple<Type, Type>, Action<ICollidableEntity, ICollidableEntity>>();

        public static void CollisionResponse(ICollidableEntity e1, ICollidableEntity e2)
        {
            /* New Implementation is now lazy implementation similar to a flyweight pattern where 
             * each command is created only when it is needed, and not repeatedly created and executed 
             * - Aaron
             */
            Tuple<Type, Type> generic = new Tuple<Type, Type>(e1.GetType(), e2.GetType());
            /* Check if the collider contains the following collision happening */
            if (!collisionResponseDictionary.ContainsKey(generic) && colliderDict.TryGetValue(generic, out var command))
            {
                collisionResponseDictionary.Add(generic, command);
            }
            /* Execute the command */
            if (collisionResponseDictionary.TryGetValue(generic, out var collision))
            {
                collision(e1, e2);
            }
        }
    }
}
