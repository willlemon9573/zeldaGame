using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SprintZero1.LevelFiles
{
    delegate void RemoveDelegate(IEntity entity);
    /// <summary>
    /// A class that is used to hold the information for individual levels
    /// @author Aaron Heishman
    /// </summary>
    internal class DungeonRoom
    {

        /* --------------------------Private fields-------------------------- */
        /* enemy entity list is separate because we need to remove enemies from the file if they die */
        private readonly List<IEntity> _liveEnemyList;
        private readonly List<IEntity> _deadEnemyList;
        /* Dropepd Item List */
        private readonly List<IEntity> _floorItems;
        private readonly List<IEntity> _itemCollector;
        /* Holds all the architecture of the level (blocks, walls, doors, floor) */
        private readonly List<IEntity> _architechtureList;
        private readonly Dictionary<Direction, Vector2> _playerStartingPositionMap;
        private string _roomName; /* identification for the room */

        /* --------------------------Public properties-------------------------- */
        /// <summary>
        /// Get the live enemy list
        /// </summary>
        public List<IEntity> LiveEnemyList { get { return _liveEnemyList; } }

        /// <summary>
        /// Get and Set the room name
        /// </summary>
        public string RoomName { get { return _roomName; } set { _roomName = value; } }

        /* --------------------------Public methods-------------------------- */
        /// <summary>
        /// Construct a new room object that will hold the information for the room.
        /// </summary>
        /// <param name="roomName">The name of the room</param>
        public DungeonRoom()
        {
            _liveEnemyList = new List<IEntity>();
            _architechtureList = new List<IEntity>();
            _playerStartingPositionMap = new Dictionary<Direction, Vector2>();
            _floorItems = new List<IEntity>();
            _itemCollector = new List<IEntity>();
        }

        /// <summary>
        /// Add an enemy to the list of enemies for this room
        /// </summary>
        /// <param name="enemy">The enemy to be added to this room</param>
        public void AddEnemy(IEntity enemy)
        {
            _liveEnemyList.Add(enemy);
        }


        /// <summary>
        /// Check if any enemy is dead and remove them from the live enemy list and add to the dead enemy list
        /// </summary>
        public void CheckEnemyIsDead()
        {
            List<IEntity> deadEnemyList = _liveEnemyList.Where(entity => (entity as ICombatEntity).Health <= 0).ToList();
            foreach (var entity in deadEnemyList)
            {
                _liveEnemyList.Remove(entity);
                deadEnemyList.Add(entity);
            }
        }

        /// <summary>
        /// Handles updating a locked door if the door can be unlocked
        /// </summary>
        /// <param name="destination">The new destination for the door</param>
        public void UnlockDoor(string destination)
        {

            LockedDoorEntity lockedDoor = _architechtureList.OfType<LockedDoorEntity>().FirstOrDefault(door => door.DoorDestination == destination);
            if (lockedDoor == null) { return; }
            string doorType = $"open_{lockedDoor.DoorDirection}";
            ISprite openDoorSprite = TileSpriteFactory.Instance.CreateNewTileSprite(doorType.ToLower());
            _architechtureList.Remove(lockedDoor);
            _architechtureList.Add(new OpenDoorEntity(openDoorSprite, lockedDoor.Position, lockedDoor.DoorDestination, lockedDoor.DoorDirection));
        }

        /// <summary>
        /// Add any architectural objects like walls, blocks, floors, etc
        /// </summary>
        /// <param name="entity">The </param>
        public void AddArchitecturalEntity(IEntity entity)
        {
            _architechtureList.Add(entity);
        }

        /// <summary>
        /// Add an item to the room item list for dropped items, items that should be on stage, or items
        /// that appear after an event happens
        /// </summary>
        /// <param name="item">The item to be added</param>
        public void AddRoomItem(IEntity item)
        {
            _floorItems.Add(item);
        }

        /// <summary>
        /// Resets all enemies back to their original health and positions
        /// </summary>
        public void ResetEnemies()
        {
            /* remove the dead enemies from the dead enemy list */
            if (_deadEnemyList.Count > 0)
            {
                _liveEnemyList.AddRange(_deadEnemyList);
                _deadEnemyList.Clear();
            }
            /* reset each enemy back to their original positions and health */
            foreach (var entity in _liveEnemyList)
            {
                (entity as BaseEnemyEntity).Reset();
            }
        }

        /// <summary>
        /// Add the position for the player to be placed whenever they do a
        /// room transition
        /// </summary>
        /// <param name="direction">The direction in which they player enters the room</param>
        /// <param name="position">The position where the player should be placed</param>
        public void AddPlayerStartingPositions(Direction direction, Vector2 position)
        {
            Debug.Assert(!_playerStartingPositionMap.ContainsKey(direction), $"Player direction {direction} already found in position map");
            _playerStartingPositionMap[direction] = position;
        }

        /// <summary>
        /// Get the starting position for the player based on which room they direction the player will be entering
        /// the room from
        /// </summary>
        /// <param name="direction">The direction in which they player enters the room</param>
        /// <returns>A vector2 that contains the (x, y) coordinates related to where the player should enter a room</returns>
        public Vector2 GetPlayerStartingPosition(Direction direction)
        {
            Debug.Assert(_playerStartingPositionMap.ContainsKey(direction), $"Room does not contain a starting position for {direction}");
            return _playerStartingPositionMap[direction];
        }
        /// <summary>
        /// Used for removing items from the room when they are picked up.
        /// </summary>
        /// <param name="entity">The entity to be removed</param>
        public void RemoveItem(IEntity entity)
        {
            _floorItems.Remove(entity);
            /* adding item to be removed from list*/
            _itemCollector.Add(entity);
        }

        /// <summary>
        /// Clear any of this rooms objects that were added to the item collector.
        /// </summary>
        public void ClearTrash()
        {
            _itemCollector.Clear();
        }

        public List<IEntity> GetEntityList()
        {
            List<IEntity> entities = new List<IEntity>();
            entities.AddRange(_liveEnemyList);
            entities.AddRange(_architechtureList);
            entities.AddRange(_floorItems);
            return entities;
        }
    }
}
