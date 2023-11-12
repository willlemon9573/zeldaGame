using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers.XMLEntityBuilder;

namespace SprintZero1.Factories
{
    internal class XMLWall : EntityBase
    {
        public override IEntity CreateEntity()
        {
            Rectangle dimensions = TileSpriteFactory.Instance.GetSpriteDimensions(_entityName);
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            ISprite doorSprite = TileSpriteFactory.Instance.CreateNewTileSprite(_entityName);
            // return new LevelDoorEntity(doorSprite, position, destinationPoint);
            /* temporary return */
            return new DungeonWallEntity(doorSprite, position, dimensions);
        }
    }
}
