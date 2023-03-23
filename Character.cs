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

        public int worldX;
        public int worldY;

        public bool stunned;
        public bool erase = false;

        public void StepBack()
        {
            x = lastX;
            y = lastY;
        }

        public static void Draw(Character subject)
        {
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

        public string GetType()
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
    }
}
