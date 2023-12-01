using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using System;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PushBackCommand : ICommand
    {
        /* Both collidable entities */
        private readonly ICollidableEntity _entityOne;
        private readonly ICollidableEntity _entityTwo;
        /// <summary>
        /// Construct an instance of the pushbackcommand using two collidable entities
        /// </summary>
        /// <param name="entityOne">The entity to be pushed back</param>
        /// <param name="entityTwo">the entity that is pushing back</param>
        public PushBackCommand(ICollidableEntity entityOne, ICollidableEntity entityTwo)
        {
            _entityOne = entityOne;
            _entityTwo = entityTwo;
        }

        public void Execute()
        {
            // Calculate the intersection of the colliders
            Rectangle intersection = Rectangle.Intersect(_entityOne.Collider.Collider, _entityTwo.Collider.Collider);

            
            Vector2 displacement;
            if (intersection.Width > intersection.Height)
            {
                /* top/bottom collision: Displace along the Y-Axis relative to the vertical 
                 *  positions of the entities involved in the collision
                 */
                displacement = new Vector2(0, intersection.Height * Math.Sign(_entityOne.Position.Y - _entityTwo.Position.Y));
            }
            else
            {
                /* left/right collision: Displace along the X-Axis relative to the vertical positions
                 * of the entities involved in the collision
                 */
                displacement = new Vector2(intersection.Width * Math.Sign(_entityOne.Position.X - _entityTwo.Position.X), 0);
            }

            // Apply the displacement to the entity's position
            _entityOne.Position += displacement;
            _entityOne.Collider.Update(_entityOne);
        }
    }
}
