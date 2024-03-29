﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.StatePatterns.GameStatePatterns;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using System;
using System.Collections.Generic;

namespace SprintZero1.Managers
{
    internal static class GameStatesManager
    {
        /// <summary>
        /// Map of IGameState's
        /// </summary>
        private static Dictionary<GameState, IGameState> _gameStateMap;

        private static Game1 _game;

        /// <summary>
        /// Current game state. Will be be called for Update and Draw
        /// </summary>
        private static IGameState _gameState;
        public static IGameState CurrentState { get { return _gameState; } }


        public static Game ThisGame { get { return _game; } }

        /// <summary>
        /// Creates the map that will contain the required functions for game state changing
        /// </summary>
        /// <param name="game">The current game</param>
        public static void InitializeGameStateMap(Game1 game)
        {
            _game = game;
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

        private static void CreatePlayers()
        {
            string controllerPath = @"XMLFiles/PlayerXMLFiles/ControllerSettings.xml";
            string characterPath = @"XMLFiles/PlayerXMLFiles/CharacterInfo.xml";
            string playerOneCharacter = "Link";
            string playerTwoCharacter = "Zelda"; // both link until we get zelda
            // create each player
            IPlayerBuilder playerBuilder = new PlayerBuilderManager(characterPath);
            Tuple<IEntity, IController> playerOne = playerBuilder.BuildPlayerWithKeyboard(controllerPath, _game, playerOneCharacter);
            Tuple<IEntity, IController> playerTwo = playerBuilder.BuildPlayerWithGamePad(controllerPath, _game, playerTwoCharacter);
            List<IEntity> playerList = new List<IEntity>()
            {
                playerOne.Item1,
                playerTwo.Item1
            };
            // add each player to each state as each state may need to modify the player in some way
            foreach (IGameState gameState in _gameStateMap.Values)
            {
                gameState.AddPlayer(playerOne);
                gameState.AddPlayer(playerTwo);
            }
            HUDManager.Initialize(playerList);
        }

        /// <summary>
        /// Set logic for game start
        /// </summary>
        public static void Start()
        {
            CreatePlayers();
            _gameState = _gameStateMap[GameState.Playing];
            (_gameState as GamePlayingState).LoadDungeonRoom("entrance");
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
