using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.Intrinsics.X86;
using System.Collections.Generic;
using SprintZero1.AI;
using System.Threading;
using SprintZero1.Factories;
using Microsoft.Xna.Framework.Input;

namespace SprintZero1.Characters
{
    public class Enemy : ICharacter
    {
        CharacterSprite EnemySprite = new CharacterSprite();   
        SpriteBatch spriteBatch;
        int health = 1;
        string name = "Enemy";
        Texture2D texture;
        Rectangle source = new Rectangle(230, 122, 16, 16);
        Rectangle destination = new Rectangle(500, 200, 49, 49);
        public Vector2 pos = new Vector2(500, 200);
        EnemyAI ai;
        Game1 game;
        EnemyFactory factory = EnemyFactory.Instance;

        List<Projectile> projectiles = new List<Projectile>();

        List<string> enemies = new List<string>() { "enemy1", "enemy2", "enemy3", "enemy4" };

        public Enemy(SpriteBatch spriteBatch, Texture2D texture, Game1 game) 
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.ai = new EnemyAI(this);
            factory.createSpriteDictionary();
        }


        public void Update(GameTime timer)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                source = factory.Source(enemies[new Random().Next(1, 4)]);
            }

            ai.Update(timer);
            destination.X = (int) pos.X;
            destination.Y = (int) pos.Y;
            
            for(int i = 0; i < projectiles.Count; i++)
            {
                Projectile projectile = projectiles[i];
                projectile.Update(timer);
                if(projectile.pos.X < 0)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw()
        {
            
            foreach(Projectile projectile in projectiles)
            {
                projectile.Draw();
            }
            EnemySprite.Draw(spriteBatch, texture, Color.White, destination, source);
        }

        public void Fire()
        {
            projectiles.Add(new Projectile(spriteBatch, pos, texture));
        }
    }
}
