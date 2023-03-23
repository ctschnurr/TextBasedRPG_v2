using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class CollisionManager
    {
        // private static Character playerChar = GameManager.GetPlayer();

        private static string message = null;

        // this handles things you can interact with, but not pickup
        public static void Triggers(char destination)
        {
            Player player = GameManager.GetPlayer();

            int[] worldCoords = MapManager.GetWorldCoords();
            int worldX = worldCoords[0];
            int worldY = worldCoords[1];

            bool hasKey = player.GetKeyStatus();
            int gold = player.GetGold();

            switch (destination)
            {
                case '─':
                    bool isLocked = MapManager.GetGateLocked();

                    if (worldY == 1 && worldX == 1 && isLocked)
                    {
                        if (!hasKey)
                        {
                            message = "The gate is locked.. find the key!";
                            MenuManager.SetTaskMessage(message);
                            HUD.SetMessage(message);
                        }

                        if (hasKey)
                        {
                            message = "You unlocked the gate!";
                            HUD.SetMessage(message);
                            MapManager.AddWalkables(destination);
                            MapManager.SetGateLocked(false);
                        }
                    }
                    break;

                case '☻':
                    if (worldY == 2 && worldX == 2)
                    {
                        if (hasKey == false)
                        {
                            if (gold < 100)
                            {
                                message = "\'I'll trade 100 gold for the key!\'";
                                MenuManager.SetTaskMessage(message);
                                HUD.SetMessage(message);
                            }

                            if (gold > 100)
                            {
                                message = "\'Here is your key!\'";
                                MenuManager.SetTaskMessage(message);
                                HUD.SetMessage(message);
                                player.SetKeyStatus(true);
                                Player.AddGold(-100);
                            }
                        }

                        else if (hasKey)
                        {
                            message = "\'You have your key! Now begone!\'";
                            HUD.SetMessage(message);
                        }
                    }
                    break;

            }
        }
    }
}
