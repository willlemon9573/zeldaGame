using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities.EnemyEntities;
using SprintZero1.Enums;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.BossStatePatterns.AquamentusStatePattern
{

    internal class AquamentusMovingState : BaseBossState
    {
        private Vector2 _movingDirection;
        private const float DefaultMoveSpeed = 15f;
        private readonly Dictionary<Direction, Vector2> _velocityMap;
        private readonly Color defaultColor;

        public AquamentusMovingState(BaseBossEntity boss) : base(boss)
        {
            _velocityMap = new Dictionary<Direction, Vector2>()
           {
                {Direction.North, new Vector2(0, -DefaultMoveSpeed) },
                {Direction.South, new Vector2(0, DefaultMoveSpeed) },
                {Direction.East, new Vector2(DefaultMoveSpeed, 0) },
                {Direction.West, new Vector2(-DefaultMoveSpeed, 0) }
           };
            defaultColor = Color.White;
        }

        public override void Request()
        {
            if (_canTransition == false || _boss.Health <= 0) { return; }
            _movingDirection = _velocityMap[_boss.Direction];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _boss.Sprite.Draw(spriteBatch, _boss.Position, defaultColor); // aquamentus should only be facing left
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _boss.Position += (_movingDirection * deltaTime);
            _boss.Sprite.Update(gameTime);
        }
    }
}
