using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.Managers;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class UnlockDoorCommand : ICommand
    {
        private const int MaxDirections = 4;
        private readonly ICollidableEntity _player;
        private readonly LockedDoorEntity _door;
        private readonly ICommand _pushBackCommand;

        private bool TryUnlockDoor()
        {

            int keyCount = PlayerInventoryManager.GetStackableItemCount(_player, StackableItems.DungeonKey);
            if (keyCount < 1) { return false; }
            /* push player back so they dont walk into the door as it's opening */
            _pushBackCommand.Execute();

            _door.OpenDoor();

            string nextRoom = _door.DoorDestination;
            int oppositeDirectionIndex = ((int)_door.DoorDirection + 2) % MaxDirections;
            Direction oppositeDoorDirection = (Direction)oppositeDirectionIndex;
            // unlock the current room's door first, then unlock the room the door the leads to the current room from the next room

            LevelManager.OpenDoor(nextRoom, oppositeDoorDirection);
            PlayerInventoryManager.UseStackableItem(_player, StackableItems.DungeonKey, 1);
            return true;
        }

        /// <summary>
        /// Create a new instance of the unlock door command
        /// </summary>
        /// <param name="player">The player interacting with the door</param>
        /// <param name="door">the door the player is interacting with</param>
        public UnlockDoorCommand(ICollidableEntity player, ICollidableEntity door)
        {
            _player = player;
            _door = door as LockedDoorEntity;
            _pushBackCommand = new PushBackCommand(player, door);
        }

        /// <summary>
        /// Executes the given command 
        /// </summary>
        public void Execute()
        {
            /* Try to unlock the door */
            if (TryUnlockDoor()) { return; }
            _pushBackCommand.Execute();
        }
    }
}
