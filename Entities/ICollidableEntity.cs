﻿using SprintZero1.Colliders;

namespace SprintZero1.Entities
{
    internal interface ICollidableEntity : IEntity
    {
        public ICollider Collider { get; }
    }
}
