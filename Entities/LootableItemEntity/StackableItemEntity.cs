using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using System;

namespace SprintZero1.Entities.LootableItemEntity
{
    internal class StackableItemEntity : LootableItemBase
    {
        public StackableItemEntity(ISprite entitySprite, Vector2 position, object ) : base(entitySprite, position, )
        {
        }

        public override void Pickup(IEntity Player, int amt = 0)
        {
            throw new NotImplementedException();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
