using SprintZero1.Enums;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using SprintZero1.StatePatterns.MovingStatePatterns;
using System;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.CombatStatePatterns
{
    internal abstract class BaseAttackingState : BaseMovingState, ICombatState
    {
        private Dictionary<Direction, Func<ICombatState>> DirectionToStateMap;
        private void CreateDirectionToStateMap()
        {
            DirectionToStateMap = new Dictionary<Direction, Func<ICombatState>>()
            {
                { Direction.North, () => new NorthAttackingState(entity) },
                { Direction.South, () => new SouthAttackingState(entity) },
                { Direction.West, () => new WestAttackingState(entity) },
                { Direction.East, () => new EastAttackingState(entity) }
            };
        }

        public BaseAttackingState()
        {
            CreateDirectionToStateMap();
        }
        /// <summary>
        /// Abstract function for all entities to overwrite
        /// </summary>
        public abstract void Attack();

        public void GetKnockedBack(Direction direction)
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
