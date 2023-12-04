using SprintZero1.Colliders;

namespace SprintZero1.Entities.EntityInterfaces
{
    internal interface ICollidableEntity : IEntity
    {
        public ICollider Collider { get; }
    }
}
