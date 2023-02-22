using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                switch (destination)
                {
                    case '▀':
                        Heal(subject);
                        messageContent = "Your health has been restored!";
                        messageNew = true;
                        break;
                }
            }

            if (subject.type == "npc")
            {
                if (subject.x == Program.player.x && subject.y == Program.player.y) BattleSystem.Battle(subject, Program.player);
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
                Console.Write("                          ");
            }
        }
    }
}
