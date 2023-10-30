﻿using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.PlayerCommands
{
    /// <summary>
    /// Basis for all the commands the player can do to change the state of game
    /// </summary>
    public abstract class BaseChangeGameStateCommand : ICommand
    {
        /// <summary>
        /// Delegates for handling game state changes
        /// </summary>
        protected readonly GameChangeStateHandler _gameChangeStateHandler;
        protected readonly GameStateHandler _gameStateHandler;
        /// <summary>
        /// Default implementation for creating game state change commands based on player input
        /// </summary>
        /// <param name="gameState">base game state class reference</param>
        public BaseChangeGameStateCommand(BaseGameState gameState)
        {
            _gameChangeStateHandler = gameState.ChangeGameState;
            _gameStateHandler = gameState.Handle;
        }

        public abstract void Execute();
    }
}
