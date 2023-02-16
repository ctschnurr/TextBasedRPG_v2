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

        public static void EventCheck(char destination, Character subject)
        {
            if (subject.type == "player")
            {
                switch (destination)
                {
                    case (char)2:
                        // start a battle
                        BattleSystem.Battle(subject, Program.enemy);
                    break;

                    case '▀':
                        Heal(subject);
                        Console.SetCursorPosition(42, 40);
                        Console.WriteLine("║ Player Healed!"); // write a HudMessage() method that displays the message for a few turns then clears
                        break;
                }
            }

            if (subject.type == "npc")
            {
                if(destination == (char)1) BattleSystem.Battle(subject, Program.player);
            }
        }
        public static void MainMenu() // MenuManager Class?
        {
            int next = 3;

            RefreshWindow();
            MapManager.DrawMenu(atlas.menuFrame);

            Console.SetCursorPosition(4, next);
            Console.WriteLine("WELCOME TO THE GRAVEYARD!");
            next += 2;

            Console.SetCursorPosition(4, next);
            Console.WriteLine("Please choose from the following options:");

            Console.SetCursorPosition(4, 7);
            Console.WriteLine("(N)ew Game");
            Console.SetCursorPosition(4, 8);
            Console.WriteLine("(Q)uit Game");

            Console.SetCursorPosition(4, 40);
            Console.WriteLine("By Chris Schnurr");

            ConsoleKeyInfo choice = Console.ReadKey(true);

            switch (choice.Key)
            {
                default:

                    // write up an Instructions .txt and treat it as a map in itself, so it can be brought up easily whenever player wants it

                    RefreshWindow();
                    MapManager.DrawMenu(atlas.menuFrame);
                    next = 5;
                    Console.SetCursorPosition(8, next);
                    Console.WriteLine("¡Θ¡");
                    next++;
                    Console.SetCursorPosition(8, next);
                    Console.WriteLine("╤═╤     Visit the shrine to heal!");
                    next++;
                    Console.SetCursorPosition(8, next);
                    Console.WriteLine("┴▀┴");
                    next += 2;
                    Console.SetCursorPosition(9, next);
                    Console.WriteLine((char)2 + "     Watch out for the Undead!");
                    next += 2;
                    Console.SetCursorPosition(8, next);
                    Console.WriteLine("Press escape from the map to quit!");
                    next += 6;

                    Console.SetCursorPosition(4, 40);
                    Console.WriteLine("By Chris Schnurr");

                    Console.SetCursorPosition(4, next);
                    Console.Write("Before we begin, please enter your name: ");
                    Program.player.name = Console.ReadLine();

                    break;

                case ConsoleKey.Q:
                    Program.gameOver = true;
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
    }
}
