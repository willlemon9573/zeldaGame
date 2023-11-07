using SprintZero1.Entities;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System.Numerics;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal class XMLItemEntity : EntityBase
    {
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
    }
}
