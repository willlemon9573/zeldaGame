using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.Managers;
using System.Collections.Generic;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class UnlockDoorCommand : ICommand
    {
        const int Zero = 0;
        const int One = 1;
        private readonly ICollidableEntity _player;
        private readonly ICollidableEntity _door;


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
            _door = door;
        }

        /// <summary>
        /// Executes the given command 
        /// </summary>
        public void Execute()
        {
            /* if the door can be unlocked then return */
            if (ProgramManager.UnlockDoor(_player, (LockedDoorEntity)_door)) { return; }
            /* push link back if the door cannot be unlocked */
            PushBack();
        }
    }
}
