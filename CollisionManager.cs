using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class CollisionManager
    {
        private static Character player = GameManager.GetPlayer();

        private static string message = null;
        private static List<Enemy> enemies;

        //this handles the pickups and battle detection
        public static void CollisionCheck(char destination, Character subject)
        {
            enemies = EnemyManager.GetEnemies();

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
                    if (MapManager.worldY == 1 && MapManager.worldX == 2)
                    {
                        MapManager.worldY = 2;
                        player.y = 14;
                        player.x = 42;
                        MapManager.redraw = true;
                    }

                    else if (MapManager.worldY == 2 && MapManager.worldX == 2)
                    {
                        MapManager.worldY = 1;
                        player.y = 23;
                        player.x = 51;
                        MapManager.redraw = true;
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

                        char[,] holder = MapManager.world[MapManager.worldY, MapManager.worldX];
                        holder[player.y, player.x] = ' ';
                    }
                }

                if (destination == '┼')
                {
                    message = "You found a sword! You deal 3 more damage!";
                    HUD.SetMessage(message);

                    player.strength += 3;
                    player.power = "slashes";

                    char[,] holder = MapManager.world[MapManager.worldY, MapManager.worldX];
                    holder[player.y, player.x] = ' ';
                }

                if (destination == '°')
                {
                    message = "You found a gold coin!";
                    HUD.SetMessage(message);

                    Player.gold += 1;

                    char[,] holder = MapManager.world[MapManager.worldY, MapManager.worldX];
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
            switch (destination)
            {
                case '─':
                    if (MapManager.worldY == 1 && MapManager.worldX == 1 && MapManager.gateLocked == true)
                    {
                        if (Player.hasKey == false)
                        {
                            message = "The gate is locked.. find the key!";
                            MenuManager.SetTaskMessage(message);
                            HUD.SetMessage(message);
                        }

                        if (Player.hasKey)
                        {
                            message = "You unlocked the gate!";
                            HUD.SetMessage(message);
                            MapManager.walkables.Add(destination);
                            MapManager.gateLocked = false;
                        }
                    }
                    break;

                case '☻':
                    if (MapManager.worldY == 2 && MapManager.worldX == 2)
                    {
                        if (Player.hasKey == false)
                        {
                            if (Player.gold < 100)
                            {
                                message = "\'I'll trade 100 gold for the key!\'";
                                MenuManager.SetTaskMessage(message);
                                HUD.SetMessage(message);
                            }

                            if (Player.gold > 100)
                            {
                                message = "\'Here is your key!\'";
                                MenuManager.SetTaskMessage(message);
                                HUD.SetMessage(message);
                                Player.hasKey = true;
                                Player.gold -= 100;
                            }
                        }

                        else if (Player.hasKey)
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
