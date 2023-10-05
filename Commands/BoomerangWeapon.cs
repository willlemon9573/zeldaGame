using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
namespace SprintZero1.Commands
{
    public class BoomerrangWeapon : ICommand
    {
        private Game1 game;
        private int Direction;
        private Vector2 location;
        //private WeaponSpriteFactory WeaponFactory;
        ISprite newSprite;

        public BoomerrangWeapon(Game1 game)
        {
            this.game = game;
            //this.WeaponFactory = WeaponSpriteFactory.Instance;
        }

        public void Execute()
        {
            //location = game.position;
            //Direction = game.CurrentDirection;
            //newSprite = WeaponFactory.CreateArrowSprite("", location, 3, Direction);
            //game.Weapon = newSprite;
        }

    }
}
