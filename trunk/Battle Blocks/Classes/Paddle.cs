using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Blocks.Classes
{
    class Paddle : Actor
    {
        #region Fields
        public static Texture2D texture;
        public float[] CornerMarkes;
        public PlayerIndex playerIndex;
        #endregion

        #region Properties
        public Vector2 Origin
        {
            get { return new Vector2(texture.Width / 2, texture.Height /2); }
        }
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)(this.Position.X - this.Origin.X),
                    (int)(this.Position.Y - this.Origin.Y),
                    texture.Width,
                    texture.Height);
            }
        }

        public Vector2 BallStartPosition
        {
            get
            {
                switch (playerIndex)
                {
                    case PlayerIndex.One:
                        return this.Position + new Vector2(this.Origin.X * 2, 0);
                    case PlayerIndex.Two:
                        return this.Position - new Vector2(this.Origin.X * 2, 0);
                    default:
                        return Vector2.Zero;
                }
            }
        }
        #endregion

        #region Constructors
        public Paddle(PlayerIndex playerIndex, Color color)
        {
           
            this.Color = color;
            this.playerIndex = playerIndex;

            Vector2[] corners = new Vector2[4];
            corners[0] = new Vector2(this.Position.X - this.Origin.X, this.Position.Y - this.Origin.Y);
            corners[1] = new Vector2(this.Position.X + this.Origin.X, this.Position.Y - this.Origin.Y);
            corners[2] = new Vector2(this.Position.X + this.Origin.X, this.Position.Y + this.Origin.Y);
            corners[3] = new Vector2(this.Position.X - this.Origin.X, this.Position.Y + this.Origin.Y);


            this.CornerMarkes = new float[4];
            for (int i = 00; i < CornerMarkes.Length; i++)
            {
                Vector2 direction = Vector2.Normalize(corners[i] - this.Position);
                this.CornerMarkes[i] = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.Pi;
            }
        }
        #endregion

        #region Methods
        #region override Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, this.Bounds, null, this.Color, 0f, new Vector2(), SpriteEffects.None, 0f);
        }
        #endregion

        #region CheckBallCollision
        private void CheckBallCollision()
        {
            for (int i = Actor.actors.Count - 1; i >= 0; i--)
            {
                Ball ball = actors[i] as Ball;
                if (ball == null) continue;

                if (this.Bounds.Intersects(ball.Bounds))
                {
                    Vector2 direction = Vector2.Normalize(ball.Position - this.Position);
                    float angle = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.Pi;

                    if (angle >= this.CornerMarkes[0] && angle <= this.CornerMarkes[1])
                    {
                        ball.Velocity = new Vector2(ball.Velocity.X, -Math.Abs(ball.Velocity.Y));
                    }
                    else if (angle >= this.CornerMarkes[1] && angle <= this.CornerMarkes[2])
                    {
                        ball.Velocity = new Vector2(Math.Abs(ball.Velocity.X), ball.Velocity.Y);
                    }
                    else if (angle >= this.CornerMarkes[2] && angle <= this.CornerMarkes[3])
                    {
                        ball.Velocity = new Vector2(ball.Velocity.X, Math.Abs(ball.Velocity.Y));
                    }
                    else
                    {
                        ball.Velocity = new Vector2(-Math.Abs(ball.Velocity.X), ball.Velocity.Y);
                    }

                     ball.PlayerIndex = this.playerIndex;
                }
            }
        }
        #endregion

        #region CheckWallCollision
        private void CheckWallCollision()
        {
            if (this.Position.Y > BattleBlocks.SCREEN_HEIGHT - this.Origin.Y)
            {
                this.position.Y = BattleBlocks.SCREEN_HEIGHT - this.Origin.Y;
            }

            if (this.Position.Y < this.Origin.Y)
            {
                this.position.Y = this.Origin.Y;
            }

        }
        #endregion

        #region override Update
        public override void Update(GameTime gameTime)
        {
            this.CheckBallCollision();
            this.CheckWallCollision();
        }
        #endregion
        #endregion
    }
}
