using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class GameManager
    {
        private static Player player;
        private static bool gameOver = false;
        private static int turn = 1;
        private static string winState = "quit";

        public static Settings settings;
        public static void GameLoop()
        {
            Console.CursorVisible = false;

            settings = new Settings();

            if (!System.IO.File.Exists("./Assets/settings.txt"))
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                String serializedSettings = JsonSerializer.Serialize(settings, options);
                File.WriteAllText("./Assets/settings.txt", serializedSettings);
            }
            else
            {
                String deserializedSettings = File.ReadAllText("./Assets/settings.txt");
                settings = JsonSerializer.Deserialize<Settings>(deserializedSettings);
            }

            player = new Player();

            MapManager atlas = new MapManager();
            MenuManager menus = new MenuManager();

            //ResetWindowSize();

            MenuManager.MainMenu();

            ItemManager.InitItems();
            WorldManager.ConstructInteractables();

            while (gameOver == false)
            {
                MapManager.Draw();
                HUD.Update();
                player.Update();
                EnemyManager.Update();
                ItemManager.Update();
                WorldManager.Update();
                turn++;
                //ResetWindowSize();
            }

            MenuManager.GameOver();
        }

        public static int GetTurn()
        {
            return turn;
        }

        public static Player GetPlayer()
        {
            return player;
        }

        public static void SetGameOver()
        {
            gameOver = true;
        }

        public static string GetWinState()
        {
            return winState;
        }

        public static void SetWinState(string input)
        {
            winState = input;
        }

        public static void ResetWindowSize()
        {
            char[,] data = MapManager.GetMap();
            Console.SetWindowSize(data.GetLength(1) + 3, data.GetLength(0) + 2);
        }
    }
}
