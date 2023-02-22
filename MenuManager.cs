using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class MenuManager
    {
        public static void MainMenu() // MenuManager Class?
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






    }
}
