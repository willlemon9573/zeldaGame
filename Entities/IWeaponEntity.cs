using Microsoft.Xna.Framework;
using SprintZero1.Enums;
using SprintZero1.Sprites;

namespace SprintZero1.Entities
{
    internal interface IWeaponEntity : IEntity
    {
        /// <summary>
        /// Get the weapon damage
        /// </summary>
        public float WeaponDamage { get; }

        public ISprite Sprite { get; }
        /// <summary>
        /// ALlows the entity to use the weapon 
        /// </summary>
        /// <param name="direction">the direction in which the entity is facing</param>
        /// <param name="position">the position of the entity</param>
        void UseWeapon(Direction direction, Vector2 position);
    }
}
