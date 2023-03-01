using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class EnemyManager
    {
        static public Enemy enemyA;
        static public Enemy enemyB;
        static public Enemy enemyC;

        static private Enemy enemyRef1;
        static private Enemy enemyRef2;
        static private Enemy enemyRef3;

        static public List<Enemy> enemies;
        static public List<Enemy> deadEnemies;

        static private List<Enemy> enemyReferences = new List<Enemy>();

        static string message = null;

        private static Character target;
        public static void initEnemies()
        {
            enemies = new List<Enemy>();
            deadEnemies = new List<Enemy>();
        }

        public static void DrawEnemies()
        {
            if (MapManager.worldX == 2 && MapManager.worldY == 0)
                foreach (Enemy enemy in enemies)
                {
                    MapManager.DrawCharacter(enemy);
                }
        }

        public static void Check()
        {
            if (MapManager.worldX == 2 && MapManager.worldY == 0)
            {
                CleanupEnemies();
                DrawEnemies();
            }
        }

        public static void GenerateEnemy()
        {
            bool spawned = false;
            string name = "baddie";

            int turn = GameManager.GetTurn();
            bool spawnTime = turn % 30 == 0;

            if (spawnTime)
            {
                if (enemies.Exists(x => enemies.Contains(enemyA)) == false)
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
                message = "A " + name + " has spawned!";
                HUD.SetMessage(message);
            }

        }

        public static void UpdateEnemies()
        {
            target = GameManager.GetPlayer();

            if (MapManager.worldX == 2 && MapManager.worldY == 0)
            {

                if (enemies.Count != 0)
                {
                    foreach (Enemy enemy in enemies)
                    {
                        if (enemy.stunned == true) enemy.stunned = false;
                        else if (enemy.stunned == false) enemy.Update(target, enemy);

                    }
                }

                GenerateEnemy();
            }
        }

        public static void CleanupEnemies()
        {
            foreach (Enemy enemy in deadEnemies)
            {
                enemies.Remove(enemy);
            }

            deadEnemies.Clear();

        }

        public static void SetRef(Enemy subject)
        {
            if (enemyRef1 == null)
            {
                enemyRef1 = subject;
                enemyReferences.Add(enemyRef1);
            }

            else if (enemyRef2 == null)
            {
                enemyRef2 = subject;
                enemyReferences.Add(enemyRef2);
            }

            else if (enemyRef3 == null)
            {
                enemyRef3 = subject;
                enemyReferences.Add(enemyRef3);
            }

            else
            {
                enemyRef3 = enemyRef2;
                enemyRef2 = enemyRef1;
                enemyRef1 = subject;

                enemyReferences.Clear();
                enemyReferences.Add(enemyRef1);
                enemyReferences.Add(enemyRef2);
                enemyReferences.Add(enemyRef3);

            }
        }

        public static List<Enemy> GetRef()
        {
            return enemyReferences;
        }



    }
}
