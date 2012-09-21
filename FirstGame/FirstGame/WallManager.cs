/*

	By: Mick Muzac and Didier Lassage



*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame
{
    class WallManager
    {

        public List<Wall> walls = new List<Wall>(5);
        public float playerCenterX;
        public float playerCenterY;
        public float feelerCenterX;
        public float feelerCenterY;

        public float temp;
        public float templeftx;
        public float templefty;
        public float temprightx;
        public float temprighty;
        public float tempmiddlex;
        public float tempmiddley;

        public void addWall(Wall wall)
        {
            walls.Add(wall);
        }

        public bool detectCollision(Player player)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if (player.BoundingBox.Intersects(walls.ElementAt(i).BoundingBox))
                {

                    if (player.BoundingBox.Right >= walls.ElementAt(i).BoundingBox.Left
                        && player.BoundingBox.Left < walls.ElementAt(i).BoundingBox.Left
                        && player.BoundingBox.Bottom > walls.ElementAt(i).BoundingBox.Top
                        && player.BoundingBox.Top < walls.ElementAt(i).BoundingBox.Bottom)
                    {
                        return true;
                    }

                    if (player.BoundingBox.Left <= walls.ElementAt(i).BoundingBox.Right
                        && player.BoundingBox.Right > walls.ElementAt(i).BoundingBox.Right
                        && player.BoundingBox.Bottom > walls.ElementAt(i).BoundingBox.Top
                        && player.BoundingBox.Top < walls.ElementAt(i).BoundingBox.Bottom)
                    {
                        return true;
                    }

                    if (player.BoundingBox.Top <= walls.ElementAt(i).BoundingBox.Bottom
                        && player.BoundingBox.Bottom > walls.ElementAt(i).BoundingBox.Bottom
                        && player.BoundingBox.Right > walls.ElementAt(i).BoundingBox.Left
                        && player.BoundingBox.Left < walls.ElementAt(i).BoundingBox.Right)
                    {
                        return true;
                    }

                    if (player.BoundingBox.Bottom >= walls.ElementAt(i).BoundingBox.Top
                        && player.BoundingBox.Top < walls.ElementAt(i).BoundingBox.Top
                        && player.BoundingBox.Right > walls.ElementAt(i).BoundingBox.Left
                        && player.BoundingBox.Left < walls.ElementAt(i).BoundingBox.Right)
                    {
                        return true;
                    }

                }
            }

            return false;
        }
    }
}
