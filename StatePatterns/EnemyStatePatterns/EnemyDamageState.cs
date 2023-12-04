using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities.EnemyEntities;
using SprintZero1.Enums;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.EnemyStatePatterns
{
    internal class EnemyDamageState : BaseEnemyState
    {
        private bool _isVulnerable;
        private const float InvulnerabilityTime = 1 / 2f;
        private float _elapsedInvulnerabilityTime;
        private readonly List<Color> _colorList;
        private const float TimeToUpdate = 1 / 9f;
        private float _flashTime;
        private int _colorIndex;
        public EnemyDamageState(EnemyBasedEntity enemyEntity) : base(enemyEntity)
        {
            _isVulnerable = true;
            _colorList = new List<Color>() { Color.Red, Color.White };
        }

        public override void ChangeDirection(Direction newDirection)
        {
            // no need to change direction in this state
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_isVulnerable || _enemyEntity.Health <= 0) { return; }

            SpriteEffects spriteEffects = _enemyEntity.Direction == Direction.West
                ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            // draw sprite
            _enemyEntity.EnemySprite.Draw(spriteBatch, _enemyEntity.Position, _colorList[_colorIndex], spriteEffects, 0, 0.1f);
        }


        public override void Request()
        {
            if (_isVulnerable == false || _enemyEntity.Health <= 0) { return; }
            _isVulnerable = false;
            _elapsedInvulnerabilityTime = 0f;
            _colorIndex = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (_isVulnerable || _enemyEntity.Health <= 0) { return; }
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _elapsedInvulnerabilityTime += deltaTime;
            _flashTime += deltaTime;
            if (_flashTime >= TimeToUpdate) // flash every update
            {
                _flashTime -= TimeToUpdate;
                _colorIndex = (_colorIndex + 1) % _colorList.Count;
            }

            if (_elapsedInvulnerabilityTime >= InvulnerabilityTime)
            {
                _isVulnerable = true;
                if (_enemyEntity is EnemyBasedEntity enemy)
                {
                    enemy.BeenAttacked = false;
                }
            }
        }
    }
}
