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
        
        static public Enemy enemyA;
        static public Enemy enemyB;
        //static public Enemy enemyC = new Enemy();

        static public List<Enemy> enemies;

        public static bool gameOver = false;

        static int messageCount = 0;
        public static string messageContent = null;
        public static bool messageNew = false;

        static int turn = 1;

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
                
                GenerateEnemy();

                player.ShowHud();
                player.Update(MapManager.overWorld, player);
                EventManager.RefreshWindow();
                UpdateEnemies();
                MapMessage();
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
            if (enemies.Count != 0)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.Update(player.x, player.y, MapManager.overWorld, enemy);
                }
            }


        }

        static void MapMessage()
        {
            if (messageNew == true)
            {
                messageCount = 10;
                messageNew = false;
            }

            if (messageCount > 0)
            {
                Console.SetCursorPosition(46, 40);
                Console.Write(messageContent);
                messageCount--;
            }

            if (messageCount == 0)
            {
                Console.SetCursorPosition(45, 40);
                Console.Write("                          ");
            }
        }

        static void GenerateEnemy()
        {
            bool spawnTime;
            bool spawned = false;

            spawnTime = turn % 30 == 0;

            if (spawnTime)
            {
                if (enemies.Exists(x => enemies.Contains(enemyA)) ==  false)
                {
                    enemyA = new Enemy();
                    enemies.Add(enemyA);
                    spawned = true;
                }

                else if (enemies.Exists(x => enemies.Contains(enemyB)) == false)
                {
                    enemyB = new Enemy();
                    enemies.Add(enemyB);
                    spawned = true;
                }
            }

            if (spawned)
            {
                messageContent = "A new enemy has spawned!";
                messageNew = true;
            }

        }
    }
}
