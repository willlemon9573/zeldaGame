using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Colliders
{
    internal interface ICollisionResponse
    {
        public void Execute(ICollider collider1, ICollider collider2);
    }
}
