using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Battle_Blocks.Classes;

namespace Battle_Blocks
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BattleBlocks : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public const int SCREEN_WIDTH = 640;
        public const int SCREEN_HEIGHT = 480;
        public static Random random;

        Player player1;
        Player player2;

        public BattleBlocks()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            Content.RootDirectory = "Content";
            random = new Random();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            int blockWidth = 20;
            int blockHeight = SCREEN_HEIGHT / 8;
            float offsetX = SCREEN_WIDTH / 2 - blockWidth * 1.5f;

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Block block = new Block(blockWidth, blockHeight);
                    block.Position = new Vector2(offsetX + x * blockWidth + block.Origin.Y, y * blockHeight + block.Origin.Y);
                }
            }

            TriggerBlock triggerBlock = new TriggerBlock(32, 32);
            triggerBlock.Position = new Vector2(20, SCREEN_HEIGHT + triggerBlock.Origin.Y);

           

            List<Color> colors = new List<Color>();
            colors.Add(Color.PaleTurquoise);
            colors.Add(Color.PaleGoldenrod);
            colors.Add(Color.Lime);
            colors.Add(Color.Orchid);
            colors.Add(Color.Orange);
            colors.Add(Color.MediumSeaGreen);
            colors.Add(Color.Moccasin);
            colors.Add(Color.MistyRose);
            colors.Add(Color.MidnightBlue);
            colors.Add(Color.Linen);
            colors.Add(Color.Lime);
            colors.Add(Color.Orchid);
            colors.Add(Color.Orange);
            colors.Add(Color.MediumSeaGreen);
            colors.Add(Color.Moccasin);
            colors.Add(Color.MistyRose);
            colors.Add(Color.MidnightBlue);
            colors.Add(Color.Linen);
            colors.Add(Color.Maroon);
            colors.Add(Color.Lime);
            colors.Add(Color.Orchid);
            colors.Add(Color.Orange);

            for (int i = 0; i < 2; i++)
			{
			    Ball ball = new Ball();
                

                float fullCircle = MathHelper.Pi * 2;
                float angle = fullCircle * (float)i / 20;

                ball.Position = new Vector2(SCREEN_WIDTH / 2 - 300, SCREEN_HEIGHT / 2 - 20);
                ball.Velocity = new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle)) * 5;
                ball.Position += new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle)) * 20;
                ball.Color = colors[i];
			}

            
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

            Ball.texture = Content.Load<Texture2D>("ball");
            Block.texture = Content.Load<Texture2D>("block");
            Paddle.texture = Content.Load<Texture2D>("paddle");

            player1 = new Player(PlayerIndex.One, Color.DarkGreen);
            player2 = new Player(PlayerIndex.Two, Color.Red);
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Vibration.UpdateVibrations(gameTime);

            player1.Update(gameTime);
            player2.Update(gameTime);

            for (int i = Actor.actors.Count - 1; i >= 0; i--)
            {
                Actor actor = Actor.actors[i];
                actor.Update(gameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            Actor.DrawActors(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
