using SprintZero1.Entities;

namespace SprintZero1.Controllers
{
    internal interface IController
    {
        /// <summary>
        /// Loads the default command map for the controller
        /// </summary>
        /// <param name="game">Game1 object</param>

        void LoadDefaultCommands(Game1 game, ICombatEntity playerEntity, IEntity ProjectileEntity);


        /// <summary>
        /// Updates game based on which input is read
        /// </summary>
        void Update();
    }
}
