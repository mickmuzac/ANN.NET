/*

	By: Mick Muzac and Didier Lassage



*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FirstGame
{
    class FoodManager
    {

        ButtonState lastState;
       public List<Food> foods = new List<Food>(100);
        Texture2D texture;


        public FoodManager(Texture2D t)
        {

            texture = t;
        }

        public void addFood(float x, float y)
        {

            foods.Add(new Food(x, y));
        }

        public void Draw(SpriteBatch s, SpriteFont f){

            int length = foods.Count;

            for (int i = 0; i < length; i++ )
            {
                s.Draw(texture, foods.ElementAt(i).getLocation(), Color.White);
            }
        }

        public void Update(GameTime t, MouseState m, float pAngle)
        {

            int length = 1;

        }

        public List<Food> foodCircle(Vector2 location, float radius, float playerAngle){

            List<Food> tempList = new List<Food>(100);
            int count = foods.Count;
            Food tFood;
            Vector2 botRight;
            Vector2 topRight;
            Vector2 botLeft;
            Vector2 topLeft;
            Vector2 foodCenter;
            float tempDegrees = 0;


            for (int i = 0; i < count; i++)
            {
                tFood = foods.ElementAt(i);

                tFood.playerDistance = 0;
                tFood.relativeAngle = 0;

                botRight.X = tFood.getLocation().X+texture.Width;
                botRight.Y = tFood.getLocation().Y+texture.Height;
                topRight.X = tFood.getLocation().X+texture.Width;
                topRight.Y = tFood.getLocation().Y;
                botLeft.X = tFood.getLocation().X;
                botLeft.Y = tFood.getLocation().Y+texture.Height;
                foodCenter.X = tFood.getLocation().X + .5f * texture.Width;
                foodCenter.Y = tFood.getLocation().Y + .5f * texture.Height;
                topLeft = tFood.getLocation();


                if (getDistance(topLeft, location) <= radius ||
                    getDistance(botLeft, location) <= radius ||
                    getDistance(topRight, location) <= radius ||
                    getDistance(botRight, location) <= radius)
                {


                    tempDegrees = (MathHelper.ToDegrees(playerAngle) + 360) % 360 + 90;

                    tFood.relativeAngle = Math.Abs((MathHelper.ToDegrees(getAngle(location, foodCenter)) - tempDegrees - 360) % 360);
                    tFood.playerDistance = (int)getDistance(location, foodCenter);

                    tempList.Add(tFood);
                    continue;
                }

                botLeft.X += 0.5f*texture.Width;
                topRight.X -= 0.5f*texture.Width;
                botRight.Y -= 0.5f * texture.Height;
                topLeft.Y += 0.5f * texture.Height;

                if (getDistance(topLeft, location) <= radius ||
                    getDistance(botLeft, location) <= radius ||
                    getDistance(topRight, location) <= radius ||
                    getDistance(botRight, location) <= radius)
                {

                    tempDegrees = (MathHelper.ToDegrees(playerAngle) + 360) % 360 + 90;

                    tFood.relativeAngle = Math.Abs(
                        (MathHelper.ToDegrees(getAngle(location, foodCenter))- tempDegrees - 360) % 360);

                    tFood.playerDistance = (int)getDistance(location, foodCenter);

                    tempList.Add(tFood);
                    continue;
                }


            }

            return tempList;
        }

        public float getDistance(Vector2 a, Vector2 b)
        {

            return (float)Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        public float getWidth()
        {

            return texture.Width;
        }

        public float getHeight()
        {
            return texture.Height;
        }

        public bool collides(Rectangle rect)
        {
            Food food;

            for (int i = 0; i < foods.Count; i++ )
            {

                food = foods.ElementAt(i);
                if(rect.Intersects(new Rectangle((int)food.getLocation().X, 
                  (int)food.getLocation().Y, texture.Width, texture.Height)))
                    return true;

            }

            return false;
        }

        public float getAngle(Vector2 a, Vector2 b)
        {

            float rX = 0;
            float rY = 0;

            rX = b.X - a.X;
            rY = b.Y - a.Y;

            if (a.X > b.X)
                return (float)(Math.Atan(rY / rX));

            else if (a.X < b.X)
                return (float)(Math.PI + Math.Atan(rY / rX));

            else if (a.X == b.X)
                return -(float)(Math.Atan(rY / rX));

            return (float)Math.Atan(rY / rX);
        }

    }
}
