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
        public static void GameLoop()
        {
            Console.CursorVisible = false;

            MapManager atlas = new MapManager();
            MenuManager menus = new MenuManager();

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
            }
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

    }
}
