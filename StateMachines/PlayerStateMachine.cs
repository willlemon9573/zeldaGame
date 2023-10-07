using SprintZero1.Enums;

namespace SprintZero1.StateMachines
{
    public class PlayerStateMachine
    {
        private Direction _currentDirection = Direction.South;
        private MovementState _currentMovementState = MovementState.Idle;

        public PlayerStateMachine(Direction direction, MovementState movementState)
        {
            _currentDirection = direction;
            _currentMovementState = movementState;
        }
        public void ChangeDirection(Direction newDirection)
        {
            this._currentDirection = newDirection;
        }

        public void ChangeMovementState(MovementState newMovementState)
        {
            _currentMovementState = newMovementState;
        }

        public Direction GetDirection()
        {
            return _currentDirection;
        }

        public MovementState GetMovementState()
        {
            return _currentMovementState;
        }
    }
}
