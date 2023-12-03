using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Controllers.EnemyControllers;
using SprintZero1.DebuggingTools;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.Entities.EnemyEntities;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.LevelFiles.RoomEvents;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SprintZero1.LevelFiles
{
    internal delegate void RemoveDelegate(IEntity entity);
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
        private readonly List<IEnemyMovementController> _enemyControllerList;
        /* Dropepd Item List */
        private readonly List<IEntity> _floorItems;
        private readonly List<IEntity> _itemCollector;
        /* Holds all the architecture of the level (blocks, walls, doors, floor) */
        private readonly List<IEntity> _architechtureList;
        private readonly Dictionary<Direction, Vector2> _playerStartingPositionMap;
        private string _roomName; /* identification for the room */
        private int enemyCount;
        private readonly SpriteDebuggingTools _spriteDebugger;
        private readonly List<IRoomEvent> _roomEvents;
        private ColliderManager _colliderManagerRef;
        private SpriteFont _font;

        public DungeonRoom(ColliderManager colliderManagerRef)
        {
            _colliderManagerRef = colliderManagerRef;
        }

        public List<IEntity> LiveEnemyList { get { return _liveEnemyList; } }

        public ColliderManager ColliderManager { set { _colliderManagerRef = value; } }

        private readonly List<ILootableEntity> hearttest = new List<ILootableEntity>();

        /* --------------------------Public properties-------------------------- */

        /// <summary>
        /// Get and Set the room name
        /// </summary>
        public string RoomName { get { return _roomName; } set { _roomName = value; } }

        /* --------------------------Public methods-------------------------- */
        /// <summary>
        /// Construct a new room object that will hold the information for the room.
        /// </summary>
        public DungeonRoom()
        {
            _liveEnemyList = new List<IEntity>();
            _deadEnemyList = new List<IEntity>();
            _enemyControllerList = new List<IEnemyMovementController>();
            _architechtureList = new List<IEntity>();
            _playerStartingPositionMap = new Dictionary<Direction, Vector2>();
            _floorItems = new List<IEntity>();
            _itemCollector = new List<IEntity>();
            _spriteDebugger = new SpriteDebuggingTools(GameStatesManager.ThisGame);
            _roomEvents = new List<IRoomEvent>();

            ISprite heart = ItemSpriteFactory.Instance.CreateNonAnimatedItemSprite("heart");
            Vector2 n = new Vector2(85, 136);
            RemoveDelegate r = this.RemoveFromRoom;

            for (int i = 0; i < 5; i++)
            {

                ILootableEntity repleneshingHeart = new ReplenishingHeartEntity(heart, n, r);
                n.X += 25;
                _floorItems.Add(repleneshingHeart);
            }
        }

        /// <summary>
        /// Add a room even to this specific room
        /// </summary>
        /// <param name="roomEvent">The event that should be added to the room</param>
        public void AddRoomEvent(IRoomEvent roomEvent)
        {
            _roomEvents.Add(roomEvent);
        }

        /// <summary>
        /// Add an enemy to the list of enemies for this room
        /// </summary>
        /// <param name="enemy">The enemy to be added to this room</param>
        public void AddEnemy(IEntity enemy)
        {
            _liveEnemyList.Add(enemy);
            enemyCount++;
        }

        /// <summary>
        /// Check if any enemy is dead and remove them from the live enemy list and add to the dead enemy list
        /// </summary>
        public void RemoveDeadEnemies(IEntity enemy)
        {
            if (_liveEnemyList.Remove(enemy))
            {
                _colliderManagerRef.RemoveCollidableEntity(enemy);
                _deadEnemyList.Add(enemy);
            }
        }

        /// <summary>
        /// Handles unlocking doors based on door destination for locked doors
        /// </summary>
        /// <param name="destination">The new destination for the door</param>
        public void UnlockDoor(Direction direction)
        {
            IDoorEntity door = _architechtureList.OfType<IDoorEntity>().FirstOrDefault(door => door.DoorDirection == direction);
            Debug.Assert(door != null, $"Testing to make sure door is not null");
            door.OpenDoor();
        }

        /// <summary>
        /// Replaces the given door with the new door
        /// </summary>
        /// <param name="blockedDoor">The old door to be removed from the list</param>
        /// <param name="openDoor">the new door to add to the list</param>
        public void UpdateDoor(IEntity blockedDoor, IEntity openDoor)
        {
            _architechtureList.Remove(blockedDoor);
            _architechtureList.Add(openDoor);
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
            _colliderManagerRef?.AddCollidableEntity(item);
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
                (entity as EnemyBasedEntity).ResetEnemy();
            }

            foreach (var controller in _enemyControllerList)
            {
                controller.Start();
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
        /// Remove an item from the room items list and save them in the room's item collector
        /// </summary>
        /// <param name="entity">The entity to be removed</param>
        public void RemoveAndSaveItem(IEntity entity)
        {
            _colliderManagerRef.RemoveCollidableEntity(entity);
            _floorItems.Remove(entity);
            /* adding item to be removed from list*/
            _itemCollector.Add(entity);
        }

        /// <summary>
        /// Remove an entity from the room items list that does not need to be saved in the room's item collector
        /// </summary>
        /// <param name="entity">The entity to be removed</param>
        public void RemoveFromRoom(IEntity entity)
        {
            _colliderManagerRef.RemoveCollidableEntity(entity);
            _floorItems.Remove(entity);
        }

        /// <summary>
        /// Clear any of this rooms objects that were added to the item collector.
        /// </summary>
        public void ClearTrash()
        {
            _itemCollector.Clear();
        }

        public void UpdateEnemyController(List<IEntity> players)
        {
            if (_enemyControllerList.Count > 0 && _liveEnemyList.Count == enemyCount) { return; }
            RemoveDelegate remover = RemoveDeadEnemies;
            _liveEnemyList.ForEach(enemy => _enemyControllerList.Add(new SmartEnemyMovementController(enemy as ICombatEntity, players, remover, _architechtureList)));
        }

        /// <summary>
        /// Get the entire list of entities currently in the room
        /// </summary>
        /// <returns>A list of all entities in the room</returns>
        public List<IEntity> GetEntityList()
        {
            List<IEntity> entities = new List<IEntity>();
            entities.AddRange(_architechtureList);
            entities.AddRange(_liveEnemyList);
            entities.AddRange(_floorItems);
            return entities;
        }


        private void CheckEvents()
        {
            for (int i = 0; i < _roomEvents.Count; i++)
            {
                _roomEvents[i].TriggerEvent();
                if (_roomEvents[i].CanTriggerEvent() == false)
                {
                    _roomEvents.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Update the room entities
        /// </summary>
        /// <param name="gameTime"> The current state of the game time</param>
        public void Update(GameTime gameTime)
        {
            _architechtureList.ForEach(entity => entity.Update(gameTime));
            _liveEnemyList.ForEach(entity => entity.Update(gameTime));
            _architechtureList.ForEach(entity => entity.Update(gameTime));
            _enemyControllerList.ForEach(entity => entity.Update(gameTime));
            _floorItems.ForEach(entity => entity.Update(gameTime));
            if (_roomEvents.Count > 0)
            {
                CheckEvents();
            }
        }

        /// <summary>
        /// Draw the room entities
        /// </summary>
        /// <param name="spriteBatch">The current sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _architechtureList.ForEach(entity => entity.Draw(spriteBatch));
            _liveEnemyList.ForEach(entity => entity.Draw(spriteBatch));
            _architechtureList.ForEach(entity => entity.Draw(spriteBatch));
            _floorItems.ForEach(entity => entity.Draw(spriteBatch));
            /* drawing entity colliders on screen */
            if (_font != null)
            {
                spriteBatch.DrawString(_font, _roomName, new Vector2(100, 50), Color.White);
            }
            foreach (IEntity entity in _architechtureList)
            {
                if (entity is ICollidableEntity)
                {
                    Rectangle collider = (entity as ICollidableEntity).Collider.Collider;
                    Color r = Color.White;
                    if (entity is BreakableWallEntity)
                    {
                        r = Color.Red;
                    }
                    _spriteDebugger.DrawRectangle(collider, r, spriteBatch);
                }
            }

            foreach (IEntity entity in _liveEnemyList)
            {
                if (entity is ICollidableEntity collidableEntity)
                {
                    Rectangle collider = collidableEntity.Collider.Collider;
                    Color c = Color.Yellow;
                    _spriteDebugger.DrawRectangle(collider, c, spriteBatch);
                }
            }

            /* drawing a collider for any floor items */
            foreach (IEntity entity in _floorItems)
            {
                if (entity is ICollidableEntity collidableEntity)
                {
                    Rectangle collider = collidableEntity.Collider.Collider;
                    Color c = Color.GreenYellow;
                    _spriteDebugger.DrawRectangle(collider, c, spriteBatch);
                }
            }
        }
    }
}
