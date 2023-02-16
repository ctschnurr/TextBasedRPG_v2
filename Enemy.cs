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
            {"Zombie","10","3" },
            {"Skeleton","15","5"},
            {"Monster","20","7" },
        };

        public Enemy()
        {
            Random rand = new Random();
            int roll = rand.Next(0, 3);

            character = (char)2;
            name = enemies[roll, 0];
            type = "npc";
            health = Int32.Parse(enemies[roll, 1]);
            healthMax = 10;
            lives = 100;
            x = rand.Next(25, 30);
            y = rand.Next(25, 30);
            color = ConsoleColor.Red;
        }
        public void Update(int playerX, int playerY, char[][,] input, Character self)
        {
            bool isWalkable = true;
            char destination = ' ';
            char[,] map = input[1];
            bool move = false;

            string choice = "blank";

            if (playerY == y)
            {
                if (playerX > x)
                {
                    choice = "right";
                }

                if (playerX < x)
                {
                    choice = "left";
                }
            }

            if (playerX == x)
            {
                if (playerY > y)
                {
                    choice = "down";
                }
                if (playerY < y)
                {
                    choice = "up";
                }
            }

            else
            {
                Random rnd = new Random();
                int walk = rnd.Next(1, 3);

                switch (walk)
                {
                    case 1:
                        {
                            if (playerX > x)
                            {
                                choice = "right";
                            }

                            if (playerX < x)
                            {
                                choice = "left";
                            }
                            break;
                        }

                    case 2:
                        {
                            if (playerY > y)
                            {
                                choice = "down";
                            }

                            if (playerY < y)
                            {
                                choice = "up";
                            }
                            break;
                        }
                }
            }

            switch (choice)
            {
                case "left":
                    destination = map[y, x - 1];
                    isWalkable = MapManager.CheckWalkable(destination);

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
                    isWalkable = MapManager.CheckWalkable(destination);

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
                    isWalkable = MapManager.CheckWalkable(destination);

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
                    isWalkable = MapManager.CheckWalkable(destination);

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
