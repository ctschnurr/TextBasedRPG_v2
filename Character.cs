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

        // public int strength;
        public string name;
        public int lives;
        public string state = "alive";

        public string type;
        public int[] spawn = new int[] { 0, 0 };
        public ConsoleColor color;
        public char character;

        public int x;
        public int y;
        public int lastX;
        public int lastY;

        // I'm building a better DrawCharacter() in MapManager, delete this once thats done
        public void Draw(int charX, int charY, char character)
        {
            Console.SetCursorPosition(charX, charY);
            Console.ForegroundColor = color;
            Console.WriteLine(character);
            Console.ResetColor();
        }

        public void ShowHud()
        {
            if(type == "player") Console.SetCursorPosition(4, 40);
            if(type == "npc") Console.SetCursorPosition(46, 40);

            string hudHealth = health.ToString();
            Console.Write(name.PadRight(name.Length + 1) + ": Health: " + hudHealth.PadRight(5));

            if (type == "player") Console.Write("Lives: " + lives);
        }
    }
}
