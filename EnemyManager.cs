﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextBasedRPG_v2.Settings;

namespace TextBasedRPG_v2
{
    internal class EnemyManager
    {
        private static Enemy enemyRef1;
        private static Enemy enemyRef2;
        private static Enemy enemyRef3;
                
        private static List<Enemy> enemies = new List<Enemy>();
        private static List<Boss> bosses = new List<Boss>();

        private static List<Enemy> deadEnemies = new List<Enemy>();
        private static List<Enemy> enemyReferences = new List<Enemy>();

        private static string message = null;

        private static bool firstSpawn = false;

        public static void Draw()
        {
            int[] worldCoords = MapManager.GetWorldCoords();
            int worldX = worldCoords[0];
            int worldY = worldCoords[1];

            if (worldX == 2 && worldY == 0)
            {
                foreach (Enemy enemy in enemies)
                {
                    Character.Draw(enemy);
                }
            }

            else if (worldX == 2 && worldY == 0)
            {
                foreach (Boss boss in bosses)
                {
                    Character.Draw(boss);
                }
            }

        }

        public static void GenerateEnemy()
        {
            bool spawned = false;

            int turn = GameManager.GetTurn();
            bool spawnTime = turn % 30 == 0;

            if (spawnTime)
            {
                for(int i = 0; i <= GameManager.settings.SpawnGroupSize; i++)
                {
                    if (enemies.Count <= GameManager.settings.EnemyMax)
                    {
                        enemies.Add(new Enemy());
                        spawned = true;
                        if (firstSpawn == false)
                        {
                            QuestManager.GenerateQuests();
                            QuestManager.SelectQuest();
                            firstSpawn = true;
                        }
                    }
                }
            }

            if (spawned)
            {
                message = "Enemies have spawned!";
                HUD.SetMessage(message);
                Draw();
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
                        bool stunned = enemy.GetStunned();
                        if (stunned == true) enemy.SetStunned(false);
                        else if (stunned == false) enemy.Update();
                    }
                }

                GenerateEnemy();
                CleanupEnemies();
            }

            if (worldX == 1 && worldY == 0)
            {
                if (bosses.Count == 0)
                {
                    bosses.Add(new Boss());
                }

                foreach (Boss boss in bosses)
                {
                    boss.Update();
                }
            }
        }

        public static void CleanupEnemies()
        {
            foreach (Enemy enemy in deadEnemies)
            {
                enemies.Remove(enemy);
                // Check if there is an active quest.
                if (QuestManager.HasActiveQuest())
                {
                    Quest activeQuest = QuestManager.GetActiveQuest();

                    // Check if this enemy's name matches the active quest's target.
                    if (activeQuest.GetName() == enemy.GetName())
                    {
                        // This enemy fulfills the active quest's target.
                        // Update quest progress and check if the quest is completed.
                        activeQuest.UpdateProgress(1);

                        // Check if the quest is completed and announce it.
                        if (QuestManager.CheckQuest())
                        {
                            // QuestManager.CheckQuest() will handle announcements.
                        }
                    }
                }
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
        // Enemy references are used in the pause menu to track the last three defeated enemies
        public static List<Enemy> GetRef()
        {
            return enemyReferences;
        }

        public static List<Enemy> GetEnemies()
        {
            return enemies;
        }

        public static List<Boss> GetBosses()
        {
            return bosses;
        }

        public static void SetDeadEnemy(Enemy input)
        {
            deadEnemies.Add(input);
        }

    }
}
