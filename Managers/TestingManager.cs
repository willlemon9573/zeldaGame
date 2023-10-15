/*A simple class to test if things are working correctly */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;

namespace SprintZero1.Managers
{
    internal static class TestingManager
    {
        private static List<IEntity> EntityList = new List<IEntity>();
        private static List<Tuple<ISprite, Vector2>> staticSpriteTestList = new List<Tuple<ISprite, Vector2>>();
        private static IController controller;
        private static Game1 myGame;
        private static bool startTesting = false;
        public static void StartTest(Game1 game)
        {
            myGame = game;
            startTesting = true;
            controller = new KeyboardController();
            startTesting = true;
        }

        public static void TestPlayerEntityWithKeyboard(Vector2 position, int health, Direction direction)
        { 
            IEntity player = new PlayerEntity(position, health, direction);
            ProjectileEntity _ProjectileEntity = new ProjectileEntity(position, direction);
            controller.LoadDefaultCommands(myGame, player, _ProjectileEntity);
            EntityList.Add(player);
            EntityList.Add(_ProjectileEntity);
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
                controller.Update();
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (startTesting)
            {
                foreach (var spriteTuple in staticSpriteTestList)
                {
                    spriteTuple.Item1.Draw(spriteBatch, spriteTuple.Item2);
                }
                foreach (IEntity entity in EntityList)
                {
                    entity.Draw(spriteBatch);
                }
            }
        }
    }
}
