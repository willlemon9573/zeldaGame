using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.Factories;
using SprintZero1.Sprites;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal class XMLDoorEntity : EntityBase
    {
        private int _destPointX;

        private int _destPointY;

        public int DestPointX { set => _destPointX = value; }
        public int DestPointY { set => _destPointY = value; }

        public override IEntity CreateEntity()
        {
            ISprite doorSprite = TileSpriteFactory.Instance.CreateNewTileSprite(_entityName);
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            Point destinationPoint = new Point(_destPointX, _destPointY);
            //return new LevelDoorEntity(doorSprite, position, destinationPoint);
            /* to be replaced with the above constructor when we get updated colliders */
            return new OpenDoorEntity(doorSprite, position, destinationPoint, Enums.Direction.South);
        }
    }
}
