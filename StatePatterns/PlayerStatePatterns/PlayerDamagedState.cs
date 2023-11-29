using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    internal class PlayerDamagedState : BasePlayerState
    {
        private const float LayerDepth = 0.1f;
        private const float Rotation = 0f;
        private const float TotalKnockBackTime = 1 / 5f;
        private float _elapsedKnockbackTime;
        private const float KnockbackSpeed = 200f; // the speed for knocking back
        private readonly Dictionary<Direction, Vector2> _velocityMap;
        private Vector2 _knockbackDirection;
        private readonly SoundEffect _damageSound;
        public PlayerDamagedState(PlayerEntity playerEntity) : base(playerEntity)
        {
            _velocityMap = new Dictionary<Direction, Vector2>()
           {
                {Direction.North, new Vector2(0, KnockbackSpeed) },
                {Direction.South, new Vector2(0, -KnockbackSpeed) },
                {Direction.East, new Vector2(-KnockbackSpeed, 0) },
                {Direction.West, new Vector2(KnockbackSpeed, 0) }
           };

            _damageSound = SoundFactory.GetSound("link_hurt");
        }

        public override void Request()
        {
            if (_canTransition == false) { return; }
            BlockTransition();
            _elapsedKnockbackTime = 0f;
            _knockbackDirection = _velocityMap[_playerEntity.Direction];
            _damageSound.Play();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects currentSpriteEffect = (_playerEntity.Direction == Direction.West) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            _playerEntity.PlayerSprite.Draw(spriteBatch, _playerEntity.Position, Color.White, currentSpriteEffect, Rotation, LayerDepth);
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _elapsedKnockbackTime += deltaTime;
            if (_elapsedKnockbackTime < TotalKnockBackTime)
            {
                _playerEntity.Position += (_knockbackDirection * deltaTime);
            }
            else
            {
                UnblockTranstion();
                _playerEntity.TransitionToState(State.Idle);
            }
        }
    }
}
