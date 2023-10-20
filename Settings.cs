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
        public string ReadMe { get; set; }
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
        public string[] TemplateReferenceKey { get; set; }
        public string[][] EnemyTemplate { get; set; }
        public string[] BossTemplate { get; set; }

        public Settings()
        {
            ReadMe = "If you edit these settings and the game becomes unplayable, delete this settings.txt and run the game again, a new one will be created.";

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

            TemplateReferenceKey = new string[] { "name", "hp", "strength", "color", "attackDescription", "icon" };

            EnemyTemplate = new string[][]
            {
             new string[] {"Zombie","10","0", "Green", "bites", "☻" },
             new string[] {"Skeleton","15","1", "Gray", "smacks", "☻"},
             new string[] {"Monster","20","3", "Red", "scratches", "☻" },
            };

            BossTemplate = new string[] { "Skeleton King", "80", "5", "Grey", "smacks", "☻" };
        }
    }
}
