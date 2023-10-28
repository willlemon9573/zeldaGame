using SprintZero1.Colliders;
using SprintZero1.Commands;
using SprintZero1.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SprintZero1.Managers
{
    internal static class CollisionsResponseManager
    {
#pragma warning disable IDE0090 // Use 'new(...)'
        private static readonly Dictionary<Tuple<Type, Type>, ICommand> collisionResponseDictionary = new Dictionary<Tuple<Type, Type>, ICommand>()
#pragma warning restore IDE0090 // Use 'new(...)'
        {
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(LevelBlockEntity) ), new PushBackCommand(null, null) },
            { new Tuple<Type, Type>(typeof(PlayerEntity), typeof(LevelDoorEntity) ), new EnterNextLevelCommand(null, null) },
            { new Tuple<Type, Type>(typeof(SwordEntity), typeof(EnemyEntityWithDirection)), new DestroyEntity(null, null) }
        };

        public static void CollisionResponse(ICollidableEntity e1, ICollidableEntity e2)
        {
#pragma warning disable IDE0090 // Use 'new(...)'
            Tuple<Type, Type> generic = new Tuple<Type, Type>(e1.GetType(), e2.GetType());
#pragma warning restore IDE0090 // Use 'new(...)'
            if (collisionResponseDictionary.ContainsKey(generic))
            {
                ConstructorInfo constructorInfoObj = collisionResponseDictionary[generic].GetType().GetConstructor(new[] { generic.Item1, generic.Item2 });
                constructorInfoObj.Invoke(new object[] { e1, e2 });
            }
        }
    }
}
