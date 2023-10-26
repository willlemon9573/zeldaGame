using SprintZero1.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Entities
{
    internal interface ICollidableEntity : IEntity
    {
        public ICollider Collider { get;}
    }
}
