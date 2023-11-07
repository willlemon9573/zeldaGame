using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Factories;
using SprintZero1.Sprites;


namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal class XMLWall : EntityBase
    {
        public override IEntity CreateEntity()
        {
            ISprite wallSprite = TileSpriteFactory.Instance.CreateNewTileSprite(_entityName);
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            Rectangle dimensions = TileSpriteFactory.Instance.GetSpriteDimensions(_entityName);
            return new DungeonWallEntity(wallSprite, position, dimensions);
        }
    }
}
