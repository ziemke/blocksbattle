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
        public static Dictionary<PlayerIndex, Player> players;
        public static int lives;

        public Paddle paddle;
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

        public Player this[PlayerIndex playerIndex]
        {
            get { return players[playerIndex]; }
        }
        #endregion

        #region Constructors
        static Player()
        {
            lives = 3;
            players = new Dictionary<PlayerIndex, Player>();
        }

        public Player(PlayerIndex playerIndex, Color color)
        {
            this.playerIndex = playerIndex;
            this.color = color;
            paddle = new Paddle(playerIndex, color);
            this.ResetPaddlePosition();
            paddleSpeed = 5;

            players.Add(playerIndex, this);
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

        #region AddScore
        public static void AddScore(PlayerIndex playerIndex, int score)
        {
            players[playerIndex].Score += score;
        }
        #endregion

        #region Update
        public void Update(GameTime gameTime)
        {
            paddle.Position -= new Vector2(0, GamePad.GetState(playerIndex).ThumbSticks.Left.Y) * paddleSpeed;
            for (int i = Actor.actors.Count - 1; i >= 0; i--)
            {
                Ball ball = Actor.actors[i] as Ball;
                if (ball == null) continue;
                if (ball.PlayerIndex != this.playerIndex) continue;
                ball.speedModifier = MathHelper.Clamp(1 + GamePad.GetState(playerIndex).ThumbSticks.Right.X * 2, 0.3f, 2.5f);
                ball.directionModifier = new Vector2(0, -(GamePad.GetState(playerIndex).ThumbSticks.Right.Y * 5));

                if (GamePad.GetState(playerIndex).Buttons.A == ButtonState.Pressed && ball.Idle)
                    ball.Idle = false;
            }
        }
        #endregion
        #endregion

    }
}
