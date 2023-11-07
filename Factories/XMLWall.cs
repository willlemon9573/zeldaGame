using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers.XMLEntityBuilder;

namespace SprintZero1.Factories
{
    internal class XMLWall : EntityBase
    {
        private int _destinationPointX;
        private int _destinationPointY;

        public int DestinationPointX { set => _destinationPointX = value; }
        public int DestinationPointY { set => _destinationPointY = value; }

        public override IEntity CreateEntity()
        {
            Point destinationPoint = new Point(_destinationPointX, _destinationPointY);
            Vector2 position = new Vector2(_destinationPointX, _destinationPointY);
            ISprite doorSprite = TileSpriteFactory.Instance.CreateNewTileSprite(_entityName);
            // return new LevelDoorEntity(doorSprite, position, destinationPoint);
            /* temporary return */
            return new LevelDoorEntity(doorSprite, position, 0);
        }
    }
}
