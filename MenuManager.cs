using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class MenuManager
    {
        public static void MainMenu()
        {
            int next = 3;

            EventManager.RefreshWindow();
            MapManager.DrawMenu(EventManager.atlas.menuFrame);

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

                    EventManager.RefreshWindow();
                    MapManager.DrawMenu(EventManager.atlas.instructions);

                    Console.SetCursorPosition(4, 38);
                    Console.Write("Before we begin, please enter your name: ");
                    Program.player.name = Console.ReadLine();

                    break;

                case ConsoleKey.Q:
                    Program.gameOver = true;
                    break;
            }
        }

        public static void PauseMenu()
        {
            bool go = false;
            while (go == false)
            {

                EventManager.RefreshWindow();
                MapManager.DrawMenu(EventManager.atlas.pauseMenu);

                Console.SetCursorPosition(12, 5);
                Console.Write(Program.player.name);

                Console.SetCursorPosition(42, 5);
                Console.Write(Program.player.health + "/" + Program.player.healthMax);

                Console.SetCursorPosition(13, 7);
                Console.Write(Program.player.lives);

                Console.SetCursorPosition(40, 7);
                Console.Write(Player.gold);

                Console.SetCursorPosition(12, 9);
                Console.Write(EventManager.taskMessage);

                if (Enemy.enemysave1 != null)
                {
                    Console.SetCursorPosition(8, 13);
                    Console.Write(Enemy.enemysave1.name + " - Health: " + Enemy.enemysave1.healthMax);
                }

                if (Enemy.enemysave2 != null)
                {
                    Console.SetCursorPosition(8, 14);
                    Console.Write(Enemy.enemysave2.name + " - Health: " + Enemy.enemysave2.healthMax);
                }

                if (Enemy.enemysave3 != null)
                {
                    Console.SetCursorPosition(8, 15);
                    Console.Write(Enemy.enemysave3.name + " - Health: " + Enemy.enemysave3.healthMax);
                }

                ConsoleKeyInfo choice = Console.ReadKey(true);

                switch (choice.Key)
                {
                    case ConsoleKey.Q:
                        EventManager.RefreshWindow();
                        MapManager.DrawMenu(EventManager.atlas.menuFrame);

                        Console.SetCursorPosition(6, 4);
                        Console.Write("Really quit? Save is not yet implimented.");

                        Console.SetCursorPosition(4, 40);
                        Console.Write("(C)ontinue                    (Q)uit");

                        ConsoleKeyInfo choice2 = Console.ReadKey(true);

                        if (choice2.Key == ConsoleKey.Q)
                        {
                            Program.gameOver = true;
                            go = true;
                        }
                        break;

                    case ConsoleKey.C:
                        go = true;
                        break;
                }

            }

        }






    }
}
