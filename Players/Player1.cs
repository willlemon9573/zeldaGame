using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

/* for sprint 3 */
namespace SprintZero1.Players
{
    internal class Player1 : PlayableCharacter
    {
        /* Refactoring to do:
         * Add get position
         * Add get direction
         */
        public Player1(int health, string name, SpriteBatch spriteBatch, Vector2 pos) : base(health, name, spriteBatch, pos)
        {
            // Can be left blank, unless you want special instantiations
        }
    }
}
