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
        private static int gold;
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
                    isWalkable = CollisionCheck(y - 1, x);

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
                    isWalkable = CollisionCheck(y + 1, x);

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
                    isWalkable = CollisionCheck(y, x - 1);

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
                    isWalkable = CollisionCheck(y, x + 1);

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
                FightCheck(this);
                PickupCheck();
                InteractableCheck();
                erase = true;
                Draw(this);
            }
        }

        public void SetLives(int change)
        {
            lives = lives + change;
        }

        public int GetLives()
        {
            return lives;
        }

        public static void AddGold(int change)
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

        public void SetWorldX(int x)
        {
            worldX = x;
        }

        public void SetWorldY(int y)
        {
            worldY = y;
        }

        public void PickupCheck()
        {
            List<Item> items = ItemManager.GetItems();

            foreach (Item item in items)
            {
                int itemX = item.GetX();
                int itemY = item.GetY();

                int itemWX = item.GetWorldX();
                int itemWY = item.GetWorldY();

                if (itemX == x && itemY == y && itemWX == worldX && itemWY == worldY)
                {
                    item.PickUp(item);
                }
            }
        }

        public void InteractableCheck()
        {
            List<Interactable> interactables = WorldManager.GetInteractables();

            foreach (Interactable interactable in interactables)
            {
                int interactableX = interactable.GetX();
                int interactableY = interactable.GetY();

                int interactableWX = interactable.GetWorldX();
                int interactableWY = interactable.GetWorldY();

                if (interactableX == x && interactableY == y && interactableWX == worldX && interactableWY == worldY)
                {
                    interactable.Interact(interactable);
                }
            }
        }
    }
}
