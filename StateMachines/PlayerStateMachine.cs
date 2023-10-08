using SprintZero1.Enums;

namespace SprintZero1.StateMachines
{
    /// <summary>
    /// Needs more states added to it. Might need to break up states? 
    /// </summary>
    public class PlayerStateMachine
    {
        private State _currentState;
        private bool _transitionable;

        public PlayerStateMachine(State startingState)
        {
            _currentState = startingState;
            _transitionable = true;
        }

        public void BlockTransition()
        {
            _transitionable = false;
        }

        public void UnblockTransition()
        {
            _transitionable = true;
        }

        public bool CanTransition()
        {
            return _transitionable;
        }

        public void ChangeState(State newState)
        {
            if (_currentState != newState)
            {
                _currentState = newState;
            }
        }

        public State GetCurrentState()
        {
            return _currentState;
        }
    }
}
