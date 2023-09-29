using SprintZero1.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Commands
{
    internal class LinkHurtCommand : ICommand
    {
        Game1 game;
        PlayableCharacter character;

        public LinkHurtCommand(Game1 game)
        {
            
        }

        
        public void Execute()
        {
            character.TakeDamage();
        } 
    }
}
