using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    internal class Character
    {
        public int health;
        public int healthMax;

        public int strength;
        public string name;
        public string power;

        public string type;
        public int[] spawn = new int[] { 0, 0 };
        public ConsoleColor color;
        public char character;

        public int x;
        public int y;
        public int lastX;
        public int lastY;

        public int worldX;
        public int worldY;

        public bool stunned;
        public bool erase = false;

        public void StepBack()
        {
            x = lastX;
            y = lastY;
        }

        public void SetX(int set)
        {
            x = set;
        }

        public void SetY(int set)
        {
            y = set;
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
    }
}
