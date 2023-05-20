using System;

namespace PaintRush.World
{
    [Serializable]
    public class Vars
    {
        public static Vars Instance;

        public float BulletDamageMultiplayer = 1.0f;
        public float CoinValueMultiplayer = 1.0f;
        public int Balance = 0;
        public int Level = 0;

        public Vars()
        {
            Instance = this;
        }
    }
}