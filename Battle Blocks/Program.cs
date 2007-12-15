using System;

namespace Battle_Blocks
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BattleBlocks game = new BattleBlocks())
            {
                game.Run();
            }
        }
    }
}

