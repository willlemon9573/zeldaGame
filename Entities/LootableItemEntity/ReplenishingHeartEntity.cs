using Microsoft.Xna.Framework;
using SprintZero1.LevelFiles;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.LootableItemEntity
{
    internal class ReplenishingHeartEntity : LootableItemBase
    {
        /// <summary>
        /// Container class for a Triforce Entity
        /// </summary>
        /// <param name="entitySprite">The sprite of the entity</param>
        /// <param name="position">The position of the entity</param>
        /// <param name="removeDelegate">The delegate for removing the entity</param>
        public ReplenishingHeartEntity(ISprite entitySprite, Vector2 position, RemoveDelegate removeDelegate) : base(entitySprite, position, removeDelegate)
        {
        }
    }
}
