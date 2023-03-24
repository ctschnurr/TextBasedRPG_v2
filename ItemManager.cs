using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextBasedRPG_v2.Settings;

namespace TextBasedRPG_v2
{
    internal class ItemManager
    {
        private static List<Item> items = new List<Item>();
        private static List<Item> usedItems = new List<Item>();

        static Random itemRand = new Random();

        public static void Draw()
        {
            int[] worldCoords = MapManager.GetWorldCoords();
            int worldX = worldCoords[0];
            int worldY = worldCoords[1];

            Character player = GameManager.GetPlayer();

            foreach (Item item in items)
            {
                int playerX = player.GetX();
                int playerY = player.GetY();

                int itemX = item.GetX();
                int itemY = item.GetY();

                int itemWX = item.GetWorldX();
                int itemWY = item.GetWorldY();

                bool playerOn = false;

                if (playerX == itemX && playerY == itemY) playerOn = true;

                if (worldX == itemWX && worldY == itemWY && !playerOn)
                {
                    Item.Draw(item);
                }
            }
        }
        public static void Update()
        {
            CleanupItems();
            Draw();
        }

        public static Random GetRandom()
        {
            return itemRand;
        }

        public static void InitItems()
        {
            // random coins on maps
            for(int n = 0; n <= coinCap; n++)
            {
                items.Add(new Coin(1, 1));
            }
            for (int n = 0; n <= coinCap; n++)
            {
                items.Add(new Coin(2, 0));
            }
            for (int n = 0; n <= coinCap; n++)
            {
                items.Add(new Coin(2, 1));
            }

            //randomly placed potions
            for (int n = 1; n <= potionCap; n++)
            {
                items.Add(new Potion(1, 0));
            }
            for (int n = 1; n <= potionCap; n++)
            {
                items.Add(new Potion(2, 0));
            }

            // specifically placed sword
            items.Add(new Sword());

            // lots of coins for final area
            for (int n = 0; n <= 30; n++)
            {
                items.Add(new Coin(1, 0));
            }
        }
        public static void CleanupItems()
        {
            foreach (Item item in usedItems)
            {
                items.Remove(item);
            }

            usedItems.Clear();

        }

        public static List<Item> GetItems()
        {
            return items;
        }

        public static void SetUsedItem(Item input)
        {
            usedItems.Add(input);
        }
    }
}
