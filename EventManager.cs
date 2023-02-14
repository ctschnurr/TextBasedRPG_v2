using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{

    internal class EventManager
    {


        static void MainMenu()
        {
            int next = 3;

            RefreshWindow();
            getMap.DrawMap(getMap.blank_frame);
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
                    RefreshWindow();
                    getMap.DrawMap(getMap.blank_frame);
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
                    player.name = Console.ReadLine();

                    break;

                case ConsoleKey.Q:
                    gameOver = true;
                    break;
            }
        }
    }


}
