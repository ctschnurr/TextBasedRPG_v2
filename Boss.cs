using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{

    // Boss class, inherits from Enemy
    internal class Boss : Enemy
    {
        public Boss()
        {
            name = "Skeleton King";
            health = 80;
            strength = 5;
            color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), enemyTemplate[1, 3]);
            power = enemyTemplate[1, 4];

            behavior = Behavior.chase;

            character = (char)2;
            type = "boss";
            healthMax = health;

            x = 18;
            y = 6;

            worldX = 1;
            worldY = 0;
        }

        // Boss gets a modified version of Update from the Enemy class, as he only ever chases
        public override void Update()
        {
            Character player = GameManager.GetPlayer();
            string choice = Chase(player);

            bool isWalkable;
            bool move = false;

            switch (choice)
            {
                case "left":
                    isWalkable = CollisionCheck(y, x - 1);

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
                    isWalkable = CollisionCheck(y, x + 1);

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
                    isWalkable = CollisionCheck(y - 1, x);

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
                    isWalkable = CollisionCheck(y + 1, x);

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
                FightCheck(this);
                erase = true;
                Draw(this);
            }
        }
    }
}
