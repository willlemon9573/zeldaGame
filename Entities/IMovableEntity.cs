using Microsoft.Xna.Framework;
using SprintZero1.Enums;
using SprintZero1.StatePatterns.StatePatternInterfaces;

namespace SprintZero1.Entities
{
    internal interface IMovableEntity : IEntity
    {
        /// <summary>
        /// Gets the Direction of the Entity
        /// </summary>
        Direction Direction { get; }

        /// <summary>
        /// Get and set the entity's state
        /// </summary>
        IEntityState State { get; set; }
        /// <summary>
        /// Changes the Direction of the Entity
        /// </summary>
        /// <param name="newDirection">The new direction the entity is facing</param>
        void ChangeDirection(Direction newDirection);
        /// <summary>
        /// Move The entity in its current direction
        /// </summary>
        /// <param name="distance">The distance the entity will go</param>
        void Move(Vector2 distance);
    }
}
