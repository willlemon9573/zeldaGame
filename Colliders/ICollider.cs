using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.Colliders
{
    internal interface ICollider
    { 
        /// <summary>
        /// The factor at which to add or subtract to/from the dimensions of the Collider. Used to keep the Collider centered.
        /// </summary>
        public int Delta { get; set; }

        /// <summary>
        /// The Parent Entity of the Collider.
        /// </summary>
        public IEntity Parent { get; set; }

        /// <summary>
        /// The Rectangle object of the Collider.
        /// </summary>
        public Rectangle Collider { get; set; }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime);

        /// <summary>
        /// Adds collider into the ColliderManager global singleton
        /// </summary>
        public void AddCollider();

        /// <summary>
        /// Removes the collider from the ColliderManager global singleton
        /// </summary>
        public void RemoveCollider();

    }
}
