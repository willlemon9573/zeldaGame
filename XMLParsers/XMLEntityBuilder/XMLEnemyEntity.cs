using SprintZero1.Entities;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System.Numerics;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal class XMLEnemyEntity : EntityBase
    {
        private int _entityHealth;
        private int _spriteFrames;

        public int EntityHealth { set => _entityHealth = value; }
        public int EntityFrames { set => _spriteFrames = value; }
        public override IEntity CreateEntity()
        {
            ISprite entitySprite = EnemySpriteFactory.Instance.CreateAnimatedEnemySpriteDirectionless(_entityName, _spriteFrames);
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            return new EnemyEntityWithoutDirection(entitySprite, position, _entityHealth, _entityName);
        }

        public IEntity CreateBossEntity()
        {
            ISprite bossSprite = EnemySpriteFactory.Instance.CreateBossSprite(_entityName);
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            return new EnemyEntityWithoutDirection(bossSprite, position, _entityHealth, _entityName);
        }
    }
}
