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
    class Wall
    {
        public Texture2D WallTexture;
        public Vector2 WallPosition;
        public Vector2 WallCenter;

        public Vector2 WallLocation()
        {
            return WallPosition;
        }

        public int Width
        {
            get { return WallTexture.Width; }
        }

        //Height of player
        public int Height
        {
            get { return WallTexture.Height; }
        }

        public BoundingSphere WallSphere
        {
            get
            {
                return new BoundingSphere(
                    new Vector3(
                        WallPosition.X + (0.5f * WallTexture.Width),
                        WallPosition.Y + (0.5f * WallTexture.Height),
                        0f
                    ),
                    (float)Radius
                );
            }
        }

        public double Radius
        {
            get
            {
                return Math.Sqrt(
                    ((Width*0.5f)*(Width*0.5f)) + 
                    ((Width*0.5f)*(Width*0.5f))
                    );
            }
        }
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)WallPosition.X,
                    (int)WallPosition.Y,
                    WallTexture.Width,
                    WallTexture.Height);
            }
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            WallTexture = texture;
            WallPosition = position;
            float CenterX = WallPosition.X + (0.5f*WallTexture.Width);
            float CenterY = WallPosition.Y + (0.5f*WallTexture.Height);
            WallCenter.X = CenterX;
            WallCenter.Y = CenterY;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 scale)
        {
            Rectangle r = new Rectangle(0, 0, WallTexture.Width, WallTexture.Height);
            spriteBatch.Draw(WallTexture, WallPosition, r, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
