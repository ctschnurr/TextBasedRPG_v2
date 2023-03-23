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
        private static int next;
        private static Random rand = new Random();
        private static bool flee;
        private static string message;

        static Character loser;

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

            loser = null;

            string firstName = first.GetName();
            string secondName = second.GetName();
            string loserName;

            string firstType = first.GetType();
            string secondType = second.GetType();

            next = 3;
 

            while (battleOver == false)
            {
                if (turn == 1)
                {
                    Console.SetCursorPosition(4, next);
                    Console.WriteLine(firstName + " attacks " + secondName + "! " + firstName + " goes first!");
                    next += 2;

                    battleOver = Attack(first, second);
                    if (battleOver == true) loser = second;
                    turn++;
                }

                if (battleOver == false && secondType == "player")
                {
                    Console.SetCursorPosition(4, next);

                    PlayerChoice(second, first);
                }

                if (battleOver == false && firstType == "npc")
                {
                    battleOver = Attack(first, second);
                    if (battleOver == true) loser = second;
                }

                if (battleOver == false && secondType == "npc")
                {
                    battleOver = Attack(second, first);
                    if (battleOver == true) loser = first;
                }

                if (battleOver == false && firstType == "player")
                {
                    Console.SetCursorPosition(4, next);

                    PlayerChoice(first, second);

                    ReDrawCheck(first, second);
                }
            }
            
            // if the player successfully flees, it is caught here

            if (flee == true && firstType == "npc") first.stunned = true;
            if (flee == true && secondType == "npc") second.stunned = true;

            // if the battle ends without the player fleeing it is handled here

            if (battleOver == true && turn != 1 && flee == false)
            {
                Player player = GameManager.GetPlayer();

                string loserType = loser.GetType();

                if (loserType == "player")
                {

                    Console.SetCursorPosition(4, next);
                    Console.WriteLine("You are critically injured!");
                    next += 2;

                    Console.ReadKey(true);
                    player.RestoreHealth();
                    player.SetLives(-1);
                    int lives = player.GetLives();

                    if (lives != 0)
                    {
                        Console.SetCursorPosition(4, next);
                        Console.WriteLine("You lost conciousness!");
                        next += 2;

                        Console.ReadKey(true);

                        loser.SetX(33);
                        loser.SetY(10);
                        MapManager.SetWorld(2, 2);

                        message = "\'I saved you! Please be careful!\'";
                        HUD.SetMessage(message);
                        MapManager.SetRedraw(true);
                    }

                    else
                    {
                        Console.SetCursorPosition(4, next);
                        Console.WriteLine("You have DIED!");

                        Console.ReadKey(true);

                        MenuManager.GameOver();
                    }
                }

                if (loserType == "npc")
                {
                    loserName = loser.GetName();

                    Console.SetCursorPosition(4, next);
                    Console.WriteLine(loserName + " has DIED!");
                    next += 2;

                    // convert from Character to Enemy
                    Enemy convert = null;
                    foreach (Enemy enemy in enemies)
                    {
                        int enemyX = enemy.GetX();
                        int loserX = loser.GetX();

                        int enemyY = enemy.GetY();
                        int loserY = loser.GetY();

                        if (enemyX == loserX && enemyY == loserY) convert = enemy;
                    }

                    EnemyManager.SetDeadEnemy(convert);
                    
                    int winnings = rand.Next(3, 13);
                    player.AddGold(winnings);
                    Console.SetCursorPosition(4, next);
                    Console.WriteLine("You got " + winnings + " gold!");
                    Console.ReadKey(true);

                    EnemyManager.SetRef(convert);
                }
            }

            if (flee && firstType == "player") first.StepBack();
            if (flee && secondType == "player") second.StepBack();

            MapManager.SetRedraw(true);
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

        // this resets the cursor position in the menu
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
            string attackerType = attacker.GetType();

            Random rand = new Random();
            int swing = rand.Next(1, 4);
            int damage;

            string attackerName = attacker.GetName();
            string victimName = victim.GetName();

            if (swing == 1)
            {
                Console.SetCursorPosition(4, next);
                Console.WriteLine(attackerName + " missed!");
                next += 2;

                if (attackerType == "player") Console.ReadKey(true);

                return false;
            }

            else
            {
                int strength = attacker.GetStrength();
                string power = attacker.GetPower();

                damage = rand.Next(1, 11);
                damage += strength;

                Console.SetCursorPosition(4, next);
                Console.WriteLine(attackerName + " " + power + " " + victimName + " for " + damage + " damage!");
                next += 2;


                int health = victim.GetHealth();
                health -= damage;
                if (health <= 0) victim.SetHealth(0);
                else victim.SetHealth(health);
                HUD.Draw(victim);

                if (attackerType == "player") Console.ReadKey(true);

                // if (attacker.type == "npc") ReDrawCheck();

                if (health <= 0)
                {
                    MapManager.SetRedraw(true);
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