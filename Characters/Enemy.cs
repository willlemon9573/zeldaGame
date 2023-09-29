using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Characters
{
    internal class Enemy : ICharacter
    {
        ISprite EnemySprite = new EnemySprite();
        SpriteBatch SpriteBatch;
        int health;
        string name = string.Empty;
        Texture2D texture;
        Rectangle source;
        Vector2 pos;

        public Enemy(Game1 game, string name, Texture2D texture, Rectangle source, Vector2 pos, int health = 1) 
        {
            this.name = name;
            this.texture = texture;
            this.source = source;
            this.pos = pos;
            this.health = health;
        }

        public void Update()
        {

        }

        public void Draw()
        {

        }
    }
}
