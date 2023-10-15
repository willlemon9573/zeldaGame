using Microsoft.Xna.Framework;
using SprintZero1.Factories;
using SprintZero1.Sprites;

namespace SprintZero1.Commands
{
    public class ShootArrowCommand : ICommand
    {
        private Game1 game;
        private int Direction;
        private Vector2 location;
        private WeaponSpriteFactory WeaponSpriteFactory;
        ISprite newSprite;

        public ShootArrowCommand(Game1 game)
        {
            this.game = game;
            this.WeaponSpriteFactory = WeaponSpriteFactory.Instance;
            //this.game.Weapon = new ArrowWeapon(WeaponSpriteFactory.CreateArrowSprite("x", game.position, 1, game.direction), game.direction, game.position);
        }

        public void Execute()
        {
            //game.Weapon.Draw(game.SpriteBatch);
        }

    }
}
