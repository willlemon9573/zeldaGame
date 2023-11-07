using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using System.Collections.Generic;

namespace SprintZero1.LevelFiles.Level1
{
    /// <summary>
    /// A class that is used to hold the information for individual levels
    /// @author Aaron Heishman
    /// </summary>
    internal class Level
    {
        /* -------------- Private Members  --------------*/
        /* enemy entity list is separate because we need to remove enemies from the file if they die */
        private List<IEntity> enemyList;
        /* Holds all the architecture of the level (blocks, walls, doors, floor) */
        private List<IEntity> archituectureList;
        private IEntity roomItem;
        private Vector2 _playerStartingPosition;

        public Level()
        {
            enemyList = new List<IEntity>();
            archituectureList = new List<IEntity>();
        }

        private void AddEnemy(IEntity enemy)
        {
            enemyList.Add(enemy);
        }

        private void AddArchitecture(IEntity architecture)
        {
            archituectureList.Add(architecture);
        }
    }
}
