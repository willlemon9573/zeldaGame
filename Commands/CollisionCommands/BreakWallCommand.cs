using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.WeaponEntities.BombEntityFolder;
using SprintZero1.Enums;
using SprintZero1.Managers;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class BreakWallCommand : ICommand
    {
        private const int MaxDirections = 4;
        ICollidableEntity _bomb;
        ICollidableEntity _wall;

        private void BreakWall(BreakableWallEntity breakableWall)
        {
            breakableWall.OpenDoor();
            string nextRoom = breakableWall.DoorDestination;

            int oppositeDirectionIndex = ((int)breakableWall.DoorDirection + 2) % MaxDirections;
            Direction oppositeDoorDirection = (Direction)oppositeDirectionIndex;

            LevelManager.OpenDoor(nextRoom, oppositeDoorDirection);
        }

        public BreakWallCommand(ICollidableEntity bomb, ICollidableEntity wall)
        {
            _bomb = bomb;
            _wall = wall;
        }

        public void Execute()
        {
            if (_bomb is BombEntity bomb && bomb.HasExploded && _wall is BreakableWallEntity breakableWall)
            {
                BreakWall(breakableWall);
            }
        }
    }
}
