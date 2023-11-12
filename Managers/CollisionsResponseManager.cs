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
        /* private static readonly Dictionary<Tuple<Type, Type>, ICommand> collisionResponseDictionary = new Dictionary<Tuple<Type, Type>, ICommand>()
         {
             { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(LevelBlockEntity) ), new PushBackCommand(null, null) },
             { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(DungeonWallEntity) ), new PushBackCommand(null, null) },
             { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(FireTrapEntity) ), new PushBackCommand(null, null) },
             { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(OpenDoorEntity) ), new EnterNextLevelCommand(null, null) },
             { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(LockedDoorEntity) ), new UnlockDoorCommand(null, null) },
             { new Tuple<Type, Type>(typeof(SwordEntity), typeof(EnemyEntityWithDirection)), new DestroyEntity(null, null) }
         };*/

        private static readonly Dictionary<Tuple<Type, Type>, Action<ICollidableEntity, ICollidableEntity>> colliderDict = new Dictionary<Tuple<Type, Type>, Action<ICollidableEntity, ICollidableEntity>>()
        {
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(LevelBlockEntity)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(DungeonWallEntity)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(FireTrapEntity)), (entity1, entity2) => new PushBackCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(OpenDoorEntity)), (entity1, entity2) => new EnterNextLevelCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(LockedDoorEntity)), (entity1, entity2) => new UnlockDoorCommand(entity1, entity2).Execute() },
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(EnemyEntityWithDirection)), (entity1, entity2) => new DestroyEntity(entity1, entity2).Execute() },
        };

        public static void CollisionResponse(ICollidableEntity e1, ICollidableEntity e2)
        {
            Tuple<Type, Type> generic = new Tuple<Type, Type>(e1.GetType(), e2.GetType());

            /*if (collisionResponseDictionary.ContainsKey(generic))
            {
                Debug.WriteLine($"{collisionResponseDictionary[generic]}");
                ConstructorInfo constructorInfoObj = collisionResponseDictionary[generic].GetType().GetConstructor(new[] { generic.Item1, generic.Item2 });
                ICommand collisionCommand = (ICommand)constructorInfoObj.Invoke(new object[] { e1, e2 });
                collisionCommand.Execute();
            }*/

            if (colliderDict.TryGetValue(generic, out var collider))
            {
                collider(e1, e2);
            }
        }
    }
}
