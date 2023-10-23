using Microsoft.Xna.Framework;
using SprintZero1.Enums;

namespace SprintZero1.Entities
{
    internal interface IWeaponEntity : IEntity
    {
        /// <summary>
        /// ALlows the entity to use the weapon 
        /// </summary>
        /// <param name="direction">the direction in which the entity is facing</param>
        /// <param name="position">the position of the entity</param>
        void UseWeapon(Direction direction, Vector2 position);
    }
}
