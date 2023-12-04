using Microsoft.Xna.Framework;
using SprintZero1.Entities.DungeonRoomEntities;
using SprintZero1.Entities.EntityInterfaces;
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
            return new DungeonWallEntity(wallSprite, position);
        }
    }
}
