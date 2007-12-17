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
        public const int SCREEN_WIDTH = 800;
        public const int SCREEN_HEIGHT = 600;
        public static Random random;
        SpriteFont font;
       
        public const float BALL_SPEED_DEFAULT = 5f;
        public const int BLOCK_WIDTH = 25;
       
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
//            TriggerBlock triggerBlock = new TriggerBlock(32, 32);
  //          triggerBlock.Position = new Vector2(20, SCREEN_HEIGHT + triggerBlock.Origin.Y);


            
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
            font = Content.Load<SpriteFont>("Font");

            this.StartGame();
            this.StartLevel();
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
            InputHelper.Update();
            Vibration.UpdateVibrations(gameTime);

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //if (InputHelper.GamePadXJustPressed)
            //{
            //    this.StartNewBall(PlayerIndex.One);
            //    this.StartNewBall(PlayerIndex.Two); ;
            //}

            Player.players[PlayerIndex.One].Update(gameTime);
            Player.players[PlayerIndex.Two].Update(gameTime);

            for (int i = Actor.actors.Count - 1; i >= 0; i--)
            {
                if (i >= Actor.actors.Count)
                    continue;
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
            DrawTextCentered(spriteBatch, font, new Vector2(SCREEN_WIDTH / 4, 45), Player.players[PlayerIndex.One].Score.ToString(), Color.White);
            DrawTextCentered(spriteBatch, font, new Vector2(SCREEN_WIDTH / 4 * 3, 45), Player.players[PlayerIndex.Two].Score.ToString(), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        #region Helpers
        #region StartLevel
        private void StartLevel()
        {
            int blockHeight = SCREEN_HEIGHT / 6;
            float offsetX = SCREEN_WIDTH / 2 - BLOCK_WIDTH * 3 / 2;

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Block block = new Block(BLOCK_WIDTH, blockHeight);
                    block.Position = new Vector2(offsetX + x * BLOCK_WIDTH, y * blockHeight + block.Origin.Y);
                }
            }

            StartNewBall(PlayerIndex.One);
            StartNewBall(PlayerIndex.Two);
        }
        #endregion

        #region StartGame
        private void StartGame()
        {
            Player.players.Clear();
            Player.players[PlayerIndex.One] = new Player(PlayerIndex.One, Color.DarkGreen);
            Player.players[PlayerIndex.Two] = new Player(PlayerIndex.Two, Color.Red);
        }
        #endregion

        #region StartNewBall
        public static  void StartNewBall(PlayerIndex playerIndex)
        {
            Ball ball = new Ball(playerIndex);
            ball.Color = Player.players[playerIndex].Color;
            ball.Position = Player.players[playerIndex].paddle.BallStartPosition;

            float angle = 0;
            switch (playerIndex)
            {
                
                case PlayerIndex.One:
                    angle = Range(MathHelper.ToRadians(45), MathHelper.ToRadians(135));
                    break;
                case PlayerIndex.Two:
                    angle = Range(MathHelper.ToRadians(360 - 135), MathHelper.ToRadians(360 - 45));
                    break;
                default:
                    break;
            }
            ball.Velocity = new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle)) * BALL_SPEED_DEFAULT;
        }
        #endregion  

        #region Range
        public static float Range(float min, float max)
        {
            return (float)random.NextDouble() * (max - min) + min;
        }
        #endregion

        #region Draw Text Centered
        public static void DrawTextCentered(SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 position, string text, Color color)
        {
            Vector2 positionCentered =  position - spriteFont.MeasureString(text) / 2;
            spriteBatch.DrawString(spriteFont, text, positionCentered, color);
        }
        #endregion
        #endregion
    }
}
