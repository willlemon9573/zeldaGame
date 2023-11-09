using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Factories;
using SprintZero1.Sprites;


namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal class XMLWallEntity : EntityBase
    {
        public override IEntity CreateEntity()
        {
            ISprite wallSprite = TileSpriteFactory.Instance.CreateNewTileSprite(_entityName);
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            Rectangle dimensions = TileSpriteFactory.Instance.GetSpriteDimensions(_entityName);
            dimensions.X = (int)position.X;
            dimensions.Y = (int)position.Y;
            return new DungeonWallEntity(wallSprite, position, dimensions);
        }
    }
}
