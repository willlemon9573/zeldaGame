using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Commands.PlayerCommands;
using SprintZero1.Enums;
using SprintZero1.StatePatterns.GameStatePatterns;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using System.Collections.Generic;

namespace SprintZero1.Managers
{
    internal static class GameStatesManager
    {
        /// <summary>
        /// Map of IGameState's
        /// </summary>
        private static Dictionary<GameState, IGameState> _gameStateMap;

        /// <summary>
        /// Current game state. Will be be called and set to be global to game.
        /// </summary>
        private static IGameState _gameState;
        public static IGameState CurrentState { get { return _gameState; } }



        /// <summary>
        /// Creates the map that will contain the required functions for game state changing
        /// </summary>
        /// <param name="game">The current game</param>
        public static void InitializeGameStateMap(Game1 game)
        {
            _gameStateMap = new Dictionary<GameState, IGameState>()
            {
                { GameState.GameOver, new GameOverState(game) },
                { GameState.ItemSelectionScreen, new GameItemSelectionState(game) },
                { GameState.LevelCompleted, new GameBeatState(game) },
                { GameState.Paused, new GamePausedState(game) },
                { GameState.Playing, new GamePlayingState(game) },
                { GameState.Reset, new GameResetState(game) },
                { GameState.RoomTransition, new GameRoomTransitionState(game) }
            };
        }

        /// <summary>
        /// Change the global _gameState 
        /// </summary>
        /// <param name="newState"></param>
        public static void ChangeGameState(GameState newState)
        {
            _gameState = _gameStateMap[newState];
        }

        /// <summary>
        /// Gets the new state of the game when a game state change happens
        /// </summary>
        /// <param name="newState">The new state the game changes to</param>
        /// <returns>The new state of the game</returns>
        public static IGameState GetGameState(GameState newState)
        {
            return _gameStateMap[newState];
        }

        public static void Reset()
        {
            _gameStateMap.Clear();
        }

        /// <summary>
        /// Update the CurrentState GameState
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        public static void Update(GameTime gameTime)
        {
            CurrentState.Update(gameTime);
        }

        /// <summary>
        /// Draw the CurrentState GameState
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            CurrentState.Draw(spriteBatch);
        }
    }
}
