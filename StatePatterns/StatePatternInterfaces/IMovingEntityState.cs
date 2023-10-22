using Microsoft.Xna.Framework;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.StatePatternInterfaces
{
    internal interface IMovingEntityState
    {

        /// <summary>
        /// Handles update logic for changing entity direction
        /// </summary>
        void ChangeDirection(Direction direction);

        /// <summary>
        /// Controls when the player pauses the game / interacts with objects
        /// </summary>
        void BePaused();

        /// <summary>
        /// Handles any updates made for the entity 
        /// </summary>
        /// <param name="gameTime">The current time state of the game</param>
        void Update(GameTime gameTime);
    }
}
