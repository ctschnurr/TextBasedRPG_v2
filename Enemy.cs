using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Enemy : Character
    {
        public string[,] enemyTemplate = new string[,]
        {
            {"Zombie","10","3", "Green" },
            {"Skeleton","15","5", "Gray"},
            {"Monster","20","7", "Red" },
        };

        static public Enemy enemyA;
        static public Enemy enemyB;
        static public Enemy enemyC;

        static public Enemy enemysave1;
        static public Enemy enemysave2;
        static public Enemy enemysave3;

        static public List<Enemy> enemies;
        static public List<Enemy> deadEnemies;

        public bool onMap;

        public enum Behavior
        {
            chase,
            wander
        }

        public Behavior behavior;

        public Enemy()
        {
            Random rand = new Random();
            int roll = rand.Next(0, 3);

            name = enemyTemplate[roll, 0];
            health = Int32.Parse(enemyTemplate[roll, 1]);
            // strength = enemies[roll, 2]
            color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), enemyTemplate[roll, 3]);

            behavior = (Behavior)rand.Next(0, 2);

            character = (char)1;
            type = "npc";
            healthMax = health;

            x = rand.Next(25, 30);
            y = rand.Next(25, 30);

            worldX = 1;
            worldY = 0;
        }

        public static void initEnemies()
        {
            enemies = new List<Enemy>();
            deadEnemies = new List<Enemy>();
        }

        public static void Check()
        {
            if (MapManager.worldX == 2 && MapManager.worldY == 0)
            {
                GenerateEnemy();
                foreach (Enemy enemy in Enemy.enemies)
                {
                    MapManager.DrawCharacter(enemy);
                }

                UpdateEnemies();

            }
        }

        
        public void Update(Character player, Character self)
        {
            bool isWalkable;
            char destination = ' ';
            char[,] map = MapManager.world[MapManager.worldY, MapManager.worldX];
            bool move = false;
            int walk;
            Random rnd = new Random();

            string choice = "blank";
            
            switch (behavior)
            {
                case Behavior.chase:
                    if (player.y == y)
                    {
                        if (player.x > x)
                        {
                            choice = "right";
                        }

                        if (player.x < x)
                        {
                            choice = "left";
                        }
                    }

                    if (player.x == x)
                    {
                        if (player.y > y)
                        {
                            choice = "down";
                        }
                        if (player.y < y)
                        {
                            choice = "up";
                        }
                    }

                    else
                    {
                        walk = rnd.Next(1, 3);

                        switch (walk)
                        {
                            case 1:
                                {
                                    if (player.x > x)
                                    {
                                        choice = "right";
                                    }

                                    if (player.x < x)
                                    {
                                        choice = "left";
                                    }
                                    break;
                                }

                            case 2:
                                {
                                    if (player.y > y)
                                    {
                                        choice = "down";
                                    }

                                    if (player.y < y)
                                    {
                                        choice = "up";
                                    }
                                    break;
                                }
                        }
                    }

                    break;

                case Behavior.wander:
                    walk = rnd.Next(1, 5);
                    if (walk == 1) choice = "right";
                    if (walk == 2) choice = "left";
                    if (walk == 3) choice = "up";
                    if (walk == 4) choice = "down";
                    break;
            }
            
            switch (choice)
            {
                case "left":
                    destination = map[y, x - 1];
                    isWalkable = MapManager.CheckWalkable(destination, self);

                    if (isWalkable == true)
                    {
                        move = true;
                        lastY = y;
                        lastX = x;
                        x--;
                        break;
                    }
                    else
                    {
                        break;
                    }

                case "right":
                    destination = map[y, x + 1];
                    isWalkable = MapManager.CheckWalkable(destination, self);

                    if (isWalkable == true)
                    {
                        move = true;
                        lastY = y;
                        lastX = x;
                        x++;
                        break;
                    }
                    else
                    {
                        break;
                    }

                case "up":
                    destination = map[y - 1, x];
                    isWalkable = MapManager.CheckWalkable(destination, self);

                    if (isWalkable == true)
                    {
                        move = true;
                        lastY = y;
                        lastX = x;
                        y--;
                        break;
                    }
                    else
                    {
                        break;
                    }

                case "down":
                    destination = map[y + 1, x];
                    isWalkable = MapManager.CheckWalkable(destination, self);

                    if (isWalkable == true)
                    {
                        move = true;
                        lastY = y;
                        lastX = x;
                        y++;
                        break;
                    }
                    else
                    {
                        break;
                    }
            }

            if (move)
            {
                EventManager.EventCheck(destination, self);
                MapManager.DrawCharacter(self);
            }
        }

        public static void GenerateEnemy()
        {
            bool spawnTime;
            bool spawned = false;
            string name = "baddie";

            spawnTime = Program.turn % 30 == 0;

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
                EventManager.messageContent = "A " + name + " has spawned!";
                EventManager.messageNew = true;
            }

        }

        public static void UpdateEnemies()
        {
            if (enemies.Count != 0)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.Update(Program.player, enemy);
                }
            }

            foreach (Enemy enemy in deadEnemies)
            {
                enemies.Remove(enemy);
            }

            deadEnemies.Clear();
        }





    }
}
