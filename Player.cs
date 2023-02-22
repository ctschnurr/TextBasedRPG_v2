﻿using System;
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
            character = (char)2;
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
            color = ConsoleColor.White;
        }
        public void Update(Character self)
        {
            bool isWalkable;
            char destination = ' ';
            char[,] map = MapManager.world[MapManager.worldY, MapManager.worldX];
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

            if (y == 0)
            {
                MapManager.worldY --;
                y = 35;
                EventManager.redraw = true;
            }

            if (y == 36)
            {
                MapManager.worldY++;
                y = 1;
                EventManager.redraw = true;
            }

            if (x == 0)
            {
                MapManager.worldX--;
                x = 87;
                EventManager.redraw = true;
            }

            if (x == 88)
            {
                MapManager.worldX++;
                x = 1;
                EventManager.redraw = true;
            }

            if (move)
            {
                EventManager.EventCheck(destination, self);
                MapManager.DrawCharacter(self);
            }

        }
    }
}
