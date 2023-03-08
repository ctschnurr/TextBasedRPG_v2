using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class BattleSystem
    {
        private static bool battleOver;
        private static Character loser;
        private static int next;
        private static Random rand = new Random();
        private static bool flee;
        private static string message;

        // battle handler and mini game loop
        public static void Battle(Character first, Character second)
        {
            int turn = 1;

            battleOver = false;
            flee = false;
            MenuManager.SetMenu("menuFrame");
            MenuManager.Draw();
            HUD.Draw(first);
            HUD.Draw(second);

            List<Enemy> enemies = EnemyManager.GetEnemies();

            next = 3;

            loser = null; 

            while (battleOver == false)
            {
                if (turn == 1)
                {
                    Console.SetCursorPosition(4, next);
                    Console.WriteLine(first.name + " attacks " + second.name + "! " + first.name + " goes first!");
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
            
            // if the player successfully flees, it is caught here

            if (flee == true && first.type == "npc") first.stunned = true;
            if (flee == true && second.type == "npc") second.stunned = true;

            // if the battle ends without the player fleeing it is handled here

            if (battleOver == true && turn != 1 && flee == false)
            {
                if (loser.type == "player")
                {
                    Console.SetCursorPosition(4, next);
                    Console.WriteLine("You are critically injured!");
                    next += 2;

                    Console.ReadKey(true);
                    loser.health = loser.healthMax;
                    loser.lives --;

                    if (loser.lives != 0)
                    {
                        Console.SetCursorPosition(4, next);
                        Console.WriteLine("You lost conciousness!");
                        next += 2;

                        Console.ReadKey(true);

                        loser.x = 33;
                        loser.y = 10;
                        MapManager.worldX = 2;
                        MapManager.worldY = 2;

                        message = "\'I saved you! Please be careful!\'";
                        HUD.SetMessage(message);
                        MapManager.redraw = true;
                    }

                    else
                    {
                        Console.SetCursorPosition(4, next);
                        Console.WriteLine("You have DIED!");

                        Console.ReadKey(true);

                        MenuManager.GameOver();
                    }
                }

                if (loser.type == "npc")
                {
                    Console.SetCursorPosition(4, next);
                    Console.WriteLine(loser.name + " has DIED!");
                    next += 2;

                    // convert from Character to Enemy
                    Enemy convert = null;
                    foreach (Enemy enemy in enemies)
                    {
                        if (enemy.x == loser.x && enemy.y == loser.y) convert = enemy;
                    }

                    EnemyManager.SetDeadEnemy(convert);
                    
                    int winnings = rand.Next(3, 13);
                    Player.gold += winnings;
                    Console.SetCursorPosition(4, next);
                    Console.WriteLine("You got " + winnings + " gold!");
                    Console.ReadKey(true);

                    EnemyManager.SetRef(convert);
                }
            }

            if (flee && first.type == "player") first.StepBack();
            if (flee && second.type == "player") second.StepBack();

            MapManager.redraw = true;
        }

        // this handles the player's turn and their choice to attack or flee
        static void PlayerChoice(Character first, Character second)
        {
            Console.Write("(A)ttack   (R)un : ");
            next += 2;

            ConsoleKeyInfo choice = Console.ReadKey(true);

            while (choice.Key != ConsoleKey.A && choice.Key != ConsoleKey.R)
            {
                choice = Console.ReadKey(true);
            }

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

        // this resets the cursor position 
        static void ReDrawCheck(Character A, Character B)
        {
            if (next > 36)
            {
                MenuManager.Draw();
                HUD.Draw(A);
                HUD.Draw(B);
                next = 3;
            }
        }

        // this handles the attack phase for both player and enemy
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
                damage += attacker.strength;

                Console.SetCursorPosition(4, next);
                Console.WriteLine(attacker.name + " " + attacker.power + " " + victim.name + " for " + damage + " damage!");
                next += 2;

                victim.health -= damage;
                if (victim.health <= 0) victim.health = 0;
                HUD.Draw(victim);

                if (attacker.type == "player") Console.ReadKey(true);

                // if (attacker.type == "npc") ReDrawCheck();

                if (victim.health <= 0)
                {
                    MapManager.redraw = true;
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