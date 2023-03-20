using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class EnemyManager
    {
        private static Enemy enemyA;
        private static Enemy enemyB;
        private static Enemy enemyC;
                
        private static Enemy enemyRef1;
        private static Enemy enemyRef2;
        private static Enemy enemyRef3;
                
        private static List<Enemy> enemies = new List<Enemy>();
        private static List<Enemy> deadEnemies = new List<Enemy>();

        private static List<Enemy> enemyReferences = new List<Enemy>();

        private static string message = null;

        public static void Draw()
        {
            int[] worldCoords = MapManager.GetWorldCoords();
            int worldX = worldCoords[0];
            int worldY = worldCoords[1];

            if (worldX == 2 && worldY == 0)
                foreach (Enemy enemy in enemies)
                {
                    Character.Draw(enemy);
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

        public static void Update()
        {
            int[] worldCoords = MapManager.GetWorldCoords();
            int worldX = worldCoords[0];
            int worldY = worldCoords[1];

            if (worldX == 2 && worldY == 0)
            {

                if (enemies.Count != 0)
                {
                    foreach (Enemy enemy in enemies)
                    {
                        if (enemy.stunned == true) enemy.stunned = false;
                        else if (enemy.stunned == false) enemy.Update();
                    }
                }

                GenerateEnemy();
                CleanupEnemies();
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

        public static List<Enemy> GetEnemies()
        {
            return enemies;
        }

        public static void SetDeadEnemy(Enemy input)
        {
            deadEnemies.Add(input);
        }

    }
}
