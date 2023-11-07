using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SprintZero1.LevelFiles
{
    /// <summary>
    /// A class that is used to hold the information for individual levels
    /// @author Aaron Heishman
    /// </summary>
    internal class DungeonRoom
    {

        /* --------------------------Private fields-------------------------- */
        /* enemy entity list is separate because we need to remove enemies from the file if they die */
        private List<IEntity> _liveEnemyList;
        private List<IEntity> _deadEnemyList;
        /* Holds all the architecture of the level (blocks, walls, doors, floor) */
        private List<IEntity> archituectureList;
        private IEntity roomItem;
        private Dictionary<Direction, Vector2> _playerStartingPositionMap;
        private string _roomName; /* identification for the room */

        /* --------------------------Public properties-------------------------- */
        /// <summary>
        /// Get the live enemy list
        /// </summary>
        public List<IEntity> LiveEnemyList { get { return _liveEnemyList; } }
        /* these gets are set up just in case they have a use, if not will then be deleted */
        /// <summary>
        /// Get the room item if there exists one
        /// </summary>
        public IEntity RoomItem { get { return roomItem; } }

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
            archituectureList = new List<IEntity>();
            _playerStartingPositionMap = new Dictionary<Direction, Vector2>();

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
        private void CheckEnemyIsDead()
        {
            List<IEntity> deadEnemyList = _liveEnemyList.Where(entity => (entity as ICombatEntity).Health <= 0).ToList();
            foreach (var entity in deadEnemyList)
            {
                _liveEnemyList.Remove(entity);
                deadEnemyList.Add(entity);
            }
        }

        /// <summary>
        /// Add any architectural objects like walls, blocks, floors, etc
        /// </summary>
        /// <param name="entity">The </param>
        public void AddArchitecturalEntity(IEntity entity)
        {
            archituectureList.Add(entity);
        }

        /// <summary>
        /// If the room has an item to be displayed then adds that to the room's data
        /// </summary>
        /// <param name="item">The item to be added</param>
        public void AddRoomItem(IEntity item)
        {
            roomItem = item;
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
    }
}
