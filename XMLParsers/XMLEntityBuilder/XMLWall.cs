using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities;
using SprintZero1.Factories;
using SprintZero1.Sprites;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal class XMLWall : EntityBase
    {
        public override IEntity CreateEntity()
        {
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            ISprite doorSprite = TileSpriteFactory.Instance.CreateNewTileSprite(_entityName);
            return new DungeonWallEntity(doorSprite, position);
        }
    }
}
