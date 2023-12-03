using SprintZero1.Entities.EntityInterfaces;

namespace SprintZero1.Controllers
{
    public interface IController
    {
        /// <summary>
        /// Loads the controls for the specific controller
        /// </summary>
        void LoadControls(IEntity playerEntity);

        /// <summary>
        /// Updates game based on which input is read
        /// </summary>
        void Update();
    }
}
