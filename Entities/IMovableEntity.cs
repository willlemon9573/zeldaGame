using Microsoft.Xna.Framework;
using SprintZero1.Enums;

namespace SprintZero1.Entities
{
    internal interface IMovableEntity
    {
        /// <summary>
        /// Gets the Direction of the Entity
        /// </summary>
        Direction Direction { get; }
        /// <summary>
        /// Changes the Direction of the Entity
        /// </summary>
        /// <param name="newDirection">The new direction the entity is facing</param>
        void ChangeDirection(Direction newDirection);
        void Move(Vector2 distance);
    }
}
