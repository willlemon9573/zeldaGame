﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintZero1.Entities;
using SprintZero1.Sprites;
using System.Runtime.CompilerServices;
using SprintZero1.Controllers;
using SprintZero1.Factories;
using SprintZero1.Enums;
using SprintZero1.Colliders;

namespace SprintZero1.src
{
    internal static class ProgramManager
    {
        public static Game1 game;
        static List<IEntity> onScreenEntities = new List<IEntity>();

        // Runs on startup. Use it to add entity if you want to test things
        public static void Start(Game1 localGame)
        {


        }

        public static void AddOnScreenEntity(IEntity entity)
        {
            onScreenEntities.Add(entity);
        }

        public static void RemoveOnScreenEntity(IEntity entity)
        {
            onScreenEntities.Remove(entity);
        }

        public static void Update(GameTime gameTime)
        {

            foreach (IEntity entity in onScreenEntities)
            {
                entity.Update(gameTime);
            }
            ColliderManager.Update(gameTime);
        }

        public static void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            foreach (IEntity entity in onScreenEntities)
            {
                entity.Draw(gametime, spriteBatch);
            }
        }
    }
}
