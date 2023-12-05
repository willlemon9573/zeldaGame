using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Colliders.ItemColliders;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using SprintZero1.StatePatterns.GameStatePatterns;
using System;

namespace SprintZero1.Entities.WeaponEntities
{
    internal class AquamentusProjectileEntity : IWeaponEntity, ICollidableEntity
    {
        private const float DefaultDamage = 1f;
        private const float MoveSpeed = 1.5f;
        private const float TimeToRemove = 2f;
        private float _elapsedTime;
        private readonly ISprite _projectileSprite;
        private bool _isActive;
        private Vector2 _position;
        private Vector2 _directionToMove;
        private ICollider _projectileCollider;
        private readonly float _angle;

        public float WeaponDamage { get { return DefaultDamage; } }

        public bool IsActive { get { return _isActive; } set { _isActive = false; } }

        public ISprite Sprite { get { return _projectileSprite; } }

        public Vector2 Position { get { return _position; } set { _position = value; } }

        public ICollider Collider { get { return _projectileCollider; } }

        public AquamentusProjectileEntity(float angleInDegrees)
        {
            _angle = angleInDegrees;
            _projectileSprite = WeaponSpriteFactory.Instance.CreateAquamentusWeaponSprite();
        }

        private void StartProjectile()
        {
            if (GameStatesManager.CurrentState is GamePlayingState playingState)
            {
                playingState.AddProjectile(this);
            }

            _isActive = true;
        }

        private void StopProjectile()
        {
            if (GameStatesManager.CurrentState is GamePlayingState playingState)
            {
                playingState.RemoveProjectile(this);
            }
            _isActive = false;
        }

        public void UseWeapon(Direction direction, Vector2 position)
        {
            if (_isActive) { return; }
            _position = position;
            float angleInRad = (float)(_angle * Math.PI / 180);
            // calculate the direction to move to make sure that the projectiles move in the correct direction
            _directionToMove = new Vector2((float)Math.Cos(angleInRad), -(float)Math.Sin(angleInRad)) * MoveSpeed; // negative sin accounts for inverted axes
            _projectileCollider = new EnemyProjectileCollider(position, new System.Drawing.Size(_projectileSprite.Width, _projectileSprite.Height));
            _elapsedTime = 0;
            StartProjectile();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _projectileSprite.Draw(spriteBatch, _position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_elapsedTime < TimeToRemove && _isActive)
            {
                _position += _directionToMove;
                _projectileSprite.Update(gameTime);
                _projectileCollider.Update(this);
            }
            else
            {
                StopProjectile();
            }
        }


    }
}
