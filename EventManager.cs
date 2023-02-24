using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextBasedRPG_v2
{
    internal class EventManager
    {
        // game fundamentals
        static public MapManager atlas = new MapManager();
        
        // static public bool redraw = true;

        public static int height = atlas.menuFrame.GetLength(0) + 2;
        public static int width = atlas.menuFrame.GetLength(1) + 2;

        // pop up message variables
        static int messageCount = 0;
        public static string messageContent = null;
        public static bool messageNew = false;
        public static string taskMessage = "Explore!";

        //this handles the pickups and battle detection
        public static void EventCheck(char destination, Character subject)
        {
            if (subject.type == "player")
            {
                bool fight = false;
                Enemy victim = null;

                foreach (Enemy enemy in Enemy.enemies)
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
                        Program.player.y = 14;
                        Program.player.x = 42;
                        MapManager.redraw = true;
                    }

                    else if (MapManager.worldY == 2 && MapManager.worldX == 2)
                    {
                        MapManager.worldY = 1;
                        Program.player.y = 23;
                        Program.player.x = 51;
                        MapManager.redraw = true;
                    }
                }

                if (destination == 'ō')
                {
                    if (Program.player.health == Program.player.healthMax)
                    {
                        messageContent = "You found a potion, but health is full!";
                        messageNew = true;
                    }

                    if (Program.player.health != Program.player.healthMax)
                    {
                        messageContent = "You found a potion, health is restored!";
                        messageNew = true;

                        Program.player.health = Program.player.healthMax;

                        char[,] holder = MapManager.world[MapManager.worldY, MapManager.worldX];
                        holder[Program.player.y, Program.player.x] = ' ';
                    }
                }

                if (destination == '┼')
                {
                        messageContent = "You found a sword! You deal 3 more damage!";
                        messageNew = true;

                        Program.player.strength += 3;
                    Program.player.power = "slashes";

                        char[,] holder = MapManager.world[MapManager.worldY, MapManager.worldX];
                        holder[Program.player.y, Program.player.x] = ' ';                   
                }

                if (destination == '°')
                {
                        messageContent = "You found a gold coin!";
                        messageNew = true;

                        Player.gold += 1;

                        char[,] holder = MapManager.world[MapManager.worldY, MapManager.worldX];
                        holder[Program.player.y, Program.player.x] = ' ';
                 
                }


            }

            if (subject.type == "npc")
            {
                if (subject.x == Program.player.x && subject.y == Program.player.y) BattleSystem.Battle(subject, Program.player);
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
                            messageContent = "The gate is locked.. find the key!";
                            taskMessage = "Find the key!";
                            messageNew = true;
                        }
                        
                        if (Player.hasKey)
                        {
                            messageContent = "You unlocked the gate!";
                            messageNew = true;
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
                                messageContent = "\'I'll trade 100 gold for the key!\'";
                                taskMessage = "Bring the witch 100 gold!";
                                messageNew = true;
                            }

                            if (Player.gold > 100)
                            {
                                messageContent = "\'Here is your key!\'";
                                taskMessage = "Use the key on the gate!";
                                messageNew = true;
                                Player.hasKey = true;
                                Player.gold -= 100;
                            }
                        }

                        if (Player.hasKey)
                        {
                            messageContent = "\'You have your key! Now begone!\'";
                            messageNew = true;
                        }
                    }
                    break;

            }
        }
        
        // this detects if the window size has been tampered with, and resets it
        public static void RefreshWindow()
        {
            if (Console.WindowHeight != height || Console.WindowWidth != width)
            {
                Console.SetWindowSize(width, height);
                Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
                MapManager.redraw = true;
            }
        }

        // this handles the map's mini message
        public static void MapMessage()
        {
            if (messageNew == true)
            {
                messageCount = 10;
                messageNew = false;

                Console.SetCursorPosition(45, 40);
                Console.Write("                                             ");
            }

            if (messageCount > 0)
            {
                Console.SetCursorPosition(46, 40);
                Console.Write(messageContent);
                messageCount--;
            }

            if (messageCount == 0)
            {
                Console.SetCursorPosition(45, 40);
                Console.Write("                                             ");
            }
        }
    }
}
