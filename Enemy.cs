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
            {"Zombie","10","0", "Green", "bites" },
            {"Skeleton","15","1", "Gray", "smacks"},
            {"Monster","20","3", "Red", "scratches" },
        };


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
            strength = Int32.Parse(enemyTemplate[roll, 2]);
            color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), enemyTemplate[roll, 3]);
            power = enemyTemplate[roll, 4]; 

            behavior = (Behavior)rand.Next(0, 2);

            character = (char)2;
            type = "npc";
            healthMax = health;

            x = rand.Next(20, 50);
            y = rand.Next(15, 35);

            worldX = 0;
            worldY = 0;

            stunned = false;
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




    }
}
