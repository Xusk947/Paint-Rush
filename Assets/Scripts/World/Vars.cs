using System;

namespace PaintRush.World
{
    /// <summary>
    /// Represents the variables and settings for the game.
    /// </summary>
    [Serializable]
    public class Vars
    {
        /// <summary>
        /// The singleton instance of the Vars class.
        /// </summary>
        public static Vars Instance { get; private set; }

        /// <summary>
        /// The multiplayer value for the coin value.
        /// </summary>
        public float CoinValueMultiplayer { get; set; }

        /// <summary>
        /// The balance of the player.
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// The level of the game.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Creates a new instance of the Vars class.
        /// </summary>
        public Vars()
        {
            Instance = this;
        }
    }
}
