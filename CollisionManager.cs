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
        private static List<Enemy> enemies;

        //this handles the pickups and battle detection
        public static void CollisionCheck(char destination, Character subject)
        {
            enemies = EnemyManager.GetEnemies();
            char[,] holder = MapManager.GetWorld();
            int[] worldCoords = MapManager.GetWorldCoords();
            int worldX = worldCoords[0];
            int worldY = worldCoords[1];

            Player player = GameManager.GetPlayer();

            if (subject == player)
            {
                bool fight = false;
                Enemy victim = null;

                foreach (Enemy enemy in enemies)
                {
                    if (enemy.x == subject.x && enemy.y == subject.y && enemy.worldX == subject.worldX && enemy.worldY == subject.worldY)
                    {
                        victim = enemy;
                        fight = true;
                    }
                }

                if (fight) BattleSystem.Battle(subject, victim);

                if (destination == '▀')
                {
                    if (worldY == 1 && worldX == 2)
                    {
                        worldY = 2;
                        MapManager.SetWorld(worldX, worldY);
                        player.SetY(14);
                        player.SetX(42);
                        MapManager.SetRedraw(true);
                    }

                    else if (worldY == 2 && worldX == 2)
                    {
                        worldY = 1;
                        MapManager.SetWorld(worldX, worldY);
                        player.SetY(23);
                        player.SetX(51);
                        MapManager.SetRedraw(true);
                    }
                }

                if (destination == 'ō')
                {
                    if (player.health == player.healthMax)
                    {
                        message = "You found a potion, but health is full!";
                        HUD.SetMessage(message);
                    }

                    if (player.health != player.healthMax)
                    {
                        message = "You found a potion, health is restored!";
                        HUD.SetMessage(message);

                        player.health = player.healthMax;

                        holder[player.y, player.x] = ' ';
                    }
                }

                if (destination == '┼')
                {
                    message = "You found a sword! You deal 3 more damage!";
                    HUD.SetMessage(message);

                    player.strength += 3;
                    player.power = "slashes";

                    holder[player.y, player.x] = ' ';
                }

                if (destination == '°')
                {
                    message = "You found a gold coin!";
                    HUD.SetMessage(message);

                    player.AddGold(1);

                    holder[player.y, player.x] = ' ';
                }
            }

            if (subject.type == "npc")
            {
                if (subject.x == player.x && subject.y == player.y) BattleSystem.Battle(subject, player);
            }
        }

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
                                player.AddGold(-100);
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
