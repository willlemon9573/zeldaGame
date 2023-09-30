using Microsoft.Xna.Framework;
using SprintZero1.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Level
{
    internal class LevelManager
    {
        PlayableCharacter player1;
        

        public LevelManager(PlayableCharacter player1)
        {
            this.player1 = player1;
        }

        public static void FillLevel()
        {

        }

        public static void DrawLevel(Matrix level)
        {
            /*
            Take in a matrix, draw Spriteblocks in a grid
            pattern based on the matrix values
            */
        }

    }
}
