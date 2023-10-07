using Microsoft.Xna.Framework;

namespace SprintZero1.Characters
{
    internal interface ICharacter
    {
        public void Update(GameTime timer);
        public void Draw();
    }
}
