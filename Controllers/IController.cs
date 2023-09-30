using SprintZero1.Commands;
using Microsoft.Xna.Framework.Input;
namespace SprintZero1.Controllers
{
    public interface IController
    {
        /// <summary>
        /// Loads the default command map for the controller
        /// </summary>
        /// <param name="game">Game1 object</param>
        void LoadDefaultCommands(Game1 game);

        /// <summary>
        /// Updates game based on which input is read
        /// </summary>
        void Update();
    }
}
