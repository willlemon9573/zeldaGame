using Microsoft.Xna.Framework;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.LootableItemEntity
{
    internal class StackableLootableItem : LootableItemBase
    {
        public StackableLootableItem(ISprite entitySprite, Vector2 position) : base(entitySprite, position)
        {
        }

        public override void Pickup(IEntity Player, int amt = 0)
        {
            throw new System.NotImplementedException();
        }

        public override void Remove()
        {
            throw new System.NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
