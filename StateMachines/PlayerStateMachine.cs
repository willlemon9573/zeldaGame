using SprintZero1.Enums;

namespace SprintZero1.StateMachines
{
    public class PlayerStateMachine
    {
        private Direction _currentDirection = Direction.South;
        private State _currentMovementState = State.Idle;

        public PlayerStateMachine(Direction direction, State movementState)
        {
            _currentDirection = direction;
            _currentMovementState = movementState;
        }
        public void ChangeDirection(Direction newDirection)
        {
            this._currentDirection = newDirection;
        }

        public void ChangeMovementState(State newMovementState)
        {
            _currentMovementState = newMovementState;
        }

        public Direction GetDirection()
        {
            return _currentDirection;
        }

        public State GetMovementState()
        {
            return _currentMovementState;
        }
    }
}
