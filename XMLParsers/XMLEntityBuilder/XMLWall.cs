using SprintZero1.Entities;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System.Numerics;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal class XMLWall : EntityBase
    {
        public override IEntity CreateEntity()
        {
            ISprite wallSprite = TileSpriteFactory.Instance.CreateNewTileSprite(_entityName);
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            return new BackgroundSpriteEntity(wallSprite, position);
        }
    }
}
