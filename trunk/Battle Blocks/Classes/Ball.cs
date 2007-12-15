using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Blocks.Classes
{
    class Ball : Actor
    {
        #region Fields
        public static Texture2D texture;
        #endregion

        #region Properties
        public float Radius
        {
            get { return texture.Width / 2; }
        }

        public Rectangle Bounds
        {
            get
            {
               return new Rectangle((int)(this.position.X - this.Radius),
              (int)(this.position.Y - this.Radius),
              (int)this.Radius * 2,
              (int)this.Radius * 2);
            }
        }
        #endregion

        #region Constructors
        public Ball()
        {
            this.Color = Color.Orange;
        }
        #endregion

        #region Methods
        #region override Update
        public override void Update(GameTime gameTime)
        {
            this.CheckBallCollision();  
            this.CheckWallCollision();
            this.CheckBlockCollision();
            this.Move();
        }
        #endregion

        #region Check Ball Collision
        private void CheckBallCollision()
        {

            for (int i = actors.Count - 1; i >= 0; i--)
            {
                Actor actor = actors[i];
                if (actor == this) continue;

                Ball ballA = this as Ball;
                Ball ballB = actor as Ball;

                if (ballA == null || ballB == null) continue;

                float overlap = ballA.Radius * 2 - Vector2.Distance(ballA.Position, ballB.Position);
                if (overlap > 0f)
                {
                    Vector2 velocityA = ballA.Velocity;
                    ballA.Velocity = ballB.Velocity;
                    ballB.Velocity = velocityA;

                    Vector2 direction = Vector2.Normalize(ballA.Position - ballB.Position);
                    ballA.Move(direction * overlap / 2);
                    ballB.Move(-direction * overlap / 2);

                }

            }
        }
        #endregion

        #region override Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, this.position, null, this.Color, 0f, new Vector2(this.Radius), 1f, SpriteEffects.None, 0f);
        }
        #endregion

        #region Check Wall Collision
        private void CheckWallCollision()
        {
            if (this.position.X < this.Radius)
            {
                this.position.X = this.Radius;
                this.velocity.X *= -1;
            }

            if (this.position.X > BattleBlocks.SCREEN_WIDTH - this.Radius)
            {
                this.position.X = BattleBlocks.SCREEN_WIDTH - this.Radius;
                this.velocity.X *= -1;
            }

            if (this.position.Y < this.Radius)
            {
                this.position.Y = this.Radius;
                this.velocity.Y *= -1;
            }

            if (this.position.Y > BattleBlocks.SCREEN_HEIGHT - this.Radius)
            {
                this.position.Y = BattleBlocks.SCREEN_HEIGHT - this.Radius;
                this.velocity.Y *= -1;
            }
        }
        #endregion

        #region CheckBlockCollision
        private void CheckBlockCollision()
        {
            for (int i = actors.Count - 1; i >= 0; i--)
            {
                Block block = actors[i] as Block;
                if (block == null) continue;

                if (this.Bounds.Intersects(block.Bounds))
                {
                    Vector2 direction = Vector2.Normalize(this.Position - block.Position);
                    float angle = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.Pi;

                    if (angle >= block.CornerMarkes[0] && angle <= block.CornerMarkes[1])
                    {
                        this.velocity.Y = -Math.Abs(this.velocity.Y);
                    } 
                    else if (angle >= block.CornerMarkes[1] && angle <= block.CornerMarkes[2])
                    {
                        this.velocity.X = Math.Abs(this.velocity.X);
                    }
                    else if (angle >= block.CornerMarkes[2] && angle <= block.CornerMarkes[3])
                    {
                        this.velocity.Y = Math.Abs(this.velocity.Y);
                    }
                    else
                    {
                        this.velocity.X = -Math.Abs(this.velocity.X);
                    }
                    block.ProcessCollision();
                }
            }
        }
        #endregion
        #endregion
        
    }
}
