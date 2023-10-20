using SprintZero1.Enums;

namespace SprintZero1.StateMachines
{
    /// <summary>
    /// A statemachine to control the states of the player
    /// Currently written as PlayerStateMachine, but will be updated to be a general state machine uses for Entities with states
    /// Looking into using the state pattern instead of state machine pattern
    /// @Author Aaron Heishamn
    /// </summary>
    public class PlayerStateMachine
    {
        private State _currentState;
        private bool _transitionable;
        /// <summary>
        /// Constructor for player state machine
        /// </summary>
        /// <param name="startingState">The state in which the player will start</param>
        public PlayerStateMachine(State startingState)
        {
            _currentState = startingState;
            _transitionable = true;
        }
        /// <summary>
        /// Block the player from transitioning to a new state
        /// </summary>
        public void BlockTransition()
        {
            _transitionable = false;
        }
        /// <summary>
        /// Unblock the player from transition to a new state
        /// </summary>
        public void UnblockTransition()
        {
            _transitionable = true;
        }
        /// <summary>
        /// Check if the player can transition to a new state
        /// </summary>
        /// <returns>True if player can transition, else false</returns>
        public bool CanTransition()
        {
            return _transitionable;
        }
        /// <summary>
        /// Change the player state
        /// </summary>
        /// <param name="newState">The new state the player will take</param>
        public void ChangeState(State newState)
        {
            if (_currentState != newState)
            {
                _currentState = newState;
            }
        }
        /// <summary>
        /// Get the current state of the player
        /// </summary>
        /// <returns></returns>
        public State GetCurrentState()
        {
            return _currentState;
        }
    }
}
