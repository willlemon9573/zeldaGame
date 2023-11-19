using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers.XMLEntityBuilder;

namespace SprintZero1.XMLFiles.FactoryXMLFiles
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
