using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Blocks.Classes
{
    class Actor
    {
        #region Fields
        public static List<Actor> actors;

        protected Vector2 position;
        protected Vector2 velocity;
        private Color color;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        #endregion

        #region Constructors
        static Actor()
        {
            actors = new List<Actor>();
        }

        public Actor()
        {
            Actor.actors.Add(this);
        }
        #endregion

        #region Methods
        #region virtual Update
        public virtual void Update(GameTime gameTime)
        {
        }
        #endregion

        #region virtual Draw
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
        #endregion

        #region static DrawActors
        public static void DrawActors(SpriteBatch spriteBatch)
        {
            for (int i = actors.Count - 1; i >= 0; i--)
                actors[i].Draw(spriteBatch);
        }

        #endregion

        #region virutal ProcessCollision
        public virtual void ProcessCollision()
        {
        }
        #endregion

        #region Move(float amount)
        public void Move(Vector2 amount)
        {
            this.position += amount;
        }
        #endregion

        #region Move
        public void Move()
        {
            this.position += velocity;
        }
        #endregion

        #region SetRandomPosition
        public void SetRandomPosition()
        {
            //this.position =  new Vector2(BattleBlocks.random.Next(Button.BUTTON_RADIUS, BattleBlocks.SCREEN_WIDTH - Button.BUTTON_RADIUS), 
            //    BattleBlocks.random.Next(Button.BUTTON_RADIUS, BattleBlocks.SCREEN_HEIGHT - Button.BUTTON_RADIUS));
        }
        #endregion

        #region SetRandomDirection
        public void SetRandomDirection(float speed)
        {
            float rotation = (float)(BattleBlocks.random.NextDouble() * (Math.PI * 2));
            this.velocity = new Vector2((float)Math.Sin(rotation), (float)Math.Cos(rotation)) * speed;
        }
        
        #endregion
        #endregion
    }
}