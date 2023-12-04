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
        private float _elapsedKnockbackTime;
        private float _elapsedInvulnerabilityTime;
        private const float KnockbackSpeed = 100f; // the speed for knocking back
        private readonly List<Color> _colorList;
        private readonly Dictionary<Direction, Vector2> _velocityMap;
        private Vector2 _knockbackDirection;
        private const float TimeToUpdate = 1 / 9f;
        private float _flashTime;
        private int _colorIndex;
        public EnemyDamageState(EnemyBasedEntity enemyEntity) : base(enemyEntity)
        {
            _isVulnerable = true;
            _colorList = new List<Color>() { Color.Red, Color.White };
            _velocityMap = new Dictionary<Direction, Vector2>()
           {
                {Direction.North, new Vector2(0, KnockbackSpeed) },
                {Direction.South, new Vector2(0, -KnockbackSpeed) },
                {Direction.East, new Vector2(-KnockbackSpeed, 0) },
                {Direction.West, new Vector2(KnockbackSpeed, 0) }
           };
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
            BlockTransition();
            _isVulnerable = false;
            _elapsedInvulnerabilityTime = 0f;
            _knockbackDirection = _velocityMap[_enemyEntity.Direction];
            _colorIndex = 0;
        }

        private void Flash(float deltaTime)
        {
            _flashTime += deltaTime;
            if (_flashTime >= TimeToUpdate) // flash every update
            {
                _flashTime -= TimeToUpdate;
                _colorIndex = (_colorIndex + 1) % _colorList.Count;
            }
        }

        private void UpdateInvulnTime(float deltaTime)
        {
            _elapsedInvulnerabilityTime += deltaTime;
            if (_elapsedInvulnerabilityTime >= InvulnerabilityTime)
            {
                _isVulnerable = true;
                if (_enemyEntity is EnemyBasedEntity enemy)
                {
                    enemy.BeenAttacked = false;
                }
            }
        }

        private void KnockBack(float deltaTime)
        {
            _elapsedKnockbackTime += deltaTime;
            if (_elapsedKnockbackTime < InvulnerabilityTime)
            {
                _enemyEntity.Position += (_knockbackDirection * deltaTime);
            }
            else
            {
                UnblockTranstion();
                TransitionState(State.Moving);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_isVulnerable || _enemyEntity.Health <= 0) { return; }
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Flash(deltaTime);
            UpdateInvulnTime(deltaTime);
            KnockBack(deltaTime);


        }
    }
}
