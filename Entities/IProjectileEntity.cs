using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Sprites;
using SprintZero1.Enums;
using SprintZero1.projectile;

namespace SprintZero1.Entities
{
    internal interface IProjectileEntity : IEntity // Here, IProjectileEntity extends IEntity
    {
        Direction Direction { get; set; }
        float Rotation { get; set; }
        SpriteEffects _ChangeSpriteEffects { get; set; }
        ISprite ProjectileSprite { get; set; }
        IProjectile ProjectileUpdate { get; set; }
        ISprite EndingSprite { get; set; }
    }
}
