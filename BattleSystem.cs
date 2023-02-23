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
        static Random rand = new Random();
        static bool flee;

        public static void Battle(Character first, Character second)
        {
            int turn = 1;
            ConsoleKeyInfo choice;

            battleOver = false;
            flee = false;
            MapManager.DrawMenu(EventManager.atlas.menuFrame);
            first.ShowHud();
            second.ShowHud();

            // int damage;
            next = 3;

            loser = null; 

            while (battleOver == false)
            {
                if (turn == 1)
                {
                    Console.SetCursorPosition(4, next);
                    Console.WriteLine(first.name + " started a fight! " + first.name + " goes first!");
                    next += 2;

                    battleOver = Attack(first, second);
                    if (battleOver == true) loser = second;
                    turn++;
                }

                if (battleOver == false && second.type == "player")
                {
                    Console.SetCursorPosition(4, next);

                    PlayerChoice(second, first);
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

                    PlayerChoice(first, second);

                    ReDrawCheck(first, second);
                }
            }

            if (flee == true && first.type == "npc") first.stunned = true;
            if (flee == true && second.type == "npc") second.stunned = true;

            if (battleOver == true && turn != 1 && flee == false)
            {
                if (loser.type == "player")
                {
                    Console.SetCursorPosition(4, next);
                    Console.WriteLine("I'm afraid you have died! We'll restore and respawn you.");
                    Console.ReadKey(true);
                    loser.health = loser.healthMax;
                    loser.lives --;
                    loser.x = loser.spawn[0];
                    loser.y = loser.spawn[1];
                    MapManager.worldX = 1;
                    MapManager.worldY = 1;
                }

                if (loser.type == "npc")
                {
                    Enemy convert = null;
                    foreach (Enemy enemy in Enemy.enemies)
                    {
                        if (enemy.x == loser.x && enemy.y == loser.y) convert = enemy;
                    }

                    Enemy.deadEnemies.Add(convert);

                    int winnings = rand.Next(3, 13);
                    Player.gold += winnings;
                    Console.SetCursorPosition(4, next);
                    Console.WriteLine("You got " + winnings + " gold!");
                    Console.ReadKey(true);

                    if (Enemy.enemysave1 == null)
                    {
                        Enemy.enemysave1 = convert;
                    }

                    else if (Enemy.enemysave2 == null)
                    {
                        Enemy.enemysave2 = convert;
                    }

                    else if (Enemy.enemysave3 == null)
                    {
                        Enemy.enemysave3 = convert;
                    }

                    else
                    {
                        Enemy.enemysave3 = Enemy.enemysave2;
                        Enemy.enemysave2 = Enemy.enemysave1;
                        Enemy.enemysave1 = convert;
                    }
                }
            }
            Program.player.x = Program.player.lastX;
            Program.player.y = Program.player.lastY;
            EventManager.redraw = true;
        }

        static void PlayerChoice(Character first, Character second)
        {
            Console.Write("(A)ttack   (R)un : ");
            next += 2;

            ConsoleKeyInfo choice = Console.ReadKey(true); // build playerChoice()?;

            ReDrawCheck(first, second);

            switch (choice.Key)
            {
                case ConsoleKey.A:
                    battleOver = Attack(first, second);
                    if (battleOver == true) loser = second;
                    break;

                case ConsoleKey.R:
                    Console.SetCursorPosition(4, next);

                    int run = rand.Next(1, 4);
                    if (run != 1)
                    {
                        Console.SetCursorPosition(4, next);
                        Console.WriteLine("You couldn't get away!");
                        next += 2;
                    }

                    else
                    {
                        Console.SetCursorPosition(4, next);
                        Console.WriteLine("You flee from battle!");
                        battleOver = true;
                        flee = true;
                        next += 2;
                        Console.ReadKey(true);
                    }
                    break;
            }
        }

        static void ReDrawCheck(Character A, Character B)
        {
            if (next > 36)
            {
                MapManager.DrawMenu(EventManager.atlas.menuFrame);
                A.ShowHud();
                B.ShowHud();
                next = 3;
            }
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

                if (attacker.type == "player") Console.ReadKey(true);

                return false;
            }

            else
            {
                damage = rand.Next(1, 11);

                Console.SetCursorPosition(4, next);
                Console.WriteLine(attacker.name + " hit " + victim.name + " for " + damage + " damage!");
                next += 2;

                victim.health -= damage;
                if (victim.health <= 0) victim.health = 0;
                attacker.ShowHud();
                victim.ShowHud();

                if (attacker.type == "player") Console.ReadKey(true);

                // if (attacker.type == "npc") ReDrawCheck();

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