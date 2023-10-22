/*A simple class to test if things are working correctly */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using SprintZero1.Controllers.EnemyControllers;

namespace SprintZero1.Managers
{
    internal static class TestingManager
    {
        private static List<IEntity> EntityList = new List<IEntity>();
        private static ICombatEntity _player;
        private static ICombatEntity enemyEntity;
        private static ProjectileEntity _Weapon;
        private static List<Tuple<ISprite, Vector2>> staticSpriteTestList = new List<Tuple<ISprite, Vector2>>();
        private static IController controller;
        private static IEnemyMovementController enemyController;
        private static Game1 myGame;
        private static bool startTesting = false;
        public static void StartTest(Game1 game)
        {

            myGame = game;
            startTesting = true;
            enemyEntity = new EnemyEntityWithoutDirection(new Vector2(50,50), 10, "dungeon_gel", 2);
            enemyController = new RandomEnemyMovementController(enemyEntity);
            controller = new KeyboardController();
            startTesting = true;
        }
      
        public static void TestPlayerEntityWithKeyboard(Vector2 position, int health, Direction direction)
        {
            //enemyEntity = new EnemyEntityWithoutDirection(position, health, "dungeon_gel", 2);
            _player = new PlayerEntity(position, health, direction);//Game1 game, IEntity playerEntity, ProjectileEntity ProjectileEntity
            _Weapon = new ProjectileEntity();
            //enemyController = new RandomEnemyMovementController(enemyEntity);
            controller.LoadDefaultCommands(myGame, _player, _Weapon);
            EntityList.Add(enemyEntity);
            EntityList.Add(_player);
            EntityList.Add((IEntity)_Weapon);


        }


        public static void AddEntity(IEntity entity, ProjectileEntity ProjectileEntity)
        {
            EntityList.Add(entity);
            EntityList.Add(ProjectileEntity);
        }

        public static void AddStaticSprite(ISprite sprite, Vector2 Position)
        {
            staticSpriteTestList.Add(new Tuple<ISprite, Vector2>(sprite, Position));
        }

        public static void Update(GameTime gameTime)
        {
            if (startTesting)
            {

                foreach (IEntity entity in EntityList)
                {
                    entity.Update(gameTime);
                }
                _player.Update(gameTime);
                controller.Update();
                enemyController.Update(gameTime);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            enemyEntity.Draw(spriteBatch);
            /*if (startTesting)
            {
                int i = 0;
                foreach (var spriteTuple in staticSpriteTestList)
                {
                    spriteTuple.Item1.Draw(spriteBatch, spriteTuple.Item2);
                    i++;
                    if (i == 5)
                    {
                        *//* testing block drawn before link, but after floor *//*
                        foreach (var entity in EntityList)
                        {
                            entity.Draw(spriteBatch);
                        }

                        _player.Draw(spriteBatch);
                    }
                }

            }*/
        }
    }
}
