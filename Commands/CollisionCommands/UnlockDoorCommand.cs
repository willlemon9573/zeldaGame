using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.Enums;
using SprintZero1.Managers;
using System.Collections.Generic;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class UnlockDoorCommand : ICommand
    {
        private const int MaxDirections = 4;
        private readonly ICollidableEntity _player;
        private readonly LockedDoorEntity _door;

        private bool TryUnlockDoor()
        {

            int keyCount = PlayerInventoryManager.GetStackableItemCount(_player, StackableItems.DungeonKey);
            if (keyCount < 1) { return false; }
            /* push player back so they dont walk into the door as it's opening */
            PushBack();

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
        /// Using Muhammed's implementation of push back to push the player back if the door is locked and
        /// the player does not have a key
        /// </summary>
        private void PushBack()
        {
            PriorityQueue<Vector2, float> colliderDistances = new PriorityQueue<Vector2, float>();
            Rectangle intersection = Rectangle.Intersect(_player.Collider.Collider, _door.Collider.Collider);
            if (intersection.Width > intersection.Height)
            {
                colliderDistances.Enqueue(new Vector2(0, -1), System.Math.Abs(intersection.Center.Y - _door.Collider.Collider.Top));
                colliderDistances.Enqueue(new Vector2(0, 1), System.Math.Abs(intersection.Center.Y - _door.Collider.Collider.Bottom));
            }
            else
            {
                colliderDistances.Enqueue(new Vector2(1, 0), System.Math.Abs(intersection.Center.X - _door.Collider.Collider.Right));
                colliderDistances.Enqueue(new Vector2(-1, 0), System.Math.Abs(intersection.Center.X - _door.Collider.Collider.Left));
            }
            // Insert Pushback Code Here
            _player.Position += colliderDistances.Dequeue();
            _player.Collider.Update(_player);
        }

        /// <summary>
        /// Create a new instance of the unlock door command
        /// </summary>
        /// <param name="player"></param>
        /// <param name="door"></param>
        public UnlockDoorCommand(ICollidableEntity player, ICollidableEntity door)
        {
            _player = player;
            _door = door as LockedDoorEntity;
        }

        /// <summary>
        /// Executes the given command 
        /// </summary>
        public void Execute()
        {

            if (TryUnlockDoor()) { return; }
            PushBack();
        }
    }
}
