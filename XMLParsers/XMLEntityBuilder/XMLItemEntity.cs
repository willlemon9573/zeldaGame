using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Factories;
using SprintZero1.Sprites;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal class XMLItemEntity : EntityBase
    {
        int _itemFrames;

        public int ItemFrames { set => _itemFrames = value; }
        /// <summary>
        /// Create the item entity
        /// </summary>
        /// <returns>a new instance of the item entity</returns>
        public override IEntity CreateEntity()
        {
            ISprite itemSprite = ItemSpriteFactory.Instance.CreateNonAnimatedItemSprite(_entityName);
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            // using levelBlockEntity until we make something ofr items
            return new LevelBlockEntity(itemSprite, position);
        }

        public IEntity CreateAnimatedEntity()
        {
            ISprite itemSprite = ItemSpriteFactory.Instance.CreateAnimatedItemSprite(_entityName, _itemFrames);
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            Rectangle dimensions = ItemSpriteFactory.Instance.GetAnimatedSpriteDimensions(_entityName);
            // using levelBlockEntity until we make something ofr items
            return new FireTrapEntity(itemSprite, position, dimensions);
        }
    }
}
