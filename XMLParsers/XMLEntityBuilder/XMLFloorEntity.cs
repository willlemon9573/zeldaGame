using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Factories;
using SprintZero1.Sprites;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal class XMLFloorEntity : EntityBase
    {
        private int _originPointX;
        private int _originPointY;

        public int OriginPointX { set => _originPointX = value; }
        public int OriginPointY { set => _originPointY = value; }

        public override IEntity CreateEntity()
        {
            ISprite floorSprite = TileSpriteFactory.Instance.CreateFloorSprite(_entityName);
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            return new BackgroundSpriteEntity(floorSprite, position);
        }
        /// <summary>
        /// Get the origin of the floor
        /// </summary>
        /// <returns></returns>
        public Point GetOrigin()
        {
            return new Point(_originPointX, _originPointY);
        }
        /// <summary>
        /// Temprorary until we get point system set up. Returns the name of the floor
        /// </summary>
        /// <returns>The name of the floor</returns>
        public string GetName()
        {
            return _entityName;
        }
    }
}
