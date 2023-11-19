using SprintZero1.Entities;
using System.Numerics;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal class XMLEnemyEntity : EntityBase
    {
        private int _entityHealth;

        public int EntityHealth { set => _entityHealth = value; }
        public override IEntity CreateEntity()
        {
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            return new EnemyEntityWithoutDirection(position, _entityHealth, _entityName);
        }

        public IEntity CreateBossEntity()
        {
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            return new EnemyEntityWithoutDirection(position, _entityHealth, _entityName);
        }
    }
}
