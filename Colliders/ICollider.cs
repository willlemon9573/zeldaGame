using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintZero1.Entities;

namespace SprintZero1.Colliders
{
    internal interface ICollider
    {
        public IEntity Parent { get; set; }

        // The Colliders Rectangle
        public Rectangle Collider { get; set; }

        // Update Collider
        public void Update(GameTime gameTime);


        // Fires each frame a collision occurs with this collider
        // Entity collided with passed through parameter
        public void OnCollision(IEntity collidedEntity, ICollider collidedCollider);

        // Add Collider to Collision Manager
        public void AddCollider();

        // Remove Collider from Collision Manager
        public void RemoveCollider();

    }
}
