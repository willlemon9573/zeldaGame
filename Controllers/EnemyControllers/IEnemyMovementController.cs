using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
