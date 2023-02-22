using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Player : Character
    {
        public Player()
        {
            character = (char)1;
            name = "Player";
            healthMax = 100;
            health = healthMax;
            lives = 3;
            x = 5;
            lastX = 5;
            y = 5;
            lastY = 5;
            spawn[0] = 5;
            spawn[1] = 5;
            type = "player";
            color = ConsoleColor.Green;
        }
        public void Update(char[][,] input, Character self)
        {
            bool isWalkable;
            char destination = ' ';
            char[,] map = input[1];
            bool move = false;

            ConsoleKeyInfo choice = Console.ReadKey(true);

            switch (choice.Key)
            {
                case ConsoleKey.Escape:
                    Program.gameOver = true;
                    break;

                case ConsoleKey.W:
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

                case ConsoleKey.S:
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

                case ConsoleKey.A:
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

                case ConsoleKey.D:
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
                default:
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
