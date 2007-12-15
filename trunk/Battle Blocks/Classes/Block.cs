using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Blocks.Classes
{
    class Block : Actor
    {
        #region Fields
        public static Texture2D texture;
        private int width;
        private int height;
        public float[] CornerMarkes;
        private bool isDestructable = true;
        private int health = 100;
        #endregion

        #region Properties
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)(this.Position.X - this.Origin.X),
                    (int)(this.Position.Y - this.Origin.Y),
                    this.Width,
                    this.Height);
            }
        }

        public Vector2 Origin
        {
            get 
            {
                return new Vector2(this.Width / 2, this.Height / 2);
            }
        }

        public bool IsDestructable
        {
            get { return isDestructable; }
            set { isDestructable = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        #endregion

        #region Constructors
        public Block(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            Vector2[] corners = new Vector2[4];
            corners[0] = new Vector2(this.Position.X - this.Origin.X, this.Position.Y - this.Origin.Y);
            corners[1] = new Vector2(this.Position.X + this.Origin.X, this.Position.Y - this.Origin.Y);
            corners[2] = new Vector2(this.Position.X + this.Origin.X, this.Position.Y + this.Origin.Y);
            corners[3] = new Vector2(this.Position.X - this.Origin.X, this.Position.Y + this.Origin.Y);


            this.CornerMarkes = new float[4];
            for (int i = 1; i < CornerMarkes.Length; i++)
            {
                Vector2 direction = Vector2.Normalize(corners[i] - this.Position);
                this.CornerMarkes[i] = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.Pi;
            }

            this.Color = new Color(255, 255, 0);
        }
        #endregion

        #region Methods
        #region override Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, this.Bounds, null, this.Color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }
        #endregion

        #region override ProcessCollision
        public override void ProcessCollision()
        {
            if (this.isDestructable)
            {
                this.Health -= 25;

                byte g = (byte)MathHelper.Lerp(0, 255, (float)this.Health / 100);
                this.Color = new Color(255, g, 0, 255);

                if (this.Health <= 0)
                {
                    actors.Remove(this);
                }
            }
        }
        #endregion
        #endregion
    }
}
