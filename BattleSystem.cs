using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class BattleSystem
    {
        public static bool battleOver;
        public static Character loser;
        public static int next;
        public static void Battle(Character first, Character second)
        {
            battleOver = true;
            int turn = 1;
            ConsoleKeyInfo choice;

            battleOver = false;
            ReDraw();

            Random rand = new Random();
            next = 3;
            int swing;
            int damage;

            Character loser = null; 

            while (battleOver == false)
            {
                if (turn == 1)
                {
                    Console.SetCursorPosition(4, next);
                    Console.WriteLine(first.name + " started a fight! " + first.name + " goes first!");
                    next += 2;

                    battleOver = Attack(first, second);
                    turn++;
                }

                if (battleOver == false && second.type == "player")
                {

                    Console.SetCursorPosition(4, next);
                    Console.Write("(A)ttack   (R)un : ");
                    next += 2;

                    if (next > 36)
                    {
                        ReDraw();
                        next = 3;
                    }

                    choice = Console.ReadKey(true); // build playerChoice()?;
                    switch (choice.Key)
                    {
                        case ConsoleKey.A:
                            battleOver = Attack(second, first);
                            if (battleOver == true) loser = first;
                            break;

                        case ConsoleKey.R:
                            // build run option
                            Console.SetCursorPosition(4, next);
                            Console.WriteLine("You don't know how to run yet!");
                            next += 2;

                            if (next > 36)
                            {
                                ReDraw();
                                next = 3;
                            }

                            Console.ReadKey(true);
                            break;
                    }
                }

                if (battleOver == false && first.type == "npc")
                {
                    battleOver = Attack(first, second);
                    if (battleOver == true) loser = second;
                }

                if (battleOver == false && second.type == "npc")
                {
                    battleOver = Attack(second, first);
                    if (battleOver == true) loser = first;
                }

                if (battleOver == false && first.type == "player")
                {
                    Console.SetCursorPosition(4, next);
                    Console.Write("(A)ttack   (R)un : ");
                    next += 2;

                    if (next > 36)
                    {
                        ReDraw();
                        next = 3;
                    }

                    choice = Console.ReadKey(true); // build playerChoice()?;
                    switch (choice.Key)
                    {
                        case ConsoleKey.A:
                            battleOver = Attack(first, second);
                            if (battleOver == true) loser = second;
                            break;

                        case ConsoleKey.R:
                            // build run option
                            Console.SetCursorPosition(4, next);
                            Console.WriteLine("You don't know how to run yet!");
                            next += 2;

                            if (next > 36)
                            {
                                ReDraw();
                                next = 3;
                            }

                            Console.ReadKey(true);
                            break;
                    }
                }
            }

            if (battleOver == true && turn != 1)
            {
                if (loser.type == "player")
                {
                    Console.SetCursorPosition(4, next);
                    Console.WriteLine("I guess you suck, but I'll restore and respawn you!");
                    Console.ReadKey(true);
                    loser.health = loser.healthMax;
                    // loser.lives -= 1;
                    loser.x = loser.spawn[0];
                    loser.y = loser.spawn[1];
                }

                else
                {
                    Program.enemy = new Enemy();
                }

            }
        }

        static void ReDraw()
        {
            MapManager.DrawMap(EventManager.atlas.menuFrame);
            Program.player.ShowHud();
            Program.enemy.ShowHud();
        }

        static bool Attack(Character attacker, Character victim)
        {
            Random rand = new Random();
            int swing = rand.Next(1, 4);
            int damage;

            if (swing == 1)
            {
                Console.SetCursorPosition(4, next);
                Console.WriteLine(attacker.name + " missed!");
                next += 2;

                Console.ReadKey(true);

                if (next > 36)
                {
                    ReDraw();
                    next = 2;
                }

                return false;
            }

            else
            {
                damage = rand.Next(1, 11);

                Console.SetCursorPosition(4, next);
                Console.WriteLine(attacker.name + " hit " + victim.name + " for " + damage + " damage!");
                next += 2;

                victim.health -= damage;
                Program.player.ShowHud();
                Program.enemy.ShowHud();

                Console.ReadKey(true);

                if (next > 36)
                {
                    Console.Clear();
                    MapManager.DrawMap(EventManager.atlas.menuFrame);
                    next = 2;
                }

                if (victim.health <= 0)
                {
                    Console.SetCursorPosition(4, next);
                    Console.WriteLine(victim.name + " has DIED!");
                    next += 2;
                    Console.ReadKey(true);
                    EventManager.redraw = true;
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }


    }
}