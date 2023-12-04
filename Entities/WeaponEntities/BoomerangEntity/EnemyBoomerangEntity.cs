using SprintZero1.Colliders.ItemColliders;
using SprintZero1.Entities.EntityInterfaces;

namespace SprintZero1.Entities.WeaponEntities.BoomerangEntity
{
    /// <summary>
    /// Derived class off Regular Boomerang entity to distinguish what entity is throwing which boomerang
    /// </summary>
    internal class EnemyBoomerangEntity : RegularBoomerangEntity
    {
        private const float DefaultDamage = 1f;
        public EnemyBoomerangEntity(string weaponName, IMovableEntity player) : base(weaponName, player)
        {
            _weaponDamage = DefaultDamage; // default damage for enemy boomerang hitting link
        }

        protected override void SetCollider()
        {
            _projectileCollider = new EnemyBoomerangCollider(_weaponPosition, new System.Drawing.Size(_weaponSprite.Width, _weaponSprite.Height));
        }
    }
}
