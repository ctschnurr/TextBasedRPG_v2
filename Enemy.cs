using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Enemy : Character
    {
        public string[,] enemies = new string[,]
        {
            {"Zombie","10","3", "Green" },
            {"Skeleton","15","5", "White"},
            {"Monster","20","7", "Red" },
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

            name = enemies[roll, 0];
            health = Int32.Parse(enemies[roll, 1]);
            // strength = enemies[roll, 2]
            color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), enemies[roll, 3]);

            behavior = (Behavior)rand.Next(0, 2);

            character = (char)2;
            type = "npc";
            healthMax = health;

            x = rand.Next(25, 30);
            y = rand.Next(25, 30);

        }
        public void Update(Character player, char[][,] input, Character self)
        {
            bool isWalkable;
            char destination = ' ';
            char[,] map = input[1];
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
                MapManager.DrawCharacter(MapManager.overWorld, self);
            }
        }
    }
}
