using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Program
    {

        static public Player player = new Player();
        static public EventManager e_manager = new EventManager();
        static public Enemy enemy = new Enemy();

        // make a multiple enemy system
        static public Enemy enemyB = new Enemy();
        //static public Enemy enemyC = new Enemy();

        static public List<Enemy> enemies;

        public static bool gameOver = false;

        static int messageCount = 0;
        public static string messageContent = null;
        public static bool messageNew = false;

        static int turn = 0;

        static void Main(string[] args)
        {
            enemies = new List<Enemy>();

            Console.CursorVisible = false;
            EventManager.MainMenu();

            while (gameOver == false)
            {
                if (EventManager.redraw)
                {
                    MapManager.DrawMap(MapManager.overWorld);
                    MapManager.DrawCharacter(MapManager.overWorld, player);
                    DrawEnemies();
                    EventManager.redraw = false;
                }

                player.ShowHud();
                player.Update(MapManager.overWorld, player);
                UpdateEnemies();
                EventManager.RefreshWindow();
                MapMessage();
                GenerateEnemy();
                turn++;
            }

        }

        static void DrawEnemies()
        {
            foreach (Character enemy in enemies)
            {
                MapManager.DrawCharacter(MapManager.overWorld, enemy);
            }
        }

        static void UpdateEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(player.x, player.y, MapManager.overWorld, enemy);
            }
        }

        static void MapMessage()
        {
            if (messageNew == true)
            {
                messageCount = 5;
                messageNew = false;
            }

            if (messageCount != 0)
            {
                Console.SetCursorPosition(42, 40);
                Console.Write(messageContent);
                messageCount--;
            }
        }

        static void GenerateEnemy()
        {
            if (turn == 10 || turn == 20 || turn == 30) // write this better, like if the turn is a multiple of another number
            {

                enemies.Find(enemy)
            }
        }
    }
}
