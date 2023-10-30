using Microsoft.Xna.Framework;

namespace SprintZero1.Controllers.EnemyControllers
{
    internal interface IEnemyMovementController
    {

        /// <summary>
        /// Updates game based on which input is read
        /// </summary>
        void Update(GameTime gameTime);
    }
}
