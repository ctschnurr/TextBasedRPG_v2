using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class GameManager
    {
        private static Player player = new Player();
        private static bool gameOver = false;
        private static int turn = 1;
        private static string winState = "quit";
        public static void GameLoop()
        {
            Console.CursorVisible = false;

            MapManager atlas = new MapManager();
            MenuManager menus = new MenuManager();

            ResetWindowSize();

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
                ResetWindowSize();
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
