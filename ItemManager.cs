using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            items.Add(new Coin());
            items.Add(new Coin());
            items.Add(new Coin());
            items.Add(new Sword());
            items.Add(new Potion());
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
