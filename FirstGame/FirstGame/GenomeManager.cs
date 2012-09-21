using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Drawing;

namespace FirstGame
{
    class GenomeManager
    {

        public int getFitness(Player p, double [] fLengths, Vector2 dest, Network n){

            //Calculate distance
            int temp = (int)Math.Sqrt(Math.Pow(p.Position.X - dest.X, 2) + Math.Pow(p.Position.Y - dest.Y, 2));

            n.distance = temp;

            //Get feeler lengths and set default facing
            int fTemp = (int)(-100*(fLengths[3] + fLengths[4] + fLengths[2]));
            int facing = -2;

            //If facing, don't deduct
            if (p.topleftlevel == 1 || p.toprightlevel == 1)
                facing = 0;
            
            //Calculate fitness score
            return 350 - (temp + (int)p.Position.Y) + fTemp + facing * 200;
        }
    }
}
