using Microsoft.Xna.Framework;
using SprintZero1.Factories;
using SprintZero1.Sprites;
namespace SprintZero1.Commands
{
    public class LinkAttackCommand : ICommand
    {
        private Game1 game;
        private int Direction;
        private Vector2 location;
        private ILinkFactory _linkFactory;
        private bool isAttacking;
        ISprite newSprite;

        public LinkAttackCommand(Game1 game)
        {
            this.game = game;
            /*this._linkFactory = game.linkFactory;*/
            isAttacking = true;
        }

        public void Execute()
        {
            /* location = game.position;
             Direction = game.CurrentDirection;
             game.isAttacking = true;
             newSprite = _linkFactory.createNewLink(Direction, location, 2, isAttacking);
             game.SetLink(newSprite);
             game.isAttacking = false;*/
        }

    }
}
