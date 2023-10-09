using SprintZero1.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintZero1.Commands;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace SprintZero1.Managers
{
    internal static class CollisionsResponseManager
    {
        public static ICollider _c1;
        public static ICollider _c2;

        private static Dictionary<Tuple<Type, Type>, ICommand> collisionResponseDictionary = new Dictionary<Tuple<Type, Type>, ICommand>()
        {
            { new Tuple<Type, Type>(typeof(PlayerCollider), typeof(LevelBlockCollider) ), new  PushBackCommand() }
        };

        public static void CollisionResponse(ICollider c1, ICollider c2)
        {
            _c1 = c1;
            _c2 = c2;
            Tuple<Type, Type> generic = new Tuple<Type, Type>(c1.GetType(), c2.GetType());
            if (collisionResponseDictionary.ContainsKey(generic))
                collisionResponseDictionary[generic].Execute();
        }
    }
}
