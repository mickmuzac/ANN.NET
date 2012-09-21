using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Drawing;

namespace FirstGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PlayerManager player;
        Wall wall;
        Wall wall2;
        Feelers feeler;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        SpriteFont font;
        Radar radar;
        Vector2 scale;
        Vector2 foodLocation;
        NetworkManager network;
        double [] inputs;
        double [] output;
        int seed = unchecked(DateTime.Now.Ticks.GetHashCode());
        Random r;
        GenomeManager genomeManager = new GenomeManager();
        int ticks = 0;
        MouseState mouseState;
        float minusPos = 0;
        int training = 0;
        float playerMoveSpeed;
        float feelermoveSpeed;
        String text = "";
       // public float player.rotationAngle = 0f;

        //NEW
        FoodManager foodManager;
        WallManager wallManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            r = new Random();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new PlayerManager();
            playerMoveSpeed = 5.0f;

            wall = new Wall();
            wall2 = new Wall();

            feeler = new Feelers();
         
            feelermoveSpeed = 5.0f;

            radar = new Radar();

            this.IsMouseVisible = true;
            DrawingHelper.Initialize(graphics.GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load the player resources
            Vector2 playerPosition = new Vector2(400, 450);

            //Load the wall resources
            Vector2 wallPosition = new Vector2(190.0f, 240.0f);
            scale = new Vector2(1, 1);

            //Load the feeler resources
            Vector2 feelerPosition = new Vector2(playerPosition.X - 55, playerPosition.Y - 50);

            
            wall.Initialize(Content.Load<Texture2D>("bigwall"), wallPosition);
            feeler.Initialize(Content.Load<Texture2D>("3feelers"), feelerPosition);

            
           // radar.Initialize(Content.Load<Texture2D>("Radar"), playerPosition);

            //NEW:
            foodManager = new FoodManager(Content.Load<Texture2D>("food"));
            wallManager = new WallManager();
            wallManager.addWall(wall);

            foodLocation = new Vector2(300, 100);

            foodManager.addFood(foodLocation.X, foodLocation.Y);

            //Load the text
            font = Content.Load<SpriteFont>("myFont");

            inputs = new double[6];
            output = new double[2];

            network = new NetworkManager();

            for (int i = 0; i < 20; i++)
            {
                network.addNetwork(6, this.r);
                player.addPlayer();
            }

            player.Initialize(Content.Load<Texture2D>("theplayer"), playerPosition);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            ticks++;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Save previous state of keyboard so we can determine single key/button presses
            previousKeyboardState = currentKeyboardState;

            //Read the current state of the keyboard and store it
            currentKeyboardState = Keyboard.GetState();

            //Read the current state of the mouse
            mouseState = Mouse.GetState();
            
            for(int g = 0; g < player.players.Count; g++)
                foodManager.Update(gameTime, mouseState, player.players.ElementAt(g).rotationAngle);

            //Update the player
            UpdatePlayer(gameTime);

            base.Update(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            
            /*if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                // The time since Update was called last.
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

                // TODO: Add your game logic here.
                player.rotationAngle -= elapsed;
                float circle = MathHelper.Pi * 2;
                player.rotationAngle = player.rotationAngle % circle;

            }*/

            if (previousKeyboardState.IsKeyDown(Keys.Space) && currentKeyboardState.IsKeyUp(Keys.Space))
            {
                System.IO.File.WriteAllText(@"C:\Users\Public\WriteText.txt", network.s);
                network.s = "";
                training = 1;
            }

            if (previousKeyboardState.IsKeyDown(Keys.Up) && currentKeyboardState.IsKeyUp(Keys.Up))
            {
                minusPos -= 100;
                //player.players[0].seeking = !player.players[0].seeking;
                
            }

            else if (previousKeyboardState.IsKeyDown(Keys.Down) && currentKeyboardState.IsKeyUp(Keys.Down))
            {
                minusPos += 100;
            }

            player.players[0].seek(foodManager.foods[0].location);

            for (int g = 0; g < player.players.Count; g++)
            {
                //Prevent player from leaving screen
                player.players[g].Position.X = MathHelper.Clamp(player.players[g].Position.X,
                0, GraphicsDevice.Viewport.Width - player.players[g].Width);
                player.players[g].Position.Y = MathHelper.Clamp(player.players[g].Position.Y,
                0, GraphicsDevice.Viewport.Height - player.players[g].Height);

                player.players[g].setCenter();

                List<Food> temp = foodManager.foodCircle(player.players[g].CenterPosition, 500, player.players[g].rotationAngle);
                player.players[g].determineActivations(temp, player.players[g], player.players[g].rotationAngle, foodManager);

                

                //////////////////////////

                inputs[0] = player.players[g].topleftlevel - player.players[g].bottomleftlevel;
                inputs[1] = player.players[g].toprightlevel - player.players[g].bottomrightlevel;

                inputs[2] = (.5 - player.players[g].lengths[0] * .0125)*2;
                inputs[3] = (.5 - player.players[g].lengths[1] * .0125)*2;
                inputs[4] = (.5 - player.players[g].lengths[2] * .0125)*2;

                inputs[5] = -1;

                output = network.update(g, inputs);

                float maxAngle = 0.5f;
                if (output[1] > -maxAngle && output[1] < maxAngle)
                    output[1] = 0;
                else if (output[1] > maxAngle)
                    output[1] = 1;
                else output[1] = -1;

                player.players[g].rotationAngle += (float)(output[1]) * .01f;

                if (output[0] > 0)
                {
                    player.players[g].Position.Y -= (float)output[0] * (float)Math.Cos(player.players[g].rotationAngle);
                    if (wallManager.detectCollision(player.players[g]))
                        player.players[g].Position.Y += (float)output[0] * (float)Math.Cos(player.players[g].rotationAngle);

                    player.players[g].Position.X += (float)output[0] * (float)Math.Sin(player.players[g].rotationAngle);
                    if (wallManager.detectCollision(player.players[g]))
                        player.players[g].Position.X -= (float)output[0] * (float)Math.Sin(player.players[g].rotationAngle);

                }
                
               if (output[0] < 0)
                {
                    player.players[g].Position.Y += (float)output[0] * (float)Math.Cos(player.players[g].rotationAngle);
                    if (wallManager.detectCollision(player.players[g]))
                        player.players[g].Position.Y -= (float)output[0] * (float)Math.Cos(player.players[g].rotationAngle);

                    player.players[g].Position.X -= (float)output[0] * (float)Math.Sin(player.players[g].rotationAngle);
                    if (wallManager.detectCollision(player.players[g]))
                        player.players[g].Position.X += (float)output[0] * (float)Math.Sin(player.players[g].rotationAngle);
                }


                if (network.networks[g].done == 1 || (Math.Abs(foodManager.foods[0].location.X - player.players[g].Position.X) < 30 &&
                   Math.Abs(foodManager.foods[0].location.Y - player.players[g].Position.Y) < 30))
                    network.networks[g].done = 1;

                else
                    network.networks.ElementAt(g).fitness =
                    genomeManager.getFitness(player.players[g], inputs, foodManager.foods[0].location, network.networks[g]);

                if (ticks % 1500 == 0)
                {
                    ticks = 1;
                    player.rPos.X += minusPos;
                    foodManager.foods[0].location.X += minusPos;
                    network.die(training);
                    player.die();

                    minusPos = 0;
                    
                }

                //////////////////////////


                if (temp.Count > 0)
                {
                    player.players[g].circleColor = Color.Red;

                }
                else
                    player.players[g].circleColor = Color.Green;

                player.players[g].Update(wallManager);
            }
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Start drawing
            spriteBatch.Begin();

            //Draw the wall
            wall.Draw(spriteBatch, scale);
           // wall2.Draw(spriteBatch);

            //Draw the feeler
            
            for (int g = 0; g < player.players.Count; g++)
            {
                

                if(training == 0 || network.networks[g].done == 1){

                    player.players[g].Draw(spriteBatch, font);
                    spriteBatch.DrawString(font, "" + network.networks[g].fitness + ":xDied:" +
                    network.networks[g].timesDead + ":xMutate:" + network.networks[g].mutation + ":output:" +
                    network.networks[g].output[1], player.players[g].Position, Color.Black);
                }

                //feeler.Draw(spriteBatch, player.players[g].rotationAngle, player.players[g]);
            }
            
            //Draw the food/enemies or whatever
            foodManager.Draw(spriteBatch, font);
            spriteBatch.DrawString(font, "Total Ticks: " + network.networks.ElementAt(0).life, new Vector2(20, 130), Color.Black);
            spriteBatch.DrawString(font, "Epoch Ticks: 1500", new Vector2(20, 100), Color.Black);
            
            //Stop drawing
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
