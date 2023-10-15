using SprintZero1.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintZero1.Commands;
using System.Security.Cryptography;

namespace SprintZero1.Entities
{
    internal static class CollisionsResponse
    {
        private static Dictionary<Type, ICommand> collisionResponseDictionary = new Dictionary<Type, ICommand>()
        {
            { typeof((StaticCollider, StaticCollider)), new TestCommand() }
        };

        public static void CollisionResponse(ICollider c1, ICollider c2)
        {
            Type type = (c1, c2).GetType();
            collisionResponseDictionary[type].Execute();
        }
    }
}
