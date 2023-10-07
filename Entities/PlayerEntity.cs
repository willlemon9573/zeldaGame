using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;

namespace SprintZero1.Entities
{
    internal class PlayerEntity : Entity, IMovableEntity, IDamageableEntity
    {
        /* Player Components */
        private State _playerState;
        private int _playerHealth;
        private ISprite _playerSprite;
        private Direction _playerDirection;
        private Vector2 _playerPosition;
        /* Player entity Properties */
        public new Vector2 Position
        {
            get { return _playerPosition; }
            set { _playerPosition = value; }
        }
        public Direction Direction
        {
            get { return _playerDirection; }
        }
        public State State
        {
            get { return _playerState; }
            set { _playerState = value; }
        }
        public int Health
        {
            get { return _playerHealth; }
            set { _playerHealth = value; }
        }
        /* End of Player Entity Properties */

        /// <summary>
        /// Construct a new player entity
        /// </summary>
        /// <param name="sprite">The sprite for the player entity </param>
        /// <param name="position">The position of the player entity</param>
        /// <param name="startingHealth">The starting health of the player entity</param>
        /// <param name="startingDirection">The starting direction the player entity will be facing</param>
        /// <param name="startingState">The starting state of the player entity</param>
        public PlayerEntity(ISprite sprite, Vector2 position, int startingHealth, Direction startingDirection, State startingState) : base(sprite, position)
        {
            _playerSprite = sprite;
            _playerDirection = startingDirection;
            _playerHealth = startingHealth;
            _playerPosition = position;
            _playerState = startingState;
        }

        public void TakeDamage(int damage)
        {
            _playerHealth -= damage;
        }

        public void ChangeDirection(Direction direction)
        {
            _playerDirection = direction;
            _playerSprite = LinkSpriteFactory.Instance.GetLinkSprite(direction);
        }

        public new void Update(GameTime gameTime)
        {
            _playerSprite.Update(gameTime);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (_playerDirection == Direction.West)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            _playerSprite.Draw(spriteBatch, _playerPosition, spriteEffects);
        }


    }
}
