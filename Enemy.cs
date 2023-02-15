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

        public char character = (char)2;
        public Enemy()
        {
            Random rand = new Random();
            int roll = rand.Next(0, 3);

            name = enemies[roll, 0];
            type = "npc";
            health = Int32.Parse(enemies[roll, 1]);
            healthMax = 10;
            lives = 100;
            x = rand.Next(25, 30);
            y = rand.Next(25, 30);
            color = ConsoleColor.Red;
        }
        public void ShowHud()
        {
            string hudHealth = health.ToString();
            Console.SetCursorPosition(42, 40);
            Console.WriteLine("║ " + name.PadRight(name.Length + 1) + ": Health: " + hudHealth.PadRight(5));
        }

        public void Update(int playerX, int playerY, char[,] map, Character self)
        {
            bool isWalkable = true;
            char destination = ' ';

            Console.SetCursorPosition(x + 2, y + 1);
            char tile = map[y, x];

            MapManager.DrawTile(tile);

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
                        y++;
                        break;
                    }
                    else
                    {
                        break;
                    }
            }

            EventManager.EventCheck(destination, self);
        }
    }
}
