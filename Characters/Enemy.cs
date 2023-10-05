using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.AI;
using SprintZero1.Sprites;
using System.Collections.Generic;

namespace SprintZero1.Characters
{
    public class Enemy : ICharacter
    {
        public ISprite EnemySprite;
        SpriteBatch spriteBatch;
        int health = 1;
        string name = "Enemy";
        Texture2D texture;
        Rectangle source = new Rectangle(230, 122, 16, 16);
        Rectangle destination = new Rectangle(500, 200, 49, 49);
        public Vector2 pos = new Vector2(500, 200);
        EnemyAI ai;
        Game1 game;

        List<Projectile> projectiles = new List<Projectile>();

        List<string> enemies = new List<string>() { "enemy1", "enemy2", "enemy3", "enemy4" };

        public Enemy(SpriteBatch spriteBatch, Texture2D texture, Game1 game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.ai = new EnemyAI(this);
        }

        public void Update(GameTime timer)
        {

            ai.Update(timer);
            destination.X = (int)pos.X;
            destination.Y = (int)pos.Y;
            /* EnemySprite.Position = pos;*/
            for (int i = 0; i < projectiles.Count; i++)
            {
                Projectile projectile = projectiles[i];
                projectile.Update(timer);
                if (projectile.pos.X < 0)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
            EnemySprite.Update(timer);
        }

        public void Draw()
        {

            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw();
            }
            EnemySprite.Draw(spriteBatch);
        }

        public void Fire()
        {
            projectiles.Add(new Projectile(spriteBatch, pos, texture));
        }
    }
}
