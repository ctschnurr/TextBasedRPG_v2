using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    class Settings
    {
        // Player Stats
        public string PlayerIcon { get; set; }
        public string PlayerAttack { get; set; }
        public string PlayerColor { get; set; }

        public int PlayerHealth { get; set; }
        public int PlayerLives { get; set; }
        public int StartingX { get; set; }
        public int StartingY { get; set; }
        public int StartingGold { get; set; }

        // Item Spawning Settings
        public int CoinCap { get; set; }
        public int PotionCap { get; set; }

        // Enemy Manager Settings
        public int EnemyMax { get; set; }
        public int SpawnGroupSize { get; set; }
        public string[][] EnemyTemplate { get; set; }

        public Settings()
        {
            PlayerIcon = "☻";
            PlayerAttack = "punches";
            PlayerColor = "White";

            PlayerHealth = 100;
            PlayerLives = 3;
            StartingX = 10;
            StartingY = 10;
            StartingGold = 0;

            CoinCap = 9;
            PotionCap = 2;

            EnemyMax = 26;
            SpawnGroupSize = 3;

            EnemyTemplate = new string[][]
            {
             new string[] {"Zombie","10","0", "Green", "bites", "☻" },
             new string[] {"Skeleton","15","1", "Gray", "smacks", "☻"},
             new string[] {"Monster","20","3", "Red", "scratches", "☻" },
            };
        }
    }
}
