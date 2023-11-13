using Microsoft.Xna.Framework;

namespace SprintZero1.Controllers.EnemyControllers
{
    /// <summary>
    /// Defines an interface for enemy movement controllers.
    /// This interface is used to implement different types of movement behaviors for enemies.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal interface IEnemyMovementController
    {
        /// <summary>
        /// Updates the enemy's movement based on game time.
        /// This method is called periodically to control and adjust the enemy's position and state.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        void Update(GameTime gameTime);
    }
}
