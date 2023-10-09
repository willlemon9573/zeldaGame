using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Entities
{
    internal interface IEntity
    {
        /// <summary>
        /// Set and Get the Entity Position
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Update the Entity 
        /// </summary>
        /// <param name="gameTime">Total Game Time</param>
        public void Update(GameTime gameTime);
        /// <summary>
        /// Draw the Entity
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch);
    }
}
