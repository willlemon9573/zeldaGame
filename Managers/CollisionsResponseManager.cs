using SprintZero1.Colliders;
using SprintZero1.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SprintZero1.Managers
{
    internal static class CollisionsResponseManager
    {

        private static Dictionary<Tuple<Type, Type>, ICommand> collisionResponseDictionary = new Dictionary<Tuple<Type, Type>, ICommand>()
        {
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(LevelBlockCollider) ), new PushBackCommand(null, null) }
        };

        public static void CollisionResponse(ICollider c1, ICollider c2)
        {

            Tuple<Type, Type> generic = new Tuple<Type, Type>(c1.GetType(), c2.GetType());
            if (collisionResponseDictionary.ContainsKey(generic))
            {
                ConstructorInfo constructorInfoObj = collisionResponseDictionary[generic].GetType().GetConstructor(new[] { typeof(ICollider), typeof(ICollider) });
                constructorInfoObj.Invoke(new object[] { c1, c2 });
            }
        }
    }
}
