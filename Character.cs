using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Character
    {
        protected int health;
        protected int healthMax;

        protected int strength;
        protected string name;
        protected string power;

        protected string type;
        protected int[] spawn = new int[] { 0, 0 };
        protected ConsoleColor color;
        protected char character;

        protected int x;
        protected int y;
        protected int lastX;
        protected int lastY;

        protected int worldX;
        protected int worldY;

        protected bool stunned;
        protected bool erase = false;

        public void StepBack()
        {
            x = lastX;
            y = lastY;
        }

        public void SetStunned(bool input)
        {
            stunned = input;
        }

        public bool GetStunned()
        {
            return stunned;
        }

        public static void Draw(Character subject)
        {
            GameManager.ResetWindowSize();
            char[,] map = MapManager.GetMap();
            char tile;
            string[] colorDat;

            if (subject.erase == true)
            {
                // draw over character's last position on screen with the approapriate tile from the reference map

                Console.SetCursorPosition(subject.lastX + 2, subject.lastY + 1);
                tile = map[subject.lastY, subject.lastX];
                MapManager.DrawTile(tile);

            }

            // draw the character on screen in set position

            Console.SetCursorPosition(subject.x + 2, subject.y + 1);
            tile = map[subject.y, subject.x];
            colorDat = MapManager.GetTileColor(tile);
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorDat[1]);
            Console.ForegroundColor = subject.color;
            Console.Write(subject.character);
            Console.ResetColor();
            subject.erase = false;
        }
        public void SetX(int set)
        {
            x = set;
        }

        public int GetX()
        {
            return x;
        }

        public void SetY(int set)
        {
            y = set;
        }

        public int GetY()
        {
            return y;
        }

        public int GetWorldX()
        {
            return worldX;
        }

        public int GetWorldY()
        {
            return worldY;
        }

        public void SetWorld(int x, int y)
        {
            worldX = x;
            worldY = y;
        }
        public void AddHealth(int add)
        {
            health += add;
        }

        public void RestoreHealth()
        {
            health = healthMax;
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetHealthMax()
        {
            return healthMax;
        }

        public void SetHealth(int set)
        {
            health = set;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string set)
        {
            name = set;
        }

        public string GetCharType()
        {
            return type;
        }

        public int GetStrength()
        {
            return strength;
        }

        public void AddStrength(int add)
        {
            strength += add;
        }

        public void SetPower(string set)
        {
            power = set;
        }

        public string GetPower()
        {
            return power;
        }

        public bool CollisionCheck(int inputY, int inputX)
        {
            char destination = MapManager.GetTile(inputY, inputX);
            bool isWalkable = MapManager.CheckWalkable(destination);

            return isWalkable;
        }

        public void FightCheck(Character instigator)
        {
            Player player = GameManager.GetPlayer();

            int playerX = player.GetX();
            int playerY = player.GetY();

            int instigatorX = instigator.GetX();
            int instigatorY = instigator.GetY();

            int instigatorWX = instigator.GetWorldX();
            int instigatorWY = instigator.GetWorldY();

            string instigatorType = instigator.GetCharType();

            List<Enemy> enemies = EnemyManager.GetEnemies();
            List<Boss> bosses = EnemyManager.GetBosses();

            if (instigator == player)
            {
                foreach (Enemy enemy in enemies)
                {
                    int enemyX = enemy.GetX();
                    int enemyY = enemy.GetY();

                    int enemyWX = enemy.GetWorldX();
                    int enemyWY = enemy.GetWorldY();

                    if (enemyX == instigatorX && enemyY == instigatorY && enemyWX == instigatorWX && enemyWY == instigatorWY)
                    {
                        BattleSystem.Battle(instigator, enemy);
                    }
                }

                foreach (Boss boss in bosses)
                {
                    int bossX = boss.GetX();
                    int bossY = boss.GetY();
                        
                    int bossWX = boss.GetWorldX();
                    int bossWY = boss.GetWorldY();

                    if (bossX == instigatorX && bossY == instigatorY && bossWX == instigatorWX && bossWY == instigatorWY)
                    {
                        BattleSystem.Battle(instigator, boss);
                    }
                }
            }

            if (instigatorType == "npc")
            {
                if (instigatorX == playerX && instigatorY == playerY) BattleSystem.Battle(instigator, player);
            }

            if (instigatorType == "boss")
            {
                if (instigatorX == playerX && instigatorY == playerY) BattleSystem.Battle(instigator, player);
            }
        }


    }
}
