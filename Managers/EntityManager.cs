using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.LevelFiles;
using System.Collections.Generic;
using System.Linq;

namespace SprintZero1.Managers
{
    internal class EntityManager
    {
        private List<IEntity> entities = new List<IEntity>();
        private List<IEntity> entitiesToAdd = new List<IEntity>();
        private List<IEntity> entitiesToRemove = new List<IEntity>();

        /// <summary>
        /// Returns the list of entities to be display onscreen
        /// </summary>
        /// <returns>List of entities</IEntity></returns>
        public List<IEntity> OnScreenEntities()
        {
            return entities;
        }

        /// <summary>
        /// Resets Entitys in EntityManager
        /// </summary>
        public void Reset()
        {
            entities.Clear();
            entitiesToAdd.Clear();
            entitiesToRemove.Clear();
        }

        /// <summary>
        /// Readies the next screen, removing
        /// all but the player
        /// </summary>
        /// <param name="player">Player to be loaded into the next screen</param>
        public void LoadNextScreen(IEntity player)
        {
            entitiesToRemove.AddRange(entities);
            entitiesToAdd.Clear();
            entitiesToAdd.Add(player);
        }

        public void UpdateEntities(List<IEntity> update)
        {
            entities = update;
        }

        /// <summary>
        /// Change room entities to DungeonRoom entities
        /// </summary>
        /// <param name="room"></param>
        public void ParseDungeonRoom(DungeonRoom nextRoom)
        {
            Remove(entities);
            Add(nextRoom.GetEntityList());
        }

        /// <summary>
        /// Updates the list of onscreen entities
        /// Removing entities queued to remove
        /// Then adds entities queued to add
        /// </summary>
        /// <param name="gametime"> Gametime </param>
        public void Update(GameTime gametime)
        {
            entities = entities.Except(entitiesToRemove).ToList();
            entities.AddRange(entitiesToAdd);
            entitiesToRemove.Clear();
            entitiesToAdd.Clear();
        }

        /// <summary>
        /// Queues an Entity for removal next Update()
        /// </summary>
        /// <param name="entity">IEntity to remove</param>
        public void Remove(IEntity entity)
        {
            entitiesToRemove.Add(entity);
        }

        /// <summary>
        /// Queues a list of entities for removal next Update()
        /// </summary>
        /// <param name="entitiesToRemove">List of entities to remove</param>
        public void Remove(List<IEntity> entitiesToRemove)
        {
            this.entitiesToRemove.AddRange(entitiesToRemove);
        }

        /// <summary>
        /// Queues a entity for addition next Update()
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public void Add(IEntity entity)
        {
            entitiesToAdd.Add(entity);
        }

        /// <summary>
        /// Queues a list of entities to be added next Update()
        /// </summary>
        /// <param name="entitiesToAdd">List of entities to add</param>
        public void Add(List<IEntity> entitiesToAdd)
        {
            this.entitiesToAdd.AddRange(entitiesToAdd);
        }

        /// <summary>
        /// Immediately add to entities list (during current update)
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public void AddImmediately(IEntity entity)
        {
            entities.Add(entity);
        }

        /// <summary>
        /// Immediately removes Entity from list during update
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        public void RemoveImmediately(IEntity entity)
        {
            entities.Remove(entity);
        }
    }
}
