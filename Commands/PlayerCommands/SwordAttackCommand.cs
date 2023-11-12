﻿using SprintZero1.Entities;
namespace SprintZero1.Commands.PlayerCommands
{
    internal class SwordAttackCommand : ICommand
    {

        readonly ICombatEntity combatEntity;
        /// <summary>
        /// Create an object to handle when an entity needs to attack with a sword
        /// </summary>
        /// <param name="entity">The entity that uses the command</param>
        public SwordAttackCommand(ICombatEntity entity)
        {
            combatEntity = entity;
        }

        public void Execute()
        {
            combatEntity.Attack("sword");
        }
    }
}