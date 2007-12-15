using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Battle_Blocks.Classes
{
    class TriggerBlock : Block
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public TriggerBlock(int width, int height) : base(width, height)
        {
            this.IsDestructable = true;
            this.Color = Color.Red;
        }
        #endregion

        #region Methods
        #region override ProcessCollision
        public override void ProcessCollision()
        {
            for (int i = Actor.actors.Count - 1; i >= 0; i--)
            {
                Block block = actors[i] as Block;
                if (block == null) continue;
                block.IsDestructable = true;
                block.Color = new Color(255, 255, 0);

                actors.Remove(this);
            }
        }
        #endregion
        #endregion
    }
}
