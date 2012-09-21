using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FirstGame
{
    class Feelers
    {
        //The feeler itself
        public Texture2D FeelerTexture;

        //Position of feeler
        public Vector2 Position;

        //State of the player
        public bool Active;

        public float rightfeelerdistance;
        public float middlefeelerdistance;
        public float leftfeelerdistance;

        //Width of player
        public int Width
        {
            get { return FeelerTexture.Width; }
        }

        //Height of player
        public int Height
        {
            get { return FeelerTexture.Height; }
        }

        public BoundingSphere LeftCircle
        {
            get
            {
                return new BoundingSphere(
                    new Vector3(
                        Position.X + 25,
                        Position.Y + 25,
                        0f),
                    25f);
            }
        }
            
        public BoundingSphere MiddleCircle
        {
            get
            {
                return new BoundingSphere(
                    new Vector3(
                        Position.X + 75,
                        Position.Y + 25,
                        0f),
                    25f);
            }
        }
            
        public BoundingSphere RightCircle
        {
            get
            {
                return new BoundingSphere(
                    new Vector3(
                        Position.X + 125,
                        Position.Y + 25,
                        0f),
                    25f);
            }

        }
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    FeelerTexture.Width,
                    FeelerTexture.Height);
            }
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            FeelerTexture = texture;
            Position = position;
            Active = true;
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch, float RotationAngle, Player player)
        {
            Vector2 center = player.CenterPosition;
            //spriteBatch.Draw(FeelerTexture, new Vector2(Position.X+75, Position.Y+75), null, Color.White,
            //    RotationAngle, new Vector2(75, 75), 1f, SpriteEffects.None, 0f);
        }
    }
}
