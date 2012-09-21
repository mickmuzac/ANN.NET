/*

	By: Mick Muzac and Didier Lassage



*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Drawing;

namespace FirstGame
{
    class Player
    {
        //The player itself
        public Texture2D PlayerTexture;

        //Position of player
        public Vector2 Position;
        public Vector2 CenterPosition;

        //State of the player
        public bool Active;

        //Health of player
        public int Health;

        //Color of circle
        public Color circleColor;

        Vector2[] startPoints;
        Vector2[] endPoints;
        Vector2 leftradar;
        Vector2 rightradar;
        Vector2 topradar;
        Vector2 bottomradar;
        Vector2 leftradare;
        Vector2 rightradare;
        Vector2 topradare;
        Vector2 bottomradare;
        public double[] lengths;
        Color[] colors;

        int totalLength = 80;

        public float rotationAngle;

        public double bottomrightlevel;
        public double topleftlevel;
        public double bottomleftlevel;
        public double toprightlevel;
        public double cbottomrightlevel;
        public double ctopleftlevel;
        public double cbottomleftlevel;
        public double ctoprightlevel;
        public float playerslope;
        public float enemyslope;
        public float abep;
        public double x;
        public double y;
        public float distance;
        public float xdistance;
        public float ydistance;
        public float relativeAngle = 0;
        public bool seeking = false;

        public Texture2D RadarTexture;
        public Vector2 RadarPosition;

        //Width of player
        public int Width
        {
            get { return PlayerTexture.Width; }
        }

        public void reset(Vector2 pos)
        {

           Position = pos;
           rotationAngle = 0; 
        }

        //Height of player
        public int Height
        {
            get { return PlayerTexture.Height; }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    PlayerTexture.Width,
                    PlayerTexture.Height);
            }
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            PlayerTexture = texture;
            Position = position;
            CenterPosition = new Vector2(position.X+.5f*texture.Height, position.Y + .5f * texture.Height);
            Active = true;
            Health = 100;
            circleColor = Color.Green;
            endPoints = new Vector2[3];
            startPoints = new Vector2[3];
            lengths = new double[3];
            colors = new Color[3];

            bottomrightlevel = 0;
            topleftlevel = 0;
            bottomleftlevel = 0;
            toprightlevel = 0;
            RadarPosition = position;
            RadarTexture = texture;

            for (int i = 0; i < 3; i++)
            {
                colors[i] = Color.Black;
                endPoints[i] = new Vector2();
                startPoints[i] = new Vector2();
                lengths[i] = 0;
            }

            rotationAngle = 0;
        }

        public void seek(Vector2 loc)
        {
            if (seeking)
            {
                Vector2 temp;
                temp.X = loc.X - Position.X;
                temp.Y = loc.Y - Position.Y;

                if (temp.Length() < 50)
                    seeking = false;

                temp.Normalize();

                Position.Y += (float)2 * temp.Y;//(float)Math.Cos();
                Position.X += (float)2 * temp.X;//(float)Math.Sin();

            }
        }

        public void setCenter()
        {

            CenterPosition.X = Position.X + .5f * PlayerTexture.Width;
            CenterPosition.Y = Position.Y + .5f * PlayerTexture.Height;
        }

        public void Update(WallManager walls)
        {

            for (int i = 0; i < 3; i++)
                colors[i] = Color.Black;

            startPoints[0].X = CenterPosition.X + (float)Math.Sin(rotationAngle) * PlayerTexture.Width *.5f;
            startPoints[0].Y = CenterPosition.Y - (float)Math.Cos(rotationAngle) * PlayerTexture.Height * .5f;

            startPoints[1].X = CenterPosition.X - (float)(Math.Cos(rotationAngle + Math.PI / 4) * PlayerTexture.Width * .5f * Math.Sqrt(2));
            startPoints[1].Y = CenterPosition.Y - (float)(Math.Sin(rotationAngle + Math.PI / 4) * PlayerTexture.Height * .5f * Math.Sqrt(2));

            startPoints[2].X = CenterPosition.X + (float)(Math.Sin(rotationAngle + Math.PI / 4) * PlayerTexture.Width * .5f * Math.Sqrt(2));
            startPoints[2].Y = CenterPosition.Y - (float)(Math.Cos(rotationAngle + Math.PI / 4) * PlayerTexture.Height * .5f * Math.Sqrt(2));

            topradar.X = CenterPosition.X + (float)Math.Sin(rotationAngle) * PlayerTexture.Width * .5f;
            topradar.Y = CenterPosition.Y - (float)Math.Cos(rotationAngle) * PlayerTexture.Height * .5f;
            topradare.X = topradar.X + (75 * (float)Math.Sin(rotationAngle));
            topradare.Y = topradar.Y + (75 * (float)(-Math.Cos(rotationAngle)));

            leftradar.X = CenterPosition.X - (float)(Math.Cos(rotationAngle) * PlayerTexture.Width * .5f);
            leftradar.Y = CenterPosition.Y - (float)(Math.Sin(rotationAngle) * PlayerTexture.Height * .5f);
            leftradare.X = leftradar.X - (75 * (float)(Math.Cos(rotationAngle)));
            leftradare.Y = leftradar.Y - (75 * (float)(Math.Sin(rotationAngle)));

            rightradar.X = CenterPosition.X + (float)(Math.Cos(rotationAngle) * PlayerTexture.Width * .5f);
            rightradar.Y = CenterPosition.Y + (float)(Math.Sin(rotationAngle) * PlayerTexture.Height * .5f);
            rightradare.X = rightradar.X + (75 * (float)(Math.Cos(rotationAngle)));
            rightradare.Y = rightradar.Y + (75 * (float)(Math.Sin(rotationAngle)));

            bottomradar.X = CenterPosition.X - (float)Math.Sin(rotationAngle) * PlayerTexture.Width * .5f; ;
            bottomradar.Y = CenterPosition.Y + (float)Math.Cos(rotationAngle) * PlayerTexture.Height * .5f; ;
            bottomradare.X = bottomradar.X - (75 * (float)Math.Sin(rotationAngle));
            bottomradare.Y = bottomradar.Y - (75 * (float)(-Math.Cos(rotationAngle)));
           int tempLength;
           float yMod;
           float xMod;
           
            for (int feeler = 0; feeler < 3; feeler++){

                tempLength = totalLength;

                if (feeler == 0)
                {
                    xMod = (float)Math.Sin(rotationAngle);
                    yMod = (float)(-Math.Cos(rotationAngle));
                }

                else if (feeler == 1)
                {
                    xMod = (float)(-Math.Cos(rotationAngle + Math.PI / 4));
                    yMod = (float)(-Math.Sin(rotationAngle + Math.PI / 4));
                }

                else
                {
                    xMod = (float)(Math.Sin(rotationAngle + Math.PI / 4));
                    yMod = (float)(-Math.Cos(rotationAngle + Math.PI / 4));

                }

                for (int i = 0; i < walls.walls.Count; i++)
                {     
                    for (int j = 0; j < totalLength; j++)
                    {
                        endPoints[feeler].X = startPoints[feeler].X + j * xMod;
                        endPoints[feeler].Y = startPoints[feeler].Y + j * yMod;

                        if (walls.walls.ElementAt(i).BoundingBox.Contains((int)endPoints[feeler].X, (int)endPoints[feeler].Y))
                        {
                            tempLength = j;
                            colors[feeler] = Color.Green;
                            lengths[feeler] = j;
                            break;
                        }
                    }
                }

                lengths[feeler] = tempLength;
                endPoints[feeler].X = startPoints[feeler].X + tempLength * xMod;
                endPoints[feeler].Y = startPoints[feeler].Y + tempLength * yMod;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {

            DrawingHelper.DrawFastLine(topradar, topradare, Color.Red);
            DrawingHelper.DrawFastLine(bottomradar, bottomradare, Color.Red);
            DrawingHelper.DrawFastLine(leftradar, leftradare, Color.Red);
            DrawingHelper.DrawFastLine(rightradar, rightradare, Color.Red);

            for(int i = 0; i < 3; i++)
                DrawingHelper.DrawFastLine(startPoints[i], endPoints[i], colors[i]);

            //Draw the text

            DrawingHelper.DrawCircle(CenterPosition, 100, circleColor, false);
            spriteBatch.Draw(PlayerTexture, new Vector2(Position.X + 25, Position.Y+25), null, Color.White, 
                rotationAngle, new Vector2(25,25), 1f, SpriteEffects.None, 0f);

        }

        public void determineActivations(List<Food> enemy, Player player, float playerAngle, FoodManager foodManager)
        {

            float distance2;
            float sinx;
            float cosy;

            cbottomrightlevel = 0;
            ctopleftlevel = 0;
            ctoprightlevel = 0;
            cbottomleftlevel = 0;
            bottomrightlevel = 0;
            topleftlevel = 0;
            toprightlevel = 0;
            bottomleftlevel = 0;

            if (enemy.Count < 1)
                return;

            playerslope = (float)Math.Tan((double)playerAngle);

            for (int i = 0; i < enemy.Count; i++)
            {
                Food current = enemy.ElementAt(i);

                if (current.relativeAngle >= 0 && current.relativeAngle < 90)
                {
                    ctopleftlevel++;
                }
                else if (current.relativeAngle >= 90 && current.relativeAngle < 180)
                {
                    cbottomleftlevel++;
                }
                else if (current.relativeAngle >= 180 && current.relativeAngle < 270)
                {
                    cbottomrightlevel++;
                }
                else
                    ctoprightlevel++;
            }

            if (ctoprightlevel != toprightlevel)
                toprightlevel = ctoprightlevel;
            if (cbottomrightlevel != bottomrightlevel)
                bottomrightlevel = cbottomrightlevel;
            if (ctopleftlevel != topleftlevel)
                topleftlevel = ctopleftlevel;
            if (cbottomleftlevel != bottomleftlevel)
                bottomleftlevel = cbottomleftlevel;
        }
    }
}
