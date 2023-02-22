using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{

    // TO DO:
    // Map - better maps/levels. different areas
    // Enemies - impliment different movement types. ask about attacking powers?
    // Items - 3 items. Potion, Coin, Shield?
    // ask about GameManager class, can be Program? -> clean up code into Classes better
    internal class Program
    {

        static public Player player = new Player();
        static public EventManager e_manager = new EventManager();
        
        static public Enemy enemyA;
        static public Enemy enemyB;
        static public Enemy enemyC;

        static public List<Enemy> enemies;
        static public List<Enemy> deadEnemies;

        public static bool gameOver = false;

        static int messageCount = 0;
        public static string messageContent = null;
        public static bool messageNew = false;

        static int turn = 1;

        static void Main(string[] args)
        {
            enemies = new List<Enemy>();
            deadEnemies = new List<Enemy>();

            Console.CursorVisible = false;
            EventManager.MainMenu();

            while (gameOver == false)
            {
                if (EventManager.redraw)
                {
                    MapManager.DrawMap(MapManager.overWorld);
                    MapManager.DrawCharacter(MapManager.overWorld, player);
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



        static void UpdateEnemies()
        {
            if (enemies.Count != 0)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.Update(player, MapManager.overWorld, enemy);
                }
            }

            foreach (Enemy enemy in deadEnemies)
            {
                enemies.Remove(enemy);
            }

            deadEnemies.Clear();
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
            string name = "baddie";

            spawnTime = turn % 30 == 0;

            if (spawnTime)
            {
                if (enemies.Exists(x => enemies.Contains(enemyA)) ==  false)
                {
                    enemyA = new Enemy();
                    enemies.Add(enemyA);
                    name = enemyA.name;
                    spawned = true;
                }

                else if (enemies.Exists(x => enemies.Contains(enemyB)) == false)
                {
                    enemyB = new Enemy();
                    enemies.Add(enemyB);
                    name = enemyB.name;
                    spawned = true;
                }

                else if (enemies.Exists(x => enemies.Contains(enemyC)) == false)
                {
                    enemyC = new Enemy();
                    enemies.Add(enemyC);
                    name = enemyC.name;
                    spawned = true;
                }
            }

            if (spawned)
            {
                messageContent = "A " + name + " has spawned!";
                messageNew = true;
            }

        }
    }
}
