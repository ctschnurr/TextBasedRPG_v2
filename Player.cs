using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Player : Character
    {
        private bool hasKey;
        private int gold;
        private int lives;

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
        public void Update()
        {
            bool isWalkable;
            char destination = ' ';
            char[,] map = MapManager.GetWorld();
            bool move = false;
            erase = false;

            ConsoleKeyInfo choice = Console.ReadKey(true);

            switch (choice.Key)
            {
                case ConsoleKey.Escape:
                    MenuManager.PauseMenu();
                    MapManager.SetRedraw(true);
                    break;

                case ConsoleKey.W:
                    destination = map[y - 1, x];
                    isWalkable = MapManager.CheckWalkable(destination, this);
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
                    isWalkable = MapManager.CheckWalkable(destination, this);
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
                    isWalkable = MapManager.CheckWalkable(destination, this);
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
                    isWalkable = MapManager.CheckWalkable(destination, this);
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
                worldY--;
                MapManager.SetWorld(worldX, worldY);
                y = 35;
                MapManager.SetRedraw(true);
            }

            if (y == 36)
            {
                worldY++;
                MapManager.SetWorld(worldX, worldY);
                y = 1;
                MapManager.SetRedraw(true);
            }

            if (x == 0)
            {
                worldX--;
                MapManager.SetWorld(worldX, worldY);
                x = 87;
                MapManager.SetRedraw(true);
            }

            if (x == 88)
            {
                worldX++;
                MapManager.SetWorld(worldX, worldY);
                x = 1;
                MapManager.SetRedraw(true);
            }



            if (move)
            {
                CollisionManager.CollisionCheck(destination, this);
                erase = true;
                Draw(this);
            }
        }

        public void CollisionCheck()
        {

        }

        public void SetLives(int change)
        {
            lives = lives + change;
        }

        public int GetLives()
        {
            return lives;
        }

        public void AddGold(int change)
        {
            gold += change;
        }

        public int GetGold()
        {
            return gold;
        }

        public bool GetKeyStatus()
        {
            return hasKey;
        }

        public void SetKeyStatus(bool status)
        {
            hasKey = status;
        }
    }
}
