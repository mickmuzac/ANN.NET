/*

	By: Mick Muzac and Didier Lassage



*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FirstGame
{
    class Food
    {
        public Vector2 location;
        public float relativeAngle = 0;
        public int playerDistance = 0;

        public Food(float x, float y)
        {

            location = new Vector2(x, y);
        }

        public Vector2 getLocation()
        {

            return location;
        }

    }
}
