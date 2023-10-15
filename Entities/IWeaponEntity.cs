using Microsoft.Xna.Framework;
using SprintZero1.Enums;

namespace SprintZero1.Entities
{
    internal interface IWeaponEntity
    {
        /// <summary>
        /// Function to allow an entity to use the specific weapon
        /// </summary>
        /// <param name="direction">The direction the entity is facing</param>
        /// <param name="position">The position of the entity</param>
        void UseWeapon(Direction direction, Vector2 position);
    }
}
