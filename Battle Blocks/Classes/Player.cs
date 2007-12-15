using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Blocks.Classes
{
    class Player
    {
        #region Fields
        public static int lives;

        private Paddle paddle;
        private Color color;
        private int score;
        PlayerIndex playerIndex;

        private float paddleSpeed;
        #endregion

        #region Properties
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public float PaddleSpeed
        {
            get { return paddleSpeed; }
            set { paddleSpeed = value; }
        }
        #endregion

        #region Constructors
        static Player()
        {
            lives = 3;
        }

        public Player(PlayerIndex playerIndex, Color color)
        {
            this.playerIndex = playerIndex;
            this.color = color;
            paddle = new Paddle(color);
            this.ResetPaddlePosition();
            paddleSpeed = 5;
        }
        #endregion

        #region Methods
       

        #region ResetPaddlePosition
        public void ResetPaddlePosition()
        {
            float posX = 0;
            switch (playerIndex)
            {
                case PlayerIndex.One:
                    posX = paddle.Origin.X + 15;
                    break;
                case PlayerIndex.Two:
                    posX = BattleBlocks.SCREEN_WIDTH - paddle.Origin.X - 15;
                    break;
                default:
                    break;
            }
           paddle.Position = new Vector2(posX , BattleBlocks.SCREEN_HEIGHT / 2);
        }
        #endregion

        #region Update
        public void Update(GameTime gameTime)
        {
            paddle.Position -= new Vector2(0, GamePad.GetState(playerIndex).ThumbSticks.Left.Y) * paddleSpeed;
        }
        #endregion
        #endregion

    }
}
