using SprintZero1.Colliders;
using SprintZero1.Commands;
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
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(LevelBlockCollider) ), new PushBackCommand(null, null) },
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(LevelDoorCollider) ), new EnterNextLevelCommand(null, null) }
        };

        public static void CollisionResponse(ICollider c1, ICollider c2)
        {
#pragma warning disable IDE0090 // Use 'new(...)'
            Tuple<Type, Type> generic = new Tuple<Type, Type>(c1.GetType(), c2.GetType());
#pragma warning restore IDE0090 // Use 'new(...)'
            if (collisionResponseDictionary.ContainsKey(generic))
            {
                ConstructorInfo constructorInfoObj = collisionResponseDictionary[generic].GetType().GetConstructor(new[] { generic.Item1, generic.Item2 });
                constructorInfoObj.Invoke(new object[] { c1, c2 });
            }
        }
    }
}
