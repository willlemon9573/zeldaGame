using SprintZero1.Enums;

namespace SprintZero1.Entities.EntityInterfaces
{
    internal interface IMovableEntity : IEntity
    {
        /// <summary>
        /// Gets the Direction of the Entity
        /// </summary>
        Direction Direction { get; set; }

        /// <summary>
        /// Changes the Direction of the Entity
        /// </summary>
        /// <param name="newDirection">The new direction the entity is facing</param>
        void ChangeDirection(Direction newDirection);
        /// <summary>
        /// Move The entity in its current direction
        /// </summary>
        void Move();
    }
}
