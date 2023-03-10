using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Player : Character
    {
        public static bool hasKey;
        public static int gold;


        public Player()
        {
            character = (char)2;
            name = "Player";
            power = "punches";
            healthMax = 100;
            health = healthMax;
            lives = 3;
            gold = 0;

            x = 10;
            lastX = 10;
            y = 10;
            lastY = 10;

            worldX = 1;
            worldY = 1;

            spawn[0] = 5;
            spawn[1] = 5;
            type = "player";
            color = ConsoleColor.White;
            hasKey = false;

        }
        public void Update(Character self)
        {
            bool isWalkable;
            char destination = ' ';
            char[,] map = MapManager.world[MapManager.worldY, MapManager.worldX];
            bool move = false;
            erase = false;

            ConsoleKeyInfo choice = Console.ReadKey(true);

            switch (choice.Key)
            {
                case ConsoleKey.Escape:
                    MenuManager.PauseMenu();
                    MapManager.redraw = true;
                    break;

                case ConsoleKey.W:
                    destination = map[y - 1, x];
                    isWalkable = MapManager.CheckWalkable(destination, self);
                    CollisionManager.Triggers(destination);

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
                    CollisionManager.Triggers(destination);

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
                    CollisionManager.Triggers(destination);

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
                    CollisionManager.Triggers(destination);

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
                MapManager.worldY--;
                worldY--;
                y = 35;
                MapManager.redraw = true;
            }

            if (y == 36)
            {
                MapManager.worldY++;
                worldY++;
                y = 1;
                MapManager.redraw = true;
            }

            if (x == 0)
            {
                MapManager.worldX--;
                worldX--;
                x = 87;
                MapManager.redraw = true;
            }

            if (x == 88)
            {
                MapManager.worldX++;
                worldX--;
                x = 1;
                MapManager.redraw = true;
            }



            if (move)
            {
                CollisionManager.CollisionCheck(destination, self);
                erase = true;
                Draw(self);
            }
        }

        public void CollisionCheck()
        {

        }
    }
}
