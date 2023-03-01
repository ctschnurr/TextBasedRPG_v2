using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class GameManager
    {
        static public Player player = new Player();

        public static bool gameOver = false;

        public static int turn = 1;
        public static void GameLoop()
        {
            Console.CursorVisible = false;
            EnemyManager.initEnemies();
            MenuManager.MainMenu();

            while (gameOver == false)
            {
                // this redraws the map and characters when redraw is true
                if (MapManager.redraw)
                {
                    MapManager.Draw();
                    HUD.Message();
                    EnemyManager.DrawEnemies();
                    MapManager.DrawCharacter(player);
                    MapManager.redraw = false;
                }

                // this is the primary game loop essentials
                HUD.Draw(player);
                player.Update(player);
                EnemyManager.UpdateEnemies(); // testing
                EnemyManager.Check();
                // EventManager.RefreshWindow();
                turn++;
            }
        }

        public static int GetTurn()
        {
            return turn;
        }

        public static Character GetPlayer()
        {
            return player;
        }

        public static void SetGameOver()
        {
            gameOver = true;
        }

    }
}
