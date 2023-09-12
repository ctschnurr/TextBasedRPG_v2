using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    // Item class, parent class for all items
    internal class Item
    {
        protected string name;
        protected int worldX;
        protected int worldY;
        protected int localX;
        protected int localY;

        protected bool erase = false;
        protected ConsoleColor color;
        protected char icon;

        public static void Draw(Item subject)
        {
            char[,] map = MapManager.GetMap();
            char tile;
            string[] colorDat;

            // draw the item on screen in set position

            Console.SetCursorPosition(subject.localX + 2, subject.localY + 1);
            tile = map[subject.localY, subject.localX];
            colorDat = MapManager.GetTileColor(tile);
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorDat[1]);
            Console.ForegroundColor = subject.color;
            Console.Write(subject.icon);
            Console.ResetColor();
        }

        public int GetX()
        {
            return localX;
        }

        public int GetY()
        {
            return localY;
        }

        public int GetWorldX()
        {
            return worldX;
        }

        public int GetWorldY()
        {
            return worldY;
        }

        public string GetName()
        {
            return name;
        }

        public void PickUp(Item input)
        {
            Character player = GameManager.GetPlayer();
            string message = " ";

            switch (input.name)
            {
                case "coin":
                    Player.AddGold(1);
                    ItemManager.SetUsedItem(input);
                    message = "You found a gold coin!";
                    HUD.SetMessage(message);
                    break;

                case "sword":
                    player.AddStrength(3);
                    player.SetPower("slashes");
                    ItemManager.SetUsedItem(input);
                    message = "You found a sword! You deal 3 more damage!";
                    HUD.SetMessage(message);
                    break;

                case "potion":
                    int health = player.GetHealth();
                    int healthMax = player.GetHealthMax();

                    if (health == healthMax)
                    {
                        message = "You found a potion, but health is full!";
                        HUD.SetMessage(message);HUD.SetMessage(message);
                    }

                    else if (health != healthMax)
                    {
                        player.RestoreHealth();
                        ItemManager.SetUsedItem(input);
                        message = "You found a potion, health is restored!";
                        HUD.SetMessage(message);
                        HUD.Draw(player);
                    }
                    break;
            }
        }           
    }               
}                   
                    

                    

                    
                    