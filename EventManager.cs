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
        static public MapManager atlas = new MapManager();

        static public bool redraw = true;

        public static int height = atlas.menuFrame.GetLength(0) + 2;
        public static int width = atlas.menuFrame.GetLength(1) + 2;

        static int messageCount = 0;
        public static string messageContent = null;
        public static bool messageNew = false;

        public static bool gateLocked = true;
        public static string taskMessage = "Explore!";


        public static void EventCheck(char destination, Character subject)
        {
            if (subject.type == "player")
            {
                bool fight = false;
                Enemy victim = null;
                
                foreach (Enemy enemy in Enemy.enemies)
                {
                    if (enemy.x == subject.x && enemy.y == subject.y)
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
                        EventManager.redraw = true;
                    }

                    else if (MapManager.worldY == 2 && MapManager.worldX == 2)
                    {
                        MapManager.worldY = 1;
                        Program.player.y = 23;
                        Program.player.x = 51;
                        EventManager.redraw = true;
                    }
                }
            }



            if (subject.type == "npc")
            {
                if (subject.x == Program.player.x && subject.y == Program.player.y) BattleSystem.Battle(subject, Program.player);
            }
        }

        public static void Triggers(char destination)
        {
            switch (destination)
            {
                case '─':
                    if (MapManager.worldY == 1 && MapManager.worldX == 1 && gateLocked == true)
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
                            gateLocked = false;
                        }
                    }
                    break;

                case '☻':
                    if (MapManager.worldY == 2 && MapManager.worldX == 2)
                    {
                        if (Player.hasKey == false)
                        {
                            messageContent = "\'I'll trade 100 gold for the key\'";
                            taskMessage = "Bring the witch 100 gold!";
                            messageNew = true;
                        }

                        if (Player.hasKey)
                        {
                            messageContent = "You unlocked the gate!";
                            messageNew = true;
                            MapManager.walkables.Add(destination);
                            gateLocked = false;
                        }
                    }
                    break;


            }
        }
        
        public static void RefreshWindow()
        {
            if (Console.WindowHeight != height || Console.WindowWidth != width)
            {
                Console.SetWindowSize(width, height);
                Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
                redraw = true;
            }
        }

        // have this reference the tile the player is standing on/moving to, and if its the preset healer character, then heal. Take out the hard coded coords.
        public static void Heal(Character player)
        {
            player.health = player.healthMax;
        }

        public static void MapMessage()
        {
            if (messageNew == true)
            {
                messageCount = 10;
                messageNew = false;
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
