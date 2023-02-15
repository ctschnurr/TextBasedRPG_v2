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
        public int lives;
        public int strength = 10;
        public string name;
        bool living = true;
        public string type;
        public int[] spawn = new int[] { 0, 0 };
        public ConsoleColor color;

        public int x;
        public int y;
        public void Draw(int charX, int charY, char character)
        {
            Console.SetCursorPosition(charX, charY);
            Console.ForegroundColor = color;
            Console.WriteLine(character);
            Console.ResetColor();
        }
    }
}
