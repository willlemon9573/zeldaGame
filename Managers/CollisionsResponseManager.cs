using SprintZero1.Colliders;
using SprintZero1.Colliders.DoorColliders;
using SprintZero1.Commands.CollisionCommands;
using SprintZero1.Entities;
using System;
using System.Collections.Generic;

namespace SprintZero1.Managers
{
    internal static class CollisionsResponseManager
    {
        private static readonly Dictionary<Tuple<Type, Type>, Action<ICollidableEntity, ICollidableEntity>> colliderDict = new Dictionary<Tuple<Type, Type>, Action<ICollidableEntity, ICollidableEntity>>()
        {
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(OpenDoorCollider)), (entity1, entity2) => new EnterNextRoomCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(LockedDoorCollider)), (entity1, entity2) => new UnlockDoorCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(LootableItemCollider)), (entity1, entity2) => new PickUpStackableItemCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(PushBackCollider)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },

        };

        private static readonly Dictionary<Tuple<Type, Type>, Action<ICollidableEntity, ICollidableEntity>> collisionResponseDictionary = new Dictionary<Tuple<Type, Type>, Action<ICollidableEntity, ICollidableEntity>>();

        public static void CollisionResponse(ICollidableEntity e1, ICollidableEntity e2)
        {
            /*New Implementation is now lazy implementation similar to a flyweight pattern where
            * each command is created only when it is needed, and not repeatedly created and executed
             *-Aaron*/


            Tuple<Type, Type> generic = new Tuple<Type, Type>(e1.Collider.GetType(), e2.Collider.GetType());
            /*Check if the collider contains the following collision happening*/
            if (!collisionResponseDictionary.ContainsKey(generic) && colliderDict.TryGetValue(generic, out var command))
            {

                collisionResponseDictionary.Add(generic, command);
            }
            /*Execute the command*/
            if (collisionResponseDictionary.TryGetValue(generic, out var collision))
            {
                collision(e1, e2);
            }
        }
    }
}
