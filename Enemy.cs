using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Enemy : Character
    {
        public string[,] enemyTemplate = new string[,]
        {
            {"Zombie","10","0", "Green", "bites" },
            {"Skeleton","15","1", "Gray", "smacks"},
            {"Monster","20","3", "Red", "scratches" },
        };


        public enum Behavior
        {
            chase,
            wander
        }

        public Behavior behavior;

        public Enemy()
        {
            Random rand = new Random();
            int roll = rand.Next(0, 3);

            name = enemyTemplate[roll, 0];
            health = Int32.Parse(enemyTemplate[roll, 1]);
            strength = Int32.Parse(enemyTemplate[roll, 2]);
            color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), enemyTemplate[roll, 3]);
            power = enemyTemplate[roll, 4]; 

            behavior = (Behavior)rand.Next(0, 2);

            character = (char)2;
            type = "npc";
            healthMax = health;

            x = rand.Next(20, 50);
            y = rand.Next(15, 35);

            worldX = 0;
            worldY = 0;

            stunned = false;
        }
        
        public void Update()
        {
            Character player = GameManager.GetPlayer();

            bool isWalkable;
            char destination = ' ';
            char[,] map = MapManager.GetWorld();
            bool move = false;
            erase = false;

            string choice = "blank";
            
            switch (behavior)
            {
                case Behavior.chase:
                    choice = Chase(player);
                    break;

                case Behavior.wander:
                    choice = Wander();
                    break;
            }
            
            switch (choice)
            {
                case "left":
                    destination = map[y, x - 1];
                    isWalkable = MapManager.CheckWalkable(destination, this);

                    if (isWalkable == true)
                    {
                        move = true;
                        lastY = y;
                        lastX = x;
                        x--;
                        break;
                    }
                    else
                    {
                        break;
                    }

                case "right":
                    destination = map[y, x + 1];
                    isWalkable = MapManager.CheckWalkable(destination, this);

                    if (isWalkable == true)
                    {
                        move = true;
                        lastY = y;
                        lastX = x;
                        x++;
                        break;
                    }
                    else
                    {
                        break;
                    }

                case "up":
                    destination = map[y - 1, x];
                    isWalkable = MapManager.CheckWalkable(destination, this);

                    if (isWalkable == true)
                    {
                        move = true;
                        lastY = y;
                        lastX = x;
                        y--;
                        break;
                    }
                    else
                    {
                        break;
                    }

                case "down":
                    destination = map[y + 1, x];
                    isWalkable = MapManager.CheckWalkable(destination, this);

                    if (isWalkable == true)
                    {
                        move = true;
                        lastY = y;
                        lastX = x;
                        y++;
                        break;
                    }
                    else
                    {
                        break;
                    }
            }

            if (move)
            {
                CollisionManager.CollisionCheck(destination, this);
                erase = true;
                Draw(this);
            }
        }

        string Chase(Character player)
        {
            Random rnd = new Random();
            string choice = "blank";

            int playerX = player.GetX();
            int playerY = player.GetY();

            if (playerY == y)
            {
                if (playerX > x) choice = "right";
                if (playerX < x) choice = "left";
            }

            if (playerX == x)
            {
                if (playerY > y) choice = "down";
                if (playerY < y) choice = "up";
            }

            else
            {
                int walk = rnd.Next(1, 3);

                switch (walk)
                {
                    case 1:
                        {
                            if (playerX > x) choice = "right";
                            if (playerX < x) choice = "left";
                            break;
                        }

                    case 2:
                        {
                            if (playerY > y) choice = "down";
                            if (playerY < y) choice = "up";
                            break;
                        }
                }
            }

            return choice;
        }

        string Wander()
        {
            Random rnd = new Random();
            string choice = "blank";

            int walk = rnd.Next(1, 5);
            if (walk == 1) choice = "right";
            if (walk == 2) choice = "left";
            if (walk == 3) choice = "up";
            if (walk == 4) choice = "down";

            return choice;
        }
    }
}
