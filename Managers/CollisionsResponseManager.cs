using SprintZero1.Commands;
using SprintZero1.Commands.CollisionCommands;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SprintZero1.Managers
{
    internal static class CollisionsResponseManager
    {
        private static readonly Dictionary<Tuple<Type, Type>, ICommand> collisionResponseDictionary = new Dictionary<Tuple<Type, Type>, ICommand>()
        {
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(LevelBlockEntity) ), new PushBackCommand(null, null) },
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(DungeonWallEntity) ), new PushBackCommand(null, null) },
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(FireTrapEntity) ), new PushBackCommand(null, null) },
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(OpenDoorEntity) ), new EnterNextLevelCommand(null, null) },
            { new Tuple<Type, Type>(typeof(SwordEntity), typeof(EnemyEntityWithDirection)), new DestroyEntity(null, null) }
        };

        public static void CollisionResponse(ICollidableEntity e1, ICollidableEntity e2)
        {
            Tuple<Type, Type> generic = new Tuple<Type, Type>(e1.GetType(), e2.GetType());
            if (collisionResponseDictionary.ContainsKey(generic))
            {
                ConstructorInfo constructorInfoObj = collisionResponseDictionary[generic].GetType().GetConstructor(new[] { generic.Item1, generic.Item2 });
                ICommand collisionCommand = (ICommand)constructorInfoObj.Invoke(new object[] { e1, e2 });
                collisionCommand.Execute();
            }
        }
    }
}
