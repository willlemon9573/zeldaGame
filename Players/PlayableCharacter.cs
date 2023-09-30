using System.Numerics;
using Microsoft.Xna.Framework.Graphics;
/* for sprint 3 */
namespace SprintZero1.Players
{
    public abstract class PlayableCharacter
    {
        public int health { get; set; }
        public string name { get; set; }
        SpriteBatch spriteBatch { get; set; }

        public PlayableCharacter(int health, string name, SpriteBatch spriteBatch, Vector2 pos)
        {
            this.health = health;
            this.name = name;
            this.spriteBatch = spriteBatch;
            this.pos = pos;
        }

        public Vector2 pos { get; set; }
        public void TakeDamage(int damage)
        {
            health = health - damage;
        }

        public void Move(Vector2 pos) 
        {

        }
    }
}
